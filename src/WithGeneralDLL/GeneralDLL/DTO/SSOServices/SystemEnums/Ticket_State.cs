// Ignore Spelling: DTO Enums

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum Ticket_State
    {
        [Description("ادمین جواب داده")]
        HasAnswer = 0,
        [Description("منتظر جواب ادمین")]
        NoAnswer = 1,
        [Description("پایان")]
        Finished = 2
    }
}