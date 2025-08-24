using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.Registration
{
    public class GetJWTFrom1Token
    {
        public string token1Time { get; set; }
        public string baseURL { get; set; }

        public string browser { get; set; }
        public string browserID { get; set; }
    }
}
