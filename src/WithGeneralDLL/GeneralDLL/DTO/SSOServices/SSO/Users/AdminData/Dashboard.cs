// Ignore Spelling: sso dto srt sms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.Users.AdminData
{
    public class Dashboard
    {
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string path { get; set; }
        public string description { get; set; }
        public string picture { get; set; }
        public int priority { get; set; }

        public List<DashboardPart> lst_Part_Child { get; set; } = new List<DashboardPart>();
    }
}