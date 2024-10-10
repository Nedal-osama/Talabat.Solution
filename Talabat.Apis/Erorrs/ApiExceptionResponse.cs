namespace Talabat.Apis.Erorrs
{
	public class ApiExceptionResponse:ApiResponse
	{
        public string? Detailes { get; set; }
        public ApiExceptionResponse(int statuscode,string? message=null,string? detailes=null):base(statuscode,message)
        {
            Detailes= detailes;
        }
    }
}
