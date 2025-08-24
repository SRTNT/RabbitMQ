// Ignore Spelling: DTO Enums App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum NotificationTabEnum
    {
        [Description("سفارشات")]
        Orders = 0,
        [Description("تیکت ها")]
        Tickets = 1,
        [Description("مالی")]
        Finance = 2,
        [Description("پیام های سیستمی")]
        Messages = 3,
        [Description("SMS")]
        SMS = 4,
    }
}