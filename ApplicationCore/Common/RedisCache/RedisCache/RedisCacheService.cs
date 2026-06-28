using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }


        public string? Get(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                return value.ToString();
            }

            return null;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                return JsonSerializer.Deserialize<T>(value)!;
            }

            return default!;
        }

        public T Set<T>(string key, T value , int? absoluteExpireTimeInHours , int? slidingExpireTimeInHours)
        {
            var timeOut = new DistributedCacheEntryOptions();

            //will expire the entry after a set amount of time
            if (absoluteExpireTimeInHours != null)
                timeOut.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(absoluteExpireTimeInHours ?? 0);
                

            //will expire the entry if it hasn't been accessed in a set amount of time
            if (slidingExpireTimeInHours != null)
                timeOut.SlidingExpiration = TimeSpan.FromHours(slidingExpireTimeInHours ?? 0);

            if (absoluteExpireTimeInHours != null || slidingExpireTimeInHours != null)
                _cache.SetString(key, JsonSerializer.Serialize(value), timeOut);
            else
                _cache.SetString(key, JsonSerializer.Serialize(value));
            return value;
        }
    }
}
