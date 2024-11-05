﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
	public class customerBasket
	{
        public customerBasket(string id)
        {
            Id = id;
            Items = new List<BasketItem>();
        }
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClintSecrit { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
