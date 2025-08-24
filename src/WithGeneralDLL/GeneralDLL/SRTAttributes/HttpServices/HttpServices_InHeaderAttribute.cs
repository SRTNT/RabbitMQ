// Ignore Spelling: SRT

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDLL.SRTAttributes.HttpServices
{
    public class HttpServices_InHeaderAttribute : Attribute
    {
        public HttpServices_InHeaderAttribute(string name)
        {
            this.name = name;
        }

        public string name { get; set; }
    }
}
