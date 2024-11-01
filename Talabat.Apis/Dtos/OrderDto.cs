using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entites.Identity;

namespace Talabat.Apis.Dtos
{
	public class OrderDto
	{
		[Required]
        public string BuyerEmail { get; set; }
		[Required]
		public string BasketId { get; set;}
		[Required]
        public int deliveryMethodId { get; set; }
		public AddressDto ShippingAddress { get; set; }
    }
}
