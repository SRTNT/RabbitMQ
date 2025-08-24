// Ignore Spelling: DTO Enums App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum NotificationTypeTemplate
    {
        [Description("عضویت - کاربر")]
        registration_user = 0,
        [Description("عضویت - متخصص")]
        registration_expert = 1,

        [Description("متخصص - فعال ادمین")]
        ExpertActive_Admin = 5,
        [Description("متخصص - غیرفعال ادمین")]
        ExpertDeActive_Admin = 6,

        [Description("اطلاعات کاربری")]
        ExpertInfo_NewRequest = 10,
        [Description("اطلاعات کاربری - تایید شاهکار")]
        ExpertInfo_AcceptShakharVerify = 11,
        [Description("اطلاعات کاربری - عدم تایید شاهکار")]
        ExpertInfo_RejectShakharVerify = 12,
        [Description("اطلاعات کاربری - تایید ادمین")]
        ExpertInfo_AcceptAdminVerify = 13,
        [Description("اطلاعات کاربری - عدم تایید ادمین")]
        ExpertInfo_RejectAdminVerify = 14,

        [Description("اطلاعات بانکی - تایید شاهکار")]
        ExpertBank_AcceptShakharVerify = 20,

        [Description("آدرس - درخواست جدید")]
        ExpertAddress_NewRequest = 30,
        [Description("آدرس - تایید ادمین")]
        ExpertAddress_AcceptAdminVerify = 31,
        [Description("آدرس - عدم تایید ادمین")]
        ExpertAddress_RejectAdminVerify = 32,

        [Description("حقوقی - درخواست جدید")]
        ExpertCompony_NewRequest = 40,
        [Description("حقوقی - تایید ادمین")]
        ExpertCompony_AcceptAdminVerify = 41,
        [Description("حقوقی - عدم تایید ادمین")]
        ExpertCompony_RejectAdminVerify = 42,

        [Description("نماینده - درخواست جدید")]
        ExpertDeputy_NewRequest = 50,
        [Description("نماینده - تایید ادمین")]
        ExpertDeputy_AcceptAdminVerify = 51,
        [Description("نماینده - عدم تایید ادمین")]
        ExpertDeputy_RejectAdminVerify = 52,

        [Description("کیف پول - واریز")]
        ExpertWallet_Input = 60,
        [Description("کیف پول - برداشت")]
        ExpertWallet_Output = 61,

        [Description("صلاحیت - منتظر جواب سوالات")]
        Qualification_NewRequest = 200,
        [Description("صلاحیت - منتظر تایید ارمین")]
        Qualification_WaitingAdmin = 201,
        [Description("صلاحیت - تایید ادمین")]
        Qualification_AcceptAdminVerify = 202,
        [Description("صلاحیت - عدم تایید ادمین")]
        Qualification_RejectAdminVerify = 203,

        [Description("ثبت سفارش")]
        SAR = 210,

        [Description("غیره")]
        None = 249,
    }
}