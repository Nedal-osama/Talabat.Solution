
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.Apis.Erorrs;
using Talabat.Apis.Extentions;
using Talabat.Apis.Helpers;
using Talabat.Apis.MiddelWere;
using Talabat.Core.Entites;
using Talabat.Core.Repository.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.Apis
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider=>

			{
				var connection = builder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			}));
			
			builder.Services.AddApplicationServices();

			var app = builder.Build();
			//Ask Clr Explisitly For Creating Object From StoreContext
		    using	var scope=app.Services.CreateScope();
			var servieces=scope.ServiceProvider;
			var _dbContext= servieces.GetRequiredService<StoreContext>();
			var loggerFactory=servieces.GetRequiredService<ILoggerFactory>();
			try
			{
				//await _dbContext.Database.MigrateAsync();//update database
			    await	StoreContextSeed.SeedAsync(_dbContext);//Data Seeding
			}
			catch (Exception ex)
			{

				var logger=loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An erorr occurred during migrations");
			}


			// Configure the HTTP request pipeline.
			app.UseMiddleware<ExceptionMiddelwere>();
			if (app.Environment.IsDevelopment())
			{ 
			app.UseSwaggerMiddelWere();
			};
			app.UseStatusCodePagesWithReExecute("/Errors/{0}");
			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseStaticFiles();
			app.MapControllers();

			app.Run();
		}
	}
}
