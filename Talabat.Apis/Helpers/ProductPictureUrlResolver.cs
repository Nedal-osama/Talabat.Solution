﻿using AutoMapper;
using Talabat.Apis.Dtos;
using Talabat.Core.Entites;

namespace Talabat.Apis.Helpers
{
	public class ProductPictureUrlResolver:IValueResolver<Product,ProductDto,string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration) {
			_configuration = configuration;
		}
		public string Resolve(Product source,ProductDto destination,string destMember,ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
			}
			return string.Empty ;
		}
	}
}
