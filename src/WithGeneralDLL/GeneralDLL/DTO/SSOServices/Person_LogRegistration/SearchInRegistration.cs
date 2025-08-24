// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration
{
    public class SearchInRegistration : Domain.SearchInput
    {
        public string subMobile { get; set; }
        public string subNameFamily { get; set; }

        public SystemEnums.MobileDataState_Search? State { get; set; } = null;
        public DateTime dateStart { get; set; } = DateTime.Now;
        public DateTime dateEnd { get; set; } = DateTime.Now;
    }
}
