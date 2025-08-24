// Ignore Spelling: DTO SRT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.GeneralFunctions
{
    public class Wallets
    {
        public static long GetCodeTransaction()
        {
            var d = DateTime.Now;
            return long.Parse(d.Year.ToString() + d.Month.ToString() + d.Day.ToString() + d.Hour.ToString() + d.Minute.ToString() + d.Second.ToString());
        }
    }
}
