﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Order_Aggregrate
{
	public class Product
	{
        public Product()
        {
            
        }
        public Product(int productId, string productName, string productUrl)
		{
			ProductId = productId;
			ProductName = productName;
			ProductUrl = productUrl;
		}

		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductUrl { get; set; }
	}
}
