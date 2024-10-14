using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification.ProductSpecificaion
{
	public class ProductWithBrandSpecifiction:BaseSpecification<Product>
	{
		public ProductWithBrandSpecifiction(ProductspecParams spec):base(p=>
			(!spec.brandId.HasValue ||p.BrandId==spec.brandId.Value)&&
		    (!spec.categoryId.HasValue || p.CategoryId == spec.categoryId.Value)
			)
		{
			Includes.Add(p=>p.Brand);
			Includes.Add(p=>p.Category);
			if (!string.IsNullOrEmpty(spec.sort))
			{
				switch(spec.sort)
				{
					case  "priceAsc":
						AddOrderBy(p=>p.Price);
						break;
					case "priceDesc":
						AddOrderBy(p=>p.Price);
						break;
                    default:
						AddOrderBy(p=>p.Name);
						break;
				}
			}
			else
			{
				AddOrderBy(p=>p.Name);
			}
			ApplyPagination((spec.PageIndex-1)*spec.PageSize, spec.PageSize);
		}
        public ProductWithBrandSpecifiction(int id):base(p=>p.Id==id)
        {
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);

		}
    }
}
