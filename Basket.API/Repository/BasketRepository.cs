using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            await _redisCache.RemoveAsync(userName);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null!;
            }
            var basket = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return new ShoppingCart(userName);
            }
            return JsonSerializer.Deserialize<ShoppingCart>(basket)!;
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            if (basket == null)
            {
                return null!;
            }
            await _redisCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
