// Ignore Spelling: DTO SRT

using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeneralDLL.Domain
{
    public abstract class SearchInput
    {
        [JsonIgnore]
        private int _pageIndex = 1;
        public int pageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value <= 0 ? 1 : value;
        }

        [JsonIgnore]
        private int _pageSize = 20;
        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = value <= 0 ? 1 : value;
        }

        public IQueryable<T> SetPagination<T>(IQueryable<T> data)
        {
            var skipt = (pageIndex - 1) * pageSize;
            return data.Skip(skipt).Take(pageSize);
        }
    }
}