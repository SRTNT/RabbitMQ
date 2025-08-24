// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.ExpertLog
{
    public class ExpertMobileLocations
    {
        public string guid { get; set; }
        /// <summary>
        /// محل سکونت
        /// </summary>
        public ExpertMobileLocations_Data residence { get; set; }

        public List<ExpertMobileLocations_Data> workplaces { get; set; }

        public bool IsPolicyOK()
        {
            if (string.IsNullOrEmpty(guid)) return false;
            if (!residence.Equals(residence)) return false;

            return workplaces.All(q => q.IsPolicyOK());
        }
    }
    public class ExpertMobileLocations_Data
    {
        /// <summary>
        /// استان
        /// </summary>
        public string State { get; set; }

        public string City { get; set; }

        /// <summary>
        /// منطقه
        /// </summary>
        public string Area { get; set; }

        public bool IsPolicyOK()
        {
            if (string.IsNullOrEmpty(Area)) return false;
            if (string.IsNullOrEmpty(City)) return false;
            if (string.IsNullOrEmpty(State)) return false;

            return true;
        }
    }
}
