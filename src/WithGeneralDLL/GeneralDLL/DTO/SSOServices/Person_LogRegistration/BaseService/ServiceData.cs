// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.BaseService
{
    public class ServiceData
    {
        public int id { get; set; }
        public int idService { get; set; }
        public string Name { get; set; }
        public string minimal { get; set; }
        public string mainPicture { get; set; }

        public bool PolicyIsOK()
        {
            if (idService <= 0) return false;
            if (string.IsNullOrEmpty(Name)) return false;
            if (string.IsNullOrEmpty(mainPicture)) return false;
            if (string.IsNullOrEmpty(minimal)) return false;

            return true;
        }
    }
}
