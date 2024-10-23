using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Order_Aggregrate
{
	public class OrderItem:BaseEntity
	{
		public OrderItem() { }

		public OrderItem(Product product, decimal price, int quantity)
		{
			Product = product;
			Price = price;
			Quantity = quantity;
		}

		public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
