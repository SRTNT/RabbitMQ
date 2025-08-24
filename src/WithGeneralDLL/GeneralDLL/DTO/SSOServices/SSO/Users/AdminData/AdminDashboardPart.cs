// Ignore Spelling: sso dto srt sms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SSO.Users.AdminData
{
    public class AdminDashboardPart
    {
        public int id { get; set; }

        public DashboardPart Dashboard_Part { get; set; }
        public bool Active { get; set; }

        public int idAdminAssign { get; set; }
        public DateTime dateAssign { get; set; } = DateTime.Now;
    }
}
