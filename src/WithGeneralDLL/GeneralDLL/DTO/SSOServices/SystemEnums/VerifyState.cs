// Ignore Spelling: DTO Enums App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum VerifyState
    {
        [Description("درخواست جدید")]
        New = 0,
        [Description("عدم تایید ادمین")]
        Decline_FromAdmin = 20,
        [Description("غیر فعال شده توسط کاربر")]
        Deactive_NewDataFromPerson = 50,
        [Description("تغییر توسط کاربر")]
        Cancel_NewDataFromPerson = 60,
        [Description("رکورد جدیدی فعال گردیده")]
        Cancel_ActiveLastRecord = 61,
        [Description("فعال شده سیستم")]
        Active = 100
    }
}
