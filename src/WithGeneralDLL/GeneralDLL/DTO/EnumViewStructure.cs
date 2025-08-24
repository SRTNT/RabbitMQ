// Ignore Spelling: DTO

using GeneralDLL.SRTExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDLL.DTO
{
    public class EnumViewStructureInstance
    {
        public static EnumViewStructure<T> GetInstance<T>(T data)
            where T : Enum
            => new EnumViewStructure<T>() { enumType = data };
    }

    public class EnumViewStructure<T>
        where T : Enum
    {
        public T enumType { get; set; }
        public string name { get => enumType.SRT_Enum_GetDescription(); }
    }
}
