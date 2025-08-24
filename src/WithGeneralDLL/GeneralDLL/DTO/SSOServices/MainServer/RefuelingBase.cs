using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.MainServer
{
    public class RefuelingBase
    {
        public int id { get; set; }

        public PersonInfo person { get; set; }

        public string OTP { get; set; }
        public string dispenserCode { get; set; }
        public double amount { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
    }
}
