using System.Net;
using System.Text.Json;
using Talabat.Apis.Erorrs;

namespace Talabat.Apis.MiddelWere
{
	public class ExceptionMiddelwere
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddelwere> _logger;
		private readonly IWebHostEnvironment _env;

		public ExceptionMiddelwere(RequestDelegate next,ILogger<ExceptionMiddelwere>logger,IWebHostEnvironment env)
        {
			_next = next;
			_logger = logger;
			_env = env;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next.Invoke(httpContext);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				httpContext.Response.ContentType = "application/json";
				httpContext.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
				var response = _env.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.StackTrace.ToString())
					:new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
				var option = new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				};
				var json=JsonSerializer.Serialize(response);
				httpContext.Response.WriteAsync(json);
			}
		}
	}
}
