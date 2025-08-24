// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.UserLog
{
    public class UserMobileBase
    {
        public UserMobileBase()
        {
            lst_StepData_Child = new List<UserMobileStepData>();
        }

        public int id { get; set; }
        public string guid { get; set; }
        public long mobile { get; set; }
        public DateTime dateStart { get; set; } = DateTime.Now;
        public DateTime? dateVerifyOTP { get; set; } = null;
        public DateTime? dateInfo { get; set; } = null;
        public DateTime? datePass { get; set; } = null;
        public DateTime? dateActivity { get; set; } = null;
        public DateTime? dateFinished { get; set; } = null;

        public List<UserMobileStepData> lst_StepData_Child { get; set; }
    }
}
