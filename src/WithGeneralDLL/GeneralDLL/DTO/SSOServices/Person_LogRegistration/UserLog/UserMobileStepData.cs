// Ignore Spelling: Json DTO App

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.UserLog
{
    public class UserMobileStepData
    {
        public int id { get; set; }
        public int id_Base { get; set; }
        public SystemEnums.MobileDataState State { get; set; }
        public string dataInJson { get; set; }

        public T GetData<T>()
        {
            return JsonConvert.DeserializeObject<T>(dataInJson);
        }
    }
}
