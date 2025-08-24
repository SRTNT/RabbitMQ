using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.RedisCash.Repository
{
    public interface IGeneralDLLRepository
    {
        Task AddNewData(string category, string key, object value, DateTime expire);

        Task<object> GetData(string category, string key);
        Task<T> GetData<T>(string category, string key)
            where T : class, new();

        Task<bool> RemoveData(string category, string key);
    }
}
