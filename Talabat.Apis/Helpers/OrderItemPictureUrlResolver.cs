﻿using AutoMapper;
using AutoMapper.Execution;
using Talabat.Apis.Dtos;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.Apis.Helpers
{
	public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.Product.ProductUrl))
			{
				return $"{_configuration["ApiBaseUrl"]}/{source.Product.ProductUrl}";
			}
			return string.Empty;
		}
		}
	} 
