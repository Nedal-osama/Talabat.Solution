using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Erorrs;
using Talabat.Repository.Data;

namespace Talabat.Apis.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuggyController : ControllerBase
	{
		private readonly StoreContext _storeContext;

		public BuggyController(StoreContext storeContext)
        {
			_storeContext = storeContext;
		}
		[HttpGet("notfound")] //    api/Buggy/notfound
		public ActionResult GetNotFoundRequest()
		{
			var product = _storeContext.Products.Find(100);
			if(product == null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok(product);
		}
		[HttpGet("servererror")]
		public ActionResult GetServerErorr()
		{
			var product= _storeContext.Products.Find(200);
			var productDto = product.ToString();
			return Ok(productDto);
		}
		[HttpGet("badrequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}
		[HttpGet("unauthorized")]
		public ActionResult GetUnAuthorized()
		{
			return Unauthorized(new ApiResponse(401));
		}
		[HttpGet("badrequest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}
    }
}
