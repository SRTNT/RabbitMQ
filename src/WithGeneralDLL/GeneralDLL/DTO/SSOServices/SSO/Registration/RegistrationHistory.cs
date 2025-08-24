using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.Registration
{
    public class RegistrationHistory
    {
        public int id { get; set; }
        public long mobile { get; set; }

        public string browser { get; set; }
        public string browserID { get; set; }

        public string callbackURL { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
        public DateTime? dateVerifyOTP { get; set; } = null;
    }
}
