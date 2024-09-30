using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Repository.Contract;

namespace Talabat.Apis.Controllers
{
	
	public class ProductsController : BaseApiController
	{
		
		private readonly IGenericRepository<Product> _productRepo;

		public ProductsController(IGenericRepository<Product> ProductRepo) {
			
			_productRepo = ProductRepo;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
		{
	     	var products=await _productRepo.GetAllAsync();
			return Ok(products);
		}

	}
}
