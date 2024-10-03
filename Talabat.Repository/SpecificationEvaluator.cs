using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specification;

namespace Talabat.Repository
{
	internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity>GetQuery(IQueryable<TEntity> innerQuery,ISpecification<TEntity> spec) 
		{
			var query = innerQuery;
			if(spec.Critria is not null)
			{
				query=query.Where(spec.Critria);
			}
			query=spec.Includes.Aggregate(query,(currentQuery,IncludesExpression)=>currentQuery.Include(IncludesExpression));
			return query;
		}
	}
}
