﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Erorrs;

namespace Talabat.Apis.Controllers
{
	[Route("Erorrs/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErorrsController : ControllerBase
	{
		public ActionResult Erorr(int code)
		{
			if(code == 401)
			{
				return Unauthorized(new ApiResponse(401));
			}
			else if(code ==404)
			{
				return NotFound(new ApiResponse(404));
			}
			else
			{
				return StatusCode(code);
			}
		}
	}
}
