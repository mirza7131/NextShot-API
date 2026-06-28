using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache
{
    public interface IRedisCacheService
    {
        string Get(string key);
        T Get<T>(string key);
        T Set<T>(string key, T value , int? relativeExpireTimeInHours , int? slidingExpireTimeInHours);
    }
}
