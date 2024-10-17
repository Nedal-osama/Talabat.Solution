using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.Dtos
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Category { get; set; }
		[Required]
		public int Quantity { get; set; }

	}
}