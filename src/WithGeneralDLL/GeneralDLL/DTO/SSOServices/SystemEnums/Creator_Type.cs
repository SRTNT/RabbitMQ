// Ignore Spelling: DTO Enums

using System.ComponentModel;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum Creator_Type
    {
        /// <summary>
        /// نویسنده
        /// </summary>
        [Description("نویسنده")]
        Writer = 0,
        /// <summary>
        /// مؤلف
        /// </summary>
        [Description("مؤلف")]
        Moalef = 1,
        /// <summary>
        /// گردآورنده
        /// </summary>
        [Description("گردآورنده")]
        GerdAvarandeh = 2,
        /// <summary>
        /// مترجم
        /// </summary>
        [Description("مترجم")]
        Translater = 3,
        /// <summary>
        /// تصویرگر
        /// </summary>
        [Description("تصویرگر")]
        Tasvirgar = 4,
        /// <summary>
        /// سایر پدیدآورندگان
        /// </summary>
        [Description("سایر پدیدآورندگان")]
        Others = 5,
    }
}
