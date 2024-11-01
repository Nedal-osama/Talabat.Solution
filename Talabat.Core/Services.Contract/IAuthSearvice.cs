﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;

namespace Talabat.Core.Services.Contract
{
	public interface IAuthSearvice
	{
		Task<string>CreateTokenAsync(AppUser user,UserManager<AppUser>userManager);
	}
}
