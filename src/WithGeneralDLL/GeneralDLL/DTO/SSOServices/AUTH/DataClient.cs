// Ignore Spelling: Admin username dto app

using GeneralDLL.DTO.SSOServices.SSO.Users;
using GeneralDLL.DTO.SSOServices.SystemEnums;
using Newtonsoft.Json;
using System;

namespace GeneralDLL.DTO.SSOServices.AUTH
{
    public class DataClientSSO
    {
        public UserInfoAdmin mainData { get; set; }

        public string browser { get; set; }
        public string browserID { get; set; }
    }
}