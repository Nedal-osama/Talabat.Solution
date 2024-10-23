using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Order_Aggregrate
{
	public class Order : BaseEntity
	{
		public Order(string buyerEmail, DateTimeOffset orderDate, OrderStatus status, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
		{
			BuyerEmail = buyerEmail;
			OrderDate = orderDate;
			Status = status;
			ShippingAddress = shippingAddress;
			DeliveryMethod = deliveryMethod;
			Items = items;
			SubTotal = subTotal;
		}

		public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            =>SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; }

    }
}
