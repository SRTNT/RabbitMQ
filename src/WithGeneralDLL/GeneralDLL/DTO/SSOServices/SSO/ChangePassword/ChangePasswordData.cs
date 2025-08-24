using GeneralDLL.DTO.SSOServices.SSO.Users.ClientData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.ChangePassword
{
    public class ChangePasswordData
    {
        public int id { get; set; }
        public ClientInfoAdmin PersonInfo { get; set; }

        public string browser { get; set; }
        public string browserID { get; set; }
        public string callbackURL { get; set; }

        public int id_Service { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
        public DateTime? dateVerifyOTP { get; set; } = null;
    }
}