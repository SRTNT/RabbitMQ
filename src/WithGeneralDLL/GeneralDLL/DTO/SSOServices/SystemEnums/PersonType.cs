// Ignore Spelling: DTO Enums App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum PersonType
    {
        [Description("حقیقی")]
        isPerson,
        [Description("حقوقی")]
        isCompany
    }
}
