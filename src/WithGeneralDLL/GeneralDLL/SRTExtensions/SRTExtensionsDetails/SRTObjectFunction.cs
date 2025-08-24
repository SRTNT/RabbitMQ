// Ignore Spelling: SRT Nullable

using GeneralDLL.SRTExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDLL.SRTExtensions.SRTExtensionsDetails
{
    public class SRTObjectFunction
    {
        object _data;

        public SRTObjectFunction(object data)
        {
            _data = data;
        }

        #region Object To Numeric
        public bool IsNumber()
        {
            return _data.ToString().SRT_String_Converter().StringIsNumber();
        }
        public int ToInt()
        {
            return _data.ToString().SRT_String_Converter().ToInt();
        }
        public int? ToInt_Nullable()
        {
            return _data.ToString().SRT_String_Converter().ToInt_Nullable();
        }
        public byte ToByte()
        {
            return _data.ToString().SRT_String_Converter().ToByte();
        }
        public long ToLong()
        {
            return _data.ToString().SRT_String_Converter().ToLong();
        }
        public double ToDouble()
        {
            return _data.ToString().SRT_String_Converter().ToDouble();
        }
        #endregion
    }
}
