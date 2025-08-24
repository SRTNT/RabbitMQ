// Ignore Spelling: DTO App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.Person_LogRegistration.BaseService
{
    public class UserServiceRequest_Data
    {
        public int id { get; set; }
        public bool Request { get; set; }
        public string title { get; set; }
        public string Descriptions { get; set; }
    }
    public class UserServiceRequest
    {
        public string guid { get; set; }
        public List<UserServiceRequest_Data> lstService { get; set; } = new List<UserServiceRequest_Data>();
    }
}
