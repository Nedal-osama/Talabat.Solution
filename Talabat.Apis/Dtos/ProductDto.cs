using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entites;

namespace Talabat.Apis.Dtos
{
	public class ProductDto
	{
        public int Id { get; set; }
        [MaxLength(100)]
		public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }
		public int BrandId { get; set; }//Fk
		public string Brand { get; set; }
		public string Category { get; set; }
		public int CategoryId { get; set; }//Fk
	}
}
