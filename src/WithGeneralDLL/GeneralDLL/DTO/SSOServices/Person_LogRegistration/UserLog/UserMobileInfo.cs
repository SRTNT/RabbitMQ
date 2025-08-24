// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.UserLog
{
    public class UserMobileInfo
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string family { get; set; }

        public bool IsPolicyOK()
        {
            if (string.IsNullOrEmpty(guid)) return false;
            if (string.IsNullOrEmpty(name)) return false;
            if (string.IsNullOrEmpty(family)) return false;

            return true;
        }
    }
}
