
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.Apis.Helpers;
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
			;
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddAutoMapper((typeof(MappingProfile)));

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
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseStaticFiles();
			app.MapControllers();

			app.Run();
		}
	}
}
