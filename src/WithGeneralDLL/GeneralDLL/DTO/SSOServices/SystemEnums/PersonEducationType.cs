// Ignore Spelling: DTO Enums App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum PersonEducationType
    {
        [Description("سیکل")]
        sikl = 0,
        [Description("دیپلم")]
        diplom = 1,
        [Description("فوق دیپلم")]
        fog_diplom = 2,
        [Description("لیسانس")]
        lisanse = 3,
        [Description("فوق لیسانس")]
        fog_lisanse = 4,
        [Description("دکتر")]
        dockter = 5
    }
}
