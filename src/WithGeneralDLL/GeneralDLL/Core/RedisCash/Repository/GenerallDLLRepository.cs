using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.RedisCash.Repository
{
    public class GeneralDLLRepository : IGeneralDLLRepository
    {
        private readonly IDistributedCache _redis;

        public GeneralDLLRepository(IDistributedCache redis)
        {
            _redis = redis;
        }

        public static string GetRedisKey(string category, string key)
        { return category + "_SRT_" + key; }

        public async Task AddNewData(string category, string key, object value, DateTime expire)
        {
            var k = GetRedisKey(category, key);

            await _redis.SetStringAsync(k,
                                        JsonConvert.SerializeObject(value),
                                        new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expire.TimeOfDay });
        }

        public async Task<object> GetData(string category, string key)
        {
            var k = GetRedisKey(category, key);

            var data = await _redis.GetStringAsync(k);
            if (data is null)
                return null;
            return JsonConvert.DeserializeObject(data);
        }
        public async Task<T> GetData<T>(string category, string key)
            where T : class, new()
        {
            var k = GetRedisKey(category, key);

            var data = await _redis.GetStringAsync(k);
            if (data is null)
                return null;
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<bool> RemoveData(string category, string key)
        {
            try
            {
                var k = GetRedisKey(category, key);
                await _redis.RemoveAsync(k);

                return true;
            }
            catch { return false; }
        }

    }
}
