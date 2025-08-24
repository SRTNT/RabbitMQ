// Ignore Spelling: DTO SRT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.GeneralFunctions.Persons
{
    public class MobileCheck
    {
        public static bool isMobileInCorrectStructure(long mobile)
        {
            if (mobile.ToString().Length != 10)
                return false;

            return true;
        }

        public static string ShowMobile(long mobile)
        { return mobile.ToString("###-###-####"); }
    }
}
