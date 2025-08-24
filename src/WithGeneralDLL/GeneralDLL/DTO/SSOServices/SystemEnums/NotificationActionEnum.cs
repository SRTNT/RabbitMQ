// Ignore Spelling: DTO Enums App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum NotificationActionEnum
    {
        [Description("باز کردن فایل")]
        OpenResource = 0,
        [Description("مرتبط با تیکت")]
        OpenTicket = 10,
        [Description("کیف پول-پرداخت")]
        Wallet_Payment = 20,
        [Description("نماینده من")]
        ExpertDeputy = 30,
        [Description("ثبت سفارش - جدید")]
        SAR_New = 40,
        [Description("ثبت سفارش - در حال مناقصه")]
        SAR_InGetPrice = 41,
        [Description("ثبت سفارش - فعال")]
        SAR_Active = 42,
        [Description("ثبت سفارش - کنسل شده")]
        SAR_Cancel = 43,
        [Description("ثبت سفارش - انجام نشده")]
        SAR_Reject = 44,
        [Description("ثبت سفارش - اتمام یافته")]
        SAR_Finished = 45,
        [Description("باز کردن Modal")]
        OpenDialog = 50,
        [Description("باز کردن urlها")]
        OpenUrl = 60,
        [Description("باز کردن application")]
        OpenAPP = 70,
        [Description("بدون action")]
        None = 200
    }
}