// Ignore Spelling: Enums DTO App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum MainPagePosition
    {
        [Description("پستر های وسط - بالا - راست")]
        Middle_TopRight = 0,
        [Description("پستر های وسط - بالا - چپ")]
        Middle_TopLeft = 1,
        [Description("پستر های وسط - پایین - راست")]
        Middle_ButtonRight = 2,
        [Description("پستر های وسط - پایین - چپ")]
        Middle_ButtonLeft = 3,
    }
}