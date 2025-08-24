// Ignore Spelling: DTO SSO

using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.SSO
{
    public class basicDataClient
    {
        public long mobile { get; set; }

        public string browserID { get; set; }
        public string browserName { get; set; }

        public string platformName { get; set; }
    }

    public class basicDataAdmin: basicDataClient
    {
        public int id { get; set; }

        public string guid { get; set; } = Guid.NewGuid().ToString();

        public DateTime date { get; set; } = DateTime.Now;
        public DateTime? dateVerifyOTP { get; set; }

        public bool isFinished { get; set; } = false;
    }
}