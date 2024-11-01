using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specification;

namespace Talabat.Core.Repository.Contract
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T?> GetAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();

		Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);
		Task<T?>GetWiheSpecAsync(ISpecification<T> spec);
		Task<int> GetCountAsync(ISpecification<T> spec);
		Task AddAsync(T entity);
		void UpdateAsync(T entity);
		void DeleteAsync(T entity);
	}
}
