using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
	public class AuthServices : IAuthSearvice
	{
		private readonly IConfiguration _configuration;

		public AuthServices(IConfiguration configuration) 
		{
			_configuration = configuration;
		}
		public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			var authClamis = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,user.DisplayName),
				new Claim(ClaimTypes.Email,user.Email),
			};
			var userRoles = await userManager.GetRolesAsync(user);
			foreach(var role in userRoles)
			{
				authClamis.Add(new Claim(ClaimTypes.Role, role));
			}
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"]??string.Empty));
			var token = new JwtSecurityToken(
				audience: _configuration["JWT:ValidAudience"],
				issuer: _configuration["JWT:ValidIssure"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"] ?? "0")),
				claims: authClamis,
				signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature));
			return new JwtSecurityTokenHandler().WriteToken(token);

		}
	}
}
