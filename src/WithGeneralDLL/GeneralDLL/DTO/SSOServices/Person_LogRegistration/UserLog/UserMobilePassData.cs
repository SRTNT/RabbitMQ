// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.UserLog
{
    public class UserMobilePassData
    {
        public string guid { get; set; }
        public string pass1 { get; set; }
        public string pass2 { get; set; }

        public bool IsPolicyOK()
        {
            if (string.IsNullOrEmpty(guid)) return false;
            if (string.IsNullOrEmpty(pass1)) return false;

            if (pass1 != pass2) return false;

            return true;
        }
    }
}
