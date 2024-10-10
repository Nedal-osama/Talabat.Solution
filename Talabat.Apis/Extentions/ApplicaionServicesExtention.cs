using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Erorrs;
using Talabat.Apis.Helpers;
using Talabat.Core.Repository.Contract;
using Talabat.Repository;

namespace Talabat.Apis.Extentions
{
	public static class ApplicaionServicesExtention
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper((typeof(MappingProfile)));
			services.Configure<ApiBehaviorOptions>(option =>
			{
				option.InvalidModelStateResponseFactory = (actioncontext) =>
				{
					var erorrs = actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)
														 .SelectMany(p => p.Value.Errors)
														 .Select(E => E.ErrorMessage)
														 .ToList();
					var response = new ApiValidationErorrResponse()
					{
						Erorrs = erorrs
					};
					return new BadRequestObjectResult(response);
				};
			});
			return services;
		}
		public static WebApplication UseSwaggerMiddelWere(this WebApplication app)
		{
			
				app.UseSwagger();
				app.UseSwaggerUI();
			
			return app;
		}
	}

}
