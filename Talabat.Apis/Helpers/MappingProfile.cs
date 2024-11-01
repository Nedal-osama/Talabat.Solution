using AutoMapper;
using Talabat.Apis.Dtos;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Order_Aggregrate;
using static System.Net.WebRequestMethods;

namespace Talabat.Apis.Helpers
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
        {

			CreateMap<Product, ProductDto>().ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
											.ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
											 .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
			CreateMap<CustomerBasketDto, customerBasket>();
			CreateMap<BasketItemDto,BasketItem>();
			CreateMap<AddressDto, Core.Entites.Identity.Address>();
			CreateMap<Order, OrderToReturnDto>().ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.SortName)).ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));
			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
				.ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
				.ForMember(d=>d.ProductUrl,o=>o.MapFrom(o=>o.Product.ProductUrl))
				.ForMember(d => d.ProductUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());


		}
    }
}
