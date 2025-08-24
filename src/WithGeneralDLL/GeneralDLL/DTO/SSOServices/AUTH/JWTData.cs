// Ignore Spelling: Admin username dto app

using GeneralDLL.DTO.SSOServices.SystemEnums;
using Newtonsoft.Json;
using System;

namespace GeneralDLL.DTO.SSOServices.AUTH
{
    public class JWTData
    {
        public string jwtBrowser { get; set; }
        public string jwtSSO { get; set; }
    }
}