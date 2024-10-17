using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification.ProductSpecificaion
{
	public class ProductWithFilterationForCountSpec:BaseSpecification<Product>
	{
		public ProductWithFilterationForCountSpec(ProductspecParams spec) : base(p =>
			(!spec.brandId.HasValue || p.BrandId == spec.brandId.Value) &&
			(!spec.categoryId.HasValue || p.CategoryId == spec.categoryId.Value)
			) 
		{ 

		}
	}
}
