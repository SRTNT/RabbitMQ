// Ignore Spelling: DTO Enums App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum TypeMenuState
    {
        [Description("منوی زیر عکس اصلی")]
        SubMenu = 1,
        [Description("در قسمت وسط سایت")]
        MiddleSite = 2,
        [Description("NONE")]
        NONE = 100,
    }
}
