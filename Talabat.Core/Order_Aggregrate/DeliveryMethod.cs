using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Order_Aggregrate
{
	public class DeliveryMethod :BaseEntity
	{
		public DeliveryMethod(string sortName, string description, string deliveryTime, decimal cost)
		{
			SortName = sortName;
			Description = description;
			DeliveryTime = deliveryTime;
			Cost = cost;
		}
		public DeliveryMethod() { }

		public string SortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
