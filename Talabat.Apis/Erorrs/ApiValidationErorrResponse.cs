namespace Talabat.Apis.Erorrs
{
	public class ApiValidationErorrResponse:ApiResponse
	{
        public IEnumerable<string> Erorrs { get; set; }
        public ApiValidationErorrResponse():base(400)
        {
            Erorrs = new List<string>();
        }
    }
}
