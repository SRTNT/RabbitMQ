using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.RedisCash.Domains
{
    public class CashBackup
    {
        public int id { get; set; }

        public string category { get; set; }
        public string key { get; set; }

        public string value1 { get; set; }
        public string value2 { get; set; }
        public string value3 { get; set; }

        public DateTime dateExpire { get; set; } = DateTime.Now.AddDays(1);

        #region Combine Properties
        [JsonIgnore]
        [NotMapped]
        public string value
        {
            get
            { return (value1 ?? "") + (value2 ?? "") + (value3 ?? ""); }
            set
            {
                value1 = value2 = value3 = null;
                if (string.IsNullOrEmpty(value)) return;

                int len = 0;

                len = Math.Min(value.Length, 3990);
                value1 = value.Substring(0, len);
                value = value.Replace(value1, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                value2 = value.Substring(0, len);
                value = value.Replace(value2, "");
                if (string.IsNullOrEmpty(value)) return;

                value3 = value;
            }
        }
        #endregion
    }
}
