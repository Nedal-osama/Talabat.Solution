using AutoMapper;
using Talabat.Apis.Dtos;
using Talabat.Core.Entites;
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
		}
    }
}
