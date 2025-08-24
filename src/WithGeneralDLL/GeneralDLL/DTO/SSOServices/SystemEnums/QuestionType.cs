// Ignore Spelling: DTO Enums App

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum QuestionType
    {
        [Description("لیستی از موارد")]
        ListOfText = 10,
        [Description("جدول")]
        Table = 20,
        [Description("متن بلند")]
        LongText = 0,
        [Description("متن کوتاه")]
        ShortText = 1,
        [Description("تک انتخابی")]
        Radio = 2,
        [Description("چند انتخابی")]
        Checkbox = 3,
        [Description("DropDrawn")]
        DropDrawn = 4,
        [Description("عکس تکی")]
        Pic = 5,
        [Description("فایل یا عکس گروهی")]
        File = 6,
        [Description("عدد صحیح")]
        Number = 7,
        [Description("عدد اعشاری")]
        Double = 8
    }
}