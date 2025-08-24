// Ignore Spelling: SRT Nullable

using GeneralDLL.SRTExtensions.SRTEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneralDLL.SRTExtensions.SRTExtensionsDetails
{
    public class SRTStringFunction
    {
        private string _data;

        #region Constructors
        public SRTStringFunction(string data)
        { _data = data; }
        #endregion

        #region Main Func
        public string ToPersianNumber()
        {
            var input = _data;

            if (input is null)
                return null;

            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
                input = input.Replace(j.ToString(), persian[j]);

            return input;
        }
        public string ReplaceDigit_PersianToEnglish()
        {
            var input = _data;
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
                input = input.Replace(persian[j], j.ToString());

            return input;
        }

        public string StringArabicToPersian()
        {
            var data = _data;
            // تعریف مپ برای تبدیل حروف عربی به حروف فارسی
            var arabicToPersianMap = new Dictionary<char, char>
            {
                {'ي', 'ی'},
                {'ك', 'ک'},
                {'ة', 'ه'},
                {'ء', 'ئ'},
                {'ٱ', 'ا'},
                {'أ', 'ا'},
                {'إ', 'ا'},
                {'ؤ', 'و'},
                {'ئ', 'ی'},
                {'ى', 'ی'},
                {'آ', 'آ'}
            };

            foreach (var item in arabicToPersianMap)
            { data = data.Replace(item.Key, item.Value); }

            return data;
        }

        public string StringCheckReady()
        { return _data.Replace(" ", "").ToLower().SRT_String_Converter().StringArabicToPersian(); }

        public bool StringIsEmpty()
        { return string.IsNullOrWhiteSpace(_data); }

        public string Substring(int startIndex, int length)
        {
            var data = _data;

            if (string.IsNullOrWhiteSpace(data))
                return data;
            if (length > data.Length)
                return data;
            if (length > data.Length + startIndex)
                return data;
            return data.Substring(startIndex, length);
        }
        public string Substring(int length)
        {
            var data = _data;

            if (length > data.Length)
                return data;
            return data.Substring(0, length);
        }

        public decimal GetNumbersFromString()
        {
            var data = _data;

            string b = string.Empty;

            for (int i = 0; i < data.Length; i++)
            {
                if (char.IsDigit(data[i]) || data[i] == 46)
                    b += data[i];
            }

            if (data.IndexOf("-") == 0 || data.IndexOf("(") == 0)
                return -1 * decimal.Parse(b);
            return decimal.Parse(b);
        }
        public decimal? GetNumbersFromStringNull()
        {
            var data = _data;

            try
            { return data.SRT_String_Converter().GetNumbersFromString(); }
            catch { return null; }
        }

        public string SubString_Between(string from, string to)
        {
            var str = _data;

            int startIndex = str.IndexOf(from) + from.Length;
            int endIndex = str.IndexOf(to, startIndex);

            if (startIndex < 0)
                startIndex = 0;
            if (endIndex < 0)
                return null;

            return str.Substring(startIndex, endIndex - startIndex);
        }
        public string StringForCheck()
        {
            var str = _data;

            if (string.IsNullOrEmpty(str)) return str;
            return str.ToLower().Replace(" ", "");
        }
        public List<string> Split(string splitValue, StringSplitOptions splitOptions = StringSplitOptions.None)
        {
            var data = _data;

            return data.Split(new string[] { splitValue }, splitOptions).ToList();
        }
        #endregion

        #region string to Numeric
        public int? ToInt_Nullable()
        {
            var data = _data;

            if (data is null)
                return null;
            if (data.IndexOf("(") == 0)
                data = "-" + data.Replace("(", "").Replace(")", "");

            if (int.TryParse(data, out int i))
                return i;
            return null;
        }
        public byte? ToByte_Nullable()
        {
            var data = _data;

            if (data.IndexOf("(") == 0)
                data = "-" + data.Replace("(", "").Replace(")", "");

            if (byte.TryParse(data, out byte i))
                return i;
            return null;
        }
        public long? ToLong_Nullable()
        {
            var data = _data;

            if (data.IndexOf("(") == 0)
                data = "-" + data.Replace("(", "").Replace(")", "");

            if (long.TryParse(data, out long i))
                return i;
            return null;
        }
        public double? ToDouble_Nullable()
        {
            var data = _data;

            if (data.IndexOf("(") == 0)
                data = "-" + data.Replace("(", "").Replace(")", "");

            if (double.TryParse(data, out double i))
                return i;
            return null;
        }
        public decimal? ToDecimal_Nullable()
        {
            try
            {
                return ToDecimal();
            }
            catch
            { return null; }
        }

        public int ToInt()
        {
            var data = _data;

            try
            {
                if (data.IndexOf("(") == 0)
                    data = "-" + data.Replace("(", "").Replace(")", "");

                return int.Parse(data);
            }
            catch (Exception ex) { throw new Exception("ToInt - String=" + data, ex); }
        }
        public byte ToByte()
        {
            var data = _data;

            if (data.IndexOf("(") == 0)
                data = "-" + data.Replace("(", "").Replace(")", "");

            return byte.Parse(data);
        }
        public long ToLong()
        {
            var data = _data;

            try
            {
                if (data.IndexOf("(") == 0)
                    data = "-" + data.Replace("(", "").Replace(")", "");

                return long.Parse(data.Replace(",", ""));
            }
            catch (Exception ex) { throw new Exception("ToInt - String=" + data, ex); }
        }
        public double ToDouble()
        {
            var data = _data;

            if (data.IndexOf("(") == 0)
                data = "-" + data.Replace("(", "").Replace(")", "");

            return (double)double.Parse(data);
        }
        public decimal ToDecimal()
        {
            var data = _data;

            if (data.IndexOf("(") == 0)
                data = "-" + data.Replace("(", "").Replace(")", "");

            return decimal.Parse(data);
        }

        public bool StringIsNumber()
        { return ToDecimal_Nullable().HasValue; }

        #endregion

        #region string to Boolean
        public bool? ToBool_Nullable()
        {
            try
            {
                return ToBool();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// if string is number ==> number is equals to 0 return false else return true
        /// </summary>
        /// <returns></returns>
        public bool ToBool()
        {
            var data = _data;

            if (string.IsNullOrEmpty(data))
                return false;

            var number = ToDecimal_Nullable();

            if (number.HasValue)
            {
                if (number == 0)
                    return false;
                return true;
            }

            return bool.Parse(data);
        }
        #endregion

        #region String To Color
        public System.Drawing.Color? ToColor()
        {
            var value = _data;
            try
            {
                #region ARGB
                if (value.Count(q => q == ',') == 2)
                {
                    var d = value.Split(',');
                    return System.Drawing.Color.FromArgb(d[0].SRT_String_Converter().ToInt(), d[1].SRT_String_Converter().ToInt(), d[2].SRT_String_Converter().ToInt());
                }
                if (value.Count(q => q == ',') == 3)
                {
                    var d = value.Split(',');
                    return System.Drawing.Color.FromArgb(d[0].SRT_String_Converter().ToInt(), d[1].SRT_String_Converter().ToInt(), d[2].SRT_String_Converter().ToInt(), d[3].SRT_String_Converter().ToInt());
                }
                #endregion

                #region HTML
                if (value.IndexOf("#") < 0)
                    value = "#" + value;
                //TODO: Convert # To Color
                throw new NotImplementedException();
                #endregion
            }
            catch
            { return null; }
        }
        #endregion

        #region NumberToShow
        public string NumberToShow(int digit = 2)
        {
            var data = _data.Split('.');

            var dig = "";
            if (data.Count() > 1)
                dig = "." + (data[1].Length < digit + 1 ? data[1] : data[1].Substring(0, digit));
            return ToStringNumber(data[0]) + dig;
        }
        private static string ToStringNumber(string data, string separate = ",")
        {
            try
            {
                var lstData = MySplit(data, 3, true);
                var ret = lstData.First();

                for (int i = 1; i < lstData.Count; i++)
                {
                    ret += separate + lstData[i];
                }

                return ret;
            }
            catch { return "0"; }
        }
        private static List<string> MySplit(string str, int chunkSize, bool fromEnd = false)
        {
            var lstRet = new List<string>();
            if (fromEnd)
            {
                var p0 = str.Length % chunkSize;
                if (p0 != 0)
                {
                    lstRet.Add(str.Substring(0, p0));
                    str = new string(str.Skip(p0).ToArray());
                }
            }
            while (!string.IsNullOrEmpty(str))
            {
                var d = new string(str.Take(chunkSize).ToArray());
                lstRet.Add(d);
                str = new string(str.Skip(chunkSize).ToArray());
            }
            return lstRet;
        }
        #endregion

        #region String Enum Value
        /// <summary>
        /// data Maybe Is Name Or Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T GetEnumValue<T>(bool exact = true, List<T> lstRemove = null)
            where T : System.Enum
        {
            return GetEnumItem(exact, lstRemove).Value;
        }
        public T GetEnumValueFromName<T>(bool exact = true, List<T> lstRemove = null)
            where T : System.Enum
        {
            return GetEnumItemFromName(exact, lstRemove).Value;

        }
        public T GetEnumValueFromDescription<T>(bool exact = true, List<T> lstRemove = null)
            where T : System.Enum
        {
            return GetEnumItemFromDescription(exact, lstRemove).Value;
        }
        #endregion

        #region String Enum Item
        public EnumWorker.EnumItem<T> GetEnumItem<T>(bool exact = true, List<T> lstRemove = null)
            where T : System.Enum
        {
            var lst = EnumWorker.GetData(lstRemove);
            EnumWorker.EnumItem<T> dm = GetEnumItemFromName(exact, lstRemove);
            if (dm == null)
                dm = GetEnumItemFromDescription(exact, lstRemove);

            return dm;
        }
        public EnumWorker.EnumItem<T> GetEnumItemFromName<T>(bool exact = true, List<T> lstRemove = null)
            where T : System.Enum
        {
            var lst = EnumWorker.GetData(lstRemove);
            #region Exact
            if (exact)
                return lst.FirstOrDefault(q => q.Name.ToLower().Trim() == _data.ToLower().Trim());
            #endregion

            return lst.FirstOrDefault(q => q.Name.ToLower().Trim().Contains(_data.ToLower().Trim()));
        }
        public EnumWorker.EnumItem<T> GetEnumItemFromDescription<T>(bool exact = true, List<T> lstRemove = null)
            where T : System.Enum
        {
            var lst = EnumWorker.GetData(lstRemove);
            #region Exact
            if (exact)
                return lst.FirstOrDefault(q => q.Description.ToLower().Trim() == _data.ToLower().Trim());
            #endregion

            return lst.FirstOrDefault(q => q.Description.ToLower().Trim().Contains(_data.ToLower().Trim()));
        }
        #endregion
    }
}
