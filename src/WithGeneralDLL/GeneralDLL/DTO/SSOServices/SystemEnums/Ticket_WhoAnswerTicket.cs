// Ignore Spelling: DTO Enums

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum Ticket_WhoAnswerTicket
    {
        [Description("جدید")]
        None = 0,
        [Description("شخصی")]
        AdminOwn = 1,
        [Description("همکاران")]
        Others = 2
    }
}