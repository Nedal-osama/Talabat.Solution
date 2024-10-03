using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Core.Entites;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.ProductSpecificaion;

namespace Talabat.Apis.Controllers
{

	public class ProductsController : BaseApiController
	{

		private readonly IGenericRepository<Product> _productRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> ProductRepo,IMapper mapper) {

			_productRepo = ProductRepo;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct()
		{
			var spec = new ProductWithBrandSpecifiction();
			var products = await _productRepo.GetAllWithSpecAsync(spec);
			return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products));
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandSpecifiction(id);
			var products=await _productRepo.GetWiheSpecAsync(spec);
			if (products == null)
			{
				return NotFound(new {Message="Not Found",StatusCode=404});
			}
			return Ok(_mapper.Map<Product,ProductDto>(products));
		}

	}
}
