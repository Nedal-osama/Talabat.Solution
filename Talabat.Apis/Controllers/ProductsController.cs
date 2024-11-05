using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Erorrs;
using Talabat.Apis.Helpers;
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
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProduct([FromQuery]ProductspecParams specParams)
		{
			var spec = new ProductWithBrandSpecifiction(specParams);
			var products = await _productRepo.GetAllWithSpecAsync(spec);
			var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>((IReadOnlyList<Product>)products);
			var countSpec = new ProductWithFilterationForCountSpec(specParams);
			var count=await _productRepo.GetCountAsync(countSpec);
			return Ok(new Pagination<ProductDto>(specParams.PageSize, specParams.PageIndex,count, Data));
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
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
		{
			var brands= await _brandsRepo.GetAllAsync();
			return Ok(brands);
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetCategory()
		{
			var categories = await _categoryRepo.GetAllAsync();
			return Ok(categories);
		}
	}
}
