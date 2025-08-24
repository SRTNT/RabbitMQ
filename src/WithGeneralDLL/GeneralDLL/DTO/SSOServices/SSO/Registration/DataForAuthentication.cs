// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.Registration
{
    public class DataForAuthentication
    {
        public string browser { get; set; }
        public string browserID { get; set; }

        public string callbackURL { get; set; }

        public string token { get; set; }
    }
}
