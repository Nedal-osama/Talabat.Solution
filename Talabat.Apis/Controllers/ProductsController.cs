using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Erorrs;
using Talabat.Core.Entites;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.ProductSpecificaion;

namespace Talabat.Apis.Controllers
{

	public class ProductsController : BaseApiController
	{

		private readonly IGenericRepository<Product> _productRepo;
		private readonly IGenericRepository<ProductBrand> _brandsRepo;
		private readonly IGenericRepository<ProductCategory> _categoryRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> ProductRepo, IGenericRepository<ProductBrand> brandsRepo, IGenericRepository<ProductCategory> categoryRepo, IMapper mapper) {

			_productRepo = ProductRepo;
			_brandsRepo = brandsRepo;
			_categoryRepo = categoryRepo;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct()
		{
			var spec = new ProductWithBrandSpecifiction();
			var products = await _productRepo.GetAllWithSpecAsync(spec);
			return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products));
		}

		[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandSpecifiction(id);
			var products = await _productRepo.GetWiheSpecAsync(spec);
			if (products == null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok(_mapper.Map<Product, ProductDto>(products));
		}

		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrand()
		{
			var brands= await _brandsRepo.GetAllAsync();
			return Ok(brands);
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IEnumerable<ProductBrand>>> GetCategory()
		{
			var categories = await _categoryRepo.GetAllAsync();
			return Ok(categories);
		}
	}
}
