using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SystemEnums
{
    public enum Service_State
    {
        /// <summary>
        /// درخواست اولیه
        /// </summary>
        [Description("درخواست اولیه")]
        Request = 0,
        /// <summary>
        /// در حال بررسی
        /// </summary>
        [Description("در حال بررسی")]
        Checking = 1,
        /// <summary>
        /// تایید شده
        /// </summary>
        [Description("تایید شده")]
        Accepted = 2,
        /// <summary>
        /// رد شده
        /// </summary>
        [Description("رد شده")]
        Denied = 3,
        /// <summary>
        /// فعال
        /// </summary>
        [Description("فعال")]
        Enable = 4,
        /// <summary>
        /// غیر فعال
        /// </summary>
        [Description("غیر فعال")]
        Disable = 5,
    }
}
