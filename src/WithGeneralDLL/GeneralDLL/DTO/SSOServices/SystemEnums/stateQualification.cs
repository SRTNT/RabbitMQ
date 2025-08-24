// Ignore Spelling: DTO Enums

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum stateQualification
    {
        [Description("درخواست جدید")]
        newRequest = 0,
        [Description("در انتظار")]
        waiting = 1,
        [Description("عدم تایید")]
        rejected = 2,
        [Description("تایید شده")]
        accepted = 3
    }
}
