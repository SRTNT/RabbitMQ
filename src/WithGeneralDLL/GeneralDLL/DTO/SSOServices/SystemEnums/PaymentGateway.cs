// Ignore Spelling: Enums DTO

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum PaymentGateway
    {
        /// <summary>
        /// زرین پال
        /// </summary>
        [Description("زرین پال")]
        Zarinpal = 0,
        /// <summary>
        /// مسکن
        /// </summary>
        [Description("مسکن")]
        Maskan = 1,
        /// <summary>
        /// مسکن
        /// </summary>
        [Description("ملت")]
        Mellat = 2
    }
}