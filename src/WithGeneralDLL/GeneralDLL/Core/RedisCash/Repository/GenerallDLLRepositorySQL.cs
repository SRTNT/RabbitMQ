using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GeneralDLL.Core.RedisCash.Repository
{
    public class GeneralDLLRepositorySQL : IGeneralDLLRepository
    {
        private readonly GeneralDLLRepository _redis;
        private readonly CacheDBContext _dbContext;

        public GeneralDLLRepositorySQL(IDistributedCache redis, CacheDBContext dbContext)
        {
            _redis = new GeneralDLLRepository(redis);
            _dbContext = dbContext;
        }

        public async Task AddNewData(string category, string key, object value, DateTime expire)
        {
            await _redis.AddNewData(category, key, value, expire);

            #region SQL
            _dbContext.CashBackup.Add(new Domains.CashBackup()
            {
                category = key,
                key = key,
                value = JsonConvert.SerializeObject(value),
                dateExpire = expire,
            });
            await _dbContext.SaveChangesAsync();
            #endregion
        }

        public async Task<object> GetData(string category, string key)
        {
            var dm = await _redis.GetData(category, key);
            if (dm == null)
            {
                var dmSQL = await _dbContext.CashBackup.FirstOrDefaultAsync(x => x.category == category && x.key == key);
                if (dmSQL == null || dmSQL.dateExpire < DateTime.Now)
                    return null;

                await _redis.AddNewData(category, key, JsonConvert.DeserializeObject(dmSQL.value), dmSQL.dateExpire);

                dm = dmSQL.value;
            }

            return dm;
        }
        public async Task<T> GetData<T>(string category, string key)
            where T : class, new()
        {
            var dm = await _redis.GetData<T>(category, key);
            if (dm == null)
            {
                var dmSQL = await _dbContext.CashBackup.FirstOrDefaultAsync(x => x.category == category && x.key == key);
                if (dmSQL == null || dmSQL.dateExpire < DateTime.Now)
                    return null;

                var d = JsonConvert.DeserializeObject<T>(dmSQL.value);
                await _redis.AddNewData(category, key, d, dmSQL.dateExpire);

                dm = d;
            }

            return dm;
        }

        public async Task<bool> RemoveData(string category, string key)
        {
            try
            {
                var dm = await _redis.GetData(category, key);
                if (dm != null)
                {
                    var dmSQL = await _dbContext.CashBackup.FirstOrDefaultAsync(x => x.category == category && x.key == key);
                    if (dmSQL != null)
                        _dbContext.CashBackup.RemoveRange(_dbContext.CashBackup.Where(q => q.id == dmSQL.id));

                    await _redis.RemoveData(category, key);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
