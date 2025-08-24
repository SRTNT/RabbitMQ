// Ignore Spelling: DTO Enums App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum RequestState
    {
        /// <summary>
        /// مرحله 1
        /// </summary>
        [Description("اختصاص به متخصص")]
        InAssignToExpert = 0,
        /// <summary>
        /// مرحله 2
        /// </summary>
        [Description("قیمت دهی")]
        InExpertEnterPrice = 10,
        /// <summary>
        /// مرحله 2-3
        /// </summary>
        [Description("قیمت دهی - انتخاب متخصص")]
        InExpertEnterPrice_UserAcceptExpert = 20,
        /// <summary>
        ///  مرحله 2-3 نیست
        /// </summary>
        [Description("IDLE")]
        Idle = 25,
        /// <summary>
        /// مرحله 3
        /// </summary>
        [Description("انتخاب متخصص")]
        InUserAcceptExpert = 30,
        /// <summary>
        /// مرحله 4
        /// </summary>
        [Description("در حال اجرا")]
        InProgress = 40,
        /// <summary>
        /// مرحله 4
        /// </summary>
        [Description("کمیسیون متخصص")]
        InProgress_GetPaymentCommissionAtFirst = 43,
        /// <summary>
        /// مرحله 5
        /// </summary>
        [Description("اتمام کامل پروژه")]
        Finished = 50,
        [Description("اتمام توسط ادمین")]
        FinishedByAdmin = 55,
        [Description("اتمام - زمان تمام شده")]
        FinishedNotInProgress = 60,
        [Description("اتمام - متخصص پیدا نشد")]
        FinishedNotAssignedToExpert = 65,
        [Description("کنسل - متخصص")]
        CancelWithExpert = 70,
        [Description("کنسل - کاربر")]
        CancelWithUser = 80,
        /// <summary>
        /// مشکل بین کارفرما و متخصص - ادمین باید تمام کند
        /// </summary>
        [Description("مشکل بین طرفین")]
        InError = 200
    }
}
