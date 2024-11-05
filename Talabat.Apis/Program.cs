
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.Apis.Erorrs;
using Talabat.Apis.Extentions;
using Talabat.Apis.Helpers;
using Talabat.Apis.MiddelWere;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Identity;
using Talabat.Services;

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
			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});
			builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider=>

			{
				var connection = builder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			}));
			
			builder.Services.AddApplicationServices();
			builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				
			}).AddEntityFrameworkStores<AppIdentityDbContext>();

			builder.Services.AddAuthentication().AddJwtBearer("Bearer", options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["Jwt:ValidIssure"],
					ValidAudience = builder.Configuration["Jwt:ValidAudience"],
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:AuthKey"] ?? string.Empty))
				};
			});
			builder.Services.AddAuthentication();
			builder.Services.AddScoped(typeof(IAuthSearvice), typeof(AuthServices));
			
			builder.Services.AddScoped<IOrderService, OrderService>();



			var app = builder.Build();
			//Ask Clr Explisitly For Creating Object From StoreContext
		    using	var scope=app.Services.CreateScope();
			var servieces=scope.ServiceProvider;
			var _dbContext= servieces.GetRequiredService<StoreContext>();
			var _identityDbContext=servieces.GetRequiredService<AppIdentityDbContext>();
			var _usermanger=servieces.GetRequiredService<UserManager<AppUser>>();
			var loggerFactory=servieces.GetRequiredService<ILoggerFactory>();
			try

			{
				//await _dbContext.Database.MigrateAsync();//update database
			    await	StoreContextSeed.SeedAsync(_dbContext);//Data Seeding
				//await _identityDbContext.Database.MigrateAsync();
				await AppIdentityDbContextSeed.SeedUserAsync(_usermanger);
				await _dbContext.Database.MigrateAsync(); // Ensure migrations are applied
				await StoreContextSeed.SeedAsync(_dbContext);
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
