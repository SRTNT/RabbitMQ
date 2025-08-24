// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.ExpertLog
{
    public class ExpertMobileBase
    {
        public ExpertMobileBase()
        {
            lst_StepData_Child = new List<ExpertMobileStepData>();
        }

        public int id { get; set; }
        public string guid { get; set; }
        public long mobile { get; set; }
        public DateTime dateStart { get; set; } = DateTime.Now;
        public DateTime? dateVerifyOTP { get; set; } = null;
        public DateTime? dateInfo { get; set; } = null;
        public DateTime? dateActivity { get; set; } = null;
        public DateTime? dateLocation { get; set; } = null;
        public DateTime? dateFinished { get; set; } = null;

        public List<ExpertMobileStepData> lst_StepData_Child { get; set; }
    }
}
