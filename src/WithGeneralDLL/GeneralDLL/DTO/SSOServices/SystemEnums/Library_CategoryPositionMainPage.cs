// Ignore Spelling: Enums

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum Library_CategoryPositionMainPage
    {
        [Description("عدم نمایش صفحه اصلی")]
        None = 0,
        [Description("منطقه 1 صفحه اصلی")]
        Status1 = 1,
        [Description("منطقه 2 صفحه اصلی")]
        Status2 = 2,
        [Description("منطقه 3 صفحه اصلی")]
        Status3 = 3,
        [Description("دسته اصلی بالای صفحه اصلی")]
        TopMainPage = 10,
        [Description("کنار منوی اصلی")]
        SideOfTopMenu = 11
    }
}