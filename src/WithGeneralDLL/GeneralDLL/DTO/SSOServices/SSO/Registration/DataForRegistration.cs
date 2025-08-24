// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.Registration
{
    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string emoji { get; set; }
        public string unicode { get; set; }
        public string image { get; set; }
        public string dial_code { get; set; }
        public string dial_code_without_plus { get; set; }
    }

    public class DataForRegistration
    {
        public string browser { get; set; }
        public string browserID { get; set; }

        public string callbackURL { get; set; }

        public long mobile { get; set; }
        public Country country { get; set; }

        public GeneralDLL.DTO.SSOServices.SystemEnums.BotType sendOTOType { get; set; } = SystemEnums.BotType.SMS;
    }

    public class ConfirmOTP
    {
        public string browser { get; set; }
        public string browserID { get; set; }

        public long mobile { get; set; }
        public Country country { get; set; }

        public string otp { get; set; }
        public GeneralDLL.DTO.SSOServices.SystemEnums.OTPSenderType otpType { get; set; }
    }

    public class ConfirmChangePassword
    {
        public string browser { get; set; }
        public string browserID { get; set; }

        public long mobile { get; set; }
        public Country country { get; set; }

        public string otp { get; set; }
        public string password { get; set; }

        public GeneralDLL.DTO.SSOServices.SystemEnums.OTPSenderType otpType { get; set; }
    }
    public class CheckPassword
    {
        public string browser { get; set; }
        public string browserID { get; set; }

        public long mobile { get; set; }
        public string password { get; set; }
        public string callbackURL { get; set; }
    }
}
