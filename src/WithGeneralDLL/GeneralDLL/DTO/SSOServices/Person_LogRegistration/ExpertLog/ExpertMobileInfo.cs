// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.ExpertLog
{
    public class ExpertMobileInfo
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string pass1 { get; set; }
        public string pass2 { get; set; }

        public bool IsPolicyOK()
        {
            if (string.IsNullOrEmpty(guid)) return false;
            if (string.IsNullOrEmpty(name)) return false;
            if (string.IsNullOrEmpty(family)) return false;
            if (string.IsNullOrEmpty(pass1)) return false;

            return pass1 == pass2;
        }
    }
}
