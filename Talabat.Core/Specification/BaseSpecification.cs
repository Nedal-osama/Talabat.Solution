using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification
{
	public class BaseSpecification<T>:ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Critria { get; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; }=  new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set ; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
		public int Skip { get ; set ; }
		public int Take { get ; set ; }
		public bool IsPagintionEnable { get; set; }

		public BaseSpecification()
        {
			

		}
        public BaseSpecification(Expression<Func<T,bool>>critriaExpresion)
        {
            Critria=critriaExpresion;
         
        }

		public void AddOrderBy(Expression<Func<T, object>> orderByExperation)
		{
			OrderBy=orderByExperation;
		}
		public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExperation)
		{
		    OrderByDesc=orderByDescExperation;
		}
		public void ApplyPagination(int skip,int take)
		{
			IsPagintionEnable = true;
			Skip=skip;
			Take=take;
		}

	}
}
