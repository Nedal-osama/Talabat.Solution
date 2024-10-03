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
		public ProductWithBrandSpecifiction():base()
		{
			Includes.Add(p=>p.Brand);
			Includes.Add(p=>p.Category);
		}
        public ProductWithBrandSpecifiction(int id):base(p=>p.Id==id)
        {
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);

		}
    }
}
