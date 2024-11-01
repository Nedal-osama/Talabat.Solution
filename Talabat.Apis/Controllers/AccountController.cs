using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Erorrs;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Apis.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IAuthSearvice _authSearvice;

		public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager,IAuthSearvice authSearvice)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_authSearvice = authSearvice;
		}
		[HttpGet("login")]
		public async Task<ActionResult<UserDto>>Login(LoginDto model)
		{
			var user=await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return Unauthorized(new ApiResponse(401));
			}
			var result=await _signInManager.CheckPasswordSignInAsync(user,model.Password,false);
			if(result.Succeeded is false)
			{
				return Unauthorized(new ApiResponse(401));
			}
			return Ok(new UserDto()
			{
				DisplayName=user.DisplayName,
				Email=user.Email,
				Token=await _authSearvice.CreateTokenAsync(user,_userManager)	
			});
		}
		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var user = new AppUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.Email.Split("@")[0],
				PhoneNumber = model.PhoneNumber
			};
			var result = await _userManager.CreateAsync(user,model.Password);
			if(result.Succeeded is false)
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(new UserDto()
			{
				DisplayName=model.DisplayName,
				Email=model.Email,
				Token= await _authSearvice.CreateTokenAsync(user, _userManager)
			});
		}
	}
}
