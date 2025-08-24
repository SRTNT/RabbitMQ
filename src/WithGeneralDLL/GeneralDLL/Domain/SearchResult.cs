// Ignore Spelling: DTO SRT

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Domain
{
    public abstract class SearchResult<T>
    {
        public int count { get; set; }
        public List<T> lstData { get; set; } = new List<T>();

        public virtual List<TOut> GetShowData<TOut>(Func<T, TOut> func)
        {
            return lstData.Select(x => func(x)).ToList();
        }

        public override string ToString()
        {
            return $"Count: {count} - Type: {typeof(T)}";
        }
    }
}