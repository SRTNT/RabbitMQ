// Ignore Spelling: DTO SSO

using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.SSO
{
    public class ChangePassClient
    {
        public long mobile { get; set; }

        public string browserID { get; set; }
        public string browserName { get; set; }

        public string platformName { get; set; }

        public string pass1 { get; set; }
        public string pass2 { get; set; }
    }
}