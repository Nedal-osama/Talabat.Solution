using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
	public class Product:BaseEntity
	{
        public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set;}
		public int BrandId { get; set;}//Fk
		public ProductBrand Brand { get; set; }
        public int caregoryId { get; set; }//Fk
        public ProductCategory Category { get; set; }
    }
}
