﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specification;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T>  where T : BaseEntity
	{
		private readonly StoreContext _context;

		public GenericRepository(StoreContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			if (typeof(T) == typeof(Product))
			{
				return (IEnumerable<T>)await _context.Set<Product>().Include(p => p.Brand).ToListAsync();
			}
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<T> GetAsync(int id)
		{
			if (typeof(T) == typeof(Product))
			{
				return await _context.Set<Product>().Where(p => p.Id == id).Include(p => p.Category).FirstOrDefaultAsync() as T;
			}
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec).ToListAsync();
		}
		public async Task<T> GetWiheSpecAsync(ISpecification<T> spec)
		{
			return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec).FirstOrDefaultAsync();
		}
		
	}
}
