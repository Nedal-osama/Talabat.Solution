using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		public async static Task SeedAsync(StoreContext _dbcontext)
		{

			var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
			var brands=JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

			if(brands.Count() > 0)
			{
				if(_dbcontext.ProductBrands.Count() == 0)
				{
					foreach (var brand in brands)
					{
						_dbcontext.Set<ProductBrand>().Add(brand);
					}
					await _dbcontext.SaveChangesAsync();
				}
				
			}

			var categoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
			var category = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);

			if (category.Count() > 0)
			{
				if (_dbcontext.productCategories.Count() == 0)
				{
					foreach (var categorys in category)
					{
						_dbcontext.Set<ProductCategory>().Add(categorys);
					}
					await _dbcontext.SaveChangesAsync();
				}

			}
			var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
			var product = JsonSerializer.Deserialize<List<Product>>(ProductData);

			if (product.Count() > 0)
			{
				if (_dbcontext.product.Count() == 0)
				{
					foreach (var products in product)
					{
						_dbcontext.Set<Product>().Add(products);
					}
					await _dbcontext.SaveChangesAsync();
				}

			}



		}
	}
}
