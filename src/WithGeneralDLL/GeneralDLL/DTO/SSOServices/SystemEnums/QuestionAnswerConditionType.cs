// Ignore Spelling: DTO Enums

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GeneralDLL.DTO.SSOServices.SystemEnums
{
    public enum QuestionAnswerConditionType
    {
        [Description("مساوی")]
        equal = 0,
        [Description("نا مساوی")]
        not_equal = 1,
        [Description("کوچکتر")]
        less = 2,
        [Description("بزرگتر")]
        greater = 3,
        [Description("مکوچکتر مساوی")]
        lessEqual = 4,
        [Description("بزرگتر مساوی")]
        greaterEqual = 5,
        [Description("بین دو مقدار")]
        beetween = 6
    }
}