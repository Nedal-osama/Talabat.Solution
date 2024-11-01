using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Repository.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class UniteOfWork : IUnitOfWork
	{
		private readonly StoreContext _context;
		private Dictionary<string,GenericRepository<BaseEntity>> _repository;

		public UniteOfWork(StoreContext context) 
		{
			_context = context;
			_repository=new Dictionary<string,GenericRepository<BaseEntity>>();
			
		}
		

		public async Task<int> CompleteAsyns()
		{
	      return	  await	_context.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			 await _context.DisposeAsync();
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var Key=typeof(TEntity).Name;
			if(_repository.ContainsKey(Key))
			{
				var repository = new GenericRepository<TEntity>(_context)as GenericRepository<BaseEntity>;
				_repository.Add(Key, repository);
			}
			return _repository[Key]as IGenericRepository<TEntity>;
			
		}
	}
}
