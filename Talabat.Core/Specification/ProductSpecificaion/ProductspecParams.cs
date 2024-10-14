using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification.ProductSpecificaion
{
	public class ProductspecParams
	{
        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? categoryId { get; set;}
		private int pageSize;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value>10?10:value; }
		}
		public int PageIndex { get; set; } = 1;

    }
}
