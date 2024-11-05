using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		public async static Task SeedAsync(StoreContext _dbcontext)
		{

			var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
			var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);



			if (_dbcontext.ProductBrands.Count() == 0 && brands?.Count > 0)
			{
				foreach (var brand in brands)
				{
					_dbcontext.ProductBrands.Add(brand);
				}
				await _dbcontext.SaveChangesAsync();
			}

			var categoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
			var category = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);

			if (_dbcontext.productCategories.Count() == 0 && category?.Count > 0)
			{
				foreach (var categories in category)
				{
					_dbcontext.productCategories.Add(categories);
				}
				await _dbcontext.SaveChangesAsync();
			}
			var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
			var product = JsonSerializer.Deserialize<List<Product>>(ProductData);

			if (_dbcontext.Products.Count() == 0&& product?.Count>0) { 
				if (product?.Count>0)
				{
					foreach (var products in product)
					{
						_dbcontext.Set<Product>().Add(products);
					}
					await _dbcontext.SaveChangesAsync();
				}
	       	}

			var deliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
			var deliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);


			if (_dbcontext.DeliveryMethods.Count() == 0 && deliveryMethod?.Count > 0)
			{
				foreach (var method in deliveryMethod)
				{
					_dbcontext.DeliveryMethods.Add(method);
				}
				await _dbcontext.SaveChangesAsync();
			}

		}
	}
}
