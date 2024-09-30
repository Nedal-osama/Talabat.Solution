﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Repository.Contract
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
	}
}