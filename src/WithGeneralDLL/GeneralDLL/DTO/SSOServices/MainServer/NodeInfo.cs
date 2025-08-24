using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.MainServer
{
    public class NodeInfo
    {
        public int id { get; set; }

        public Guid code { get; set; }

        public string name { get; set; }
        public string state { get; set; }
        public string city { get; set; }

        public bool isActive { get; set; }
    }
}
