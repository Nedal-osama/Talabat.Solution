using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repository.Contract;


namespace Talabat.Repository
{
	public class BasketReposatory:IBasketReposatory
	{
        private readonly IDatabase _database;
        public BasketReposatory(IConnectionMultiplexer redis)
        {
            _database=redis.GetDatabase();
        }

		public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);
		}

		public async Task<customerBasket?> GetBasketAsync(string basketId)
		{
			var basket=await _database.StringGetAsync(basketId);
			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<customerBasket>(basket);
		}

		public async Task<customerBasket?> UpdateBasketAsync(customerBasket basket)
		{
			var createdOrUpdatedBasket=await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));

			if (createdOrUpdatedBasket is false) return null;
			return await GetBasketAsync(basket.Id);
		}
	}
}
