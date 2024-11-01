using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.Core.Specification
{
	public class OrderSpecification:BaseSpecification<Order>
	{
        public OrderSpecification(string buyerEmail):base(o=>o.BuyerEmail==buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);


        }
        public OrderSpecification(int orderId,string buyerEmail):base(o=>o.Id==orderId&&o.BuyerEmail==buyerEmail) 
        {
			Includes.Add(o => o.DeliveryMethod);
			Includes.Add(o => o.Items);
		}
    }
}
