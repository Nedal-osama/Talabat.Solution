using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Repository.Contract
{
	public interface IBasketReposatory
	{
		Task<customerBasket?>GetBasketAsync(string basketId);
		Task<customerBasket?> UpdateBasketAsync(customerBasket basket);
		Task<bool> DeleteBasketAsync(string basketId);
	}
}
