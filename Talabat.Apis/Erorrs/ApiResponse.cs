
namespace Talabat.Apis.Erorrs
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
		public string? Massage { get; set; }
		public ApiResponse(int statuscode,string? message=null)
		{
			StatusCode = statuscode;
			Massage = message??GetDefultMessageForStatusCode(statuscode);
		}

		private string? GetDefultMessageForStatusCode(int statuscode)
		{
			return statuscode switch
			{
				400 => "Bad Request",
				401 => "UnAuthorized",
				404 => "Not Found",
				500 => "Server Erorr",
				_ => null
			};
		}
	}
}
