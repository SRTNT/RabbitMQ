// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.ExpertLog
{
    public class ExpertMobileActivities
    {
        public string guid { get; set; }
        public List<MobileActivities_Data> Activities { get; set; }

        public bool IsPolicyOK()
        {
            if (string.IsNullOrEmpty(guid)) return false;
            return Activities.All(q => q.IsPolicyOK());
        }
    }
    public class MobileActivities_Data
    {
        public string Area { get; set; }
        public string Part { get; set; }

        public bool IsPolicyOK()
        {
            if (string.IsNullOrEmpty(Area)) return false;
            if (string.IsNullOrEmpty(Part)) return false;

            return true;
        }
    }
}
