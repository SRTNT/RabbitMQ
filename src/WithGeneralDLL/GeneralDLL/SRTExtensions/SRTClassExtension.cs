// Ignore Spelling: SRT Nullable

using GeneralDLL.SRTExtensions.ReflectionExtensionDetails;
using GeneralDLL.SRTExtensions.SRTEnums;
using GeneralDLL.SRTExtensions.SRTExtensionsDetails;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeneralDLL.SRTExtensions
{
    public static class SRTClassExtension
    {
        public static SRTStringFunction SRT_String_Converter(this string s)
        {
            return new SRTStringFunction(s);
        }
        public static SRTObjectFunction SRT_Object_Converter(this object s)
        {
            return new SRTObjectFunction(s);
        }
        public static string SRT_ToString(this object s)
        { return JsonConvert.SerializeObject(s); }
        public static SRTDateTimeFunction SRT_DateTime_Converter(this DateTime s)
        {
            return new SRTDateTimeFunction(s);
        }
        public static SRTDateTimeFunction SRT_DateTime_Converter(this DateTime? s)
        {
            return new SRTDateTimeFunction(s);
        }
        public static string SRT_DateTime_ConverterString(this DateTime? s, bool includeTime)
        {
            return s.SRT_DateTime_Converter().SRT_PersianData(includeTime: includeTime);
        }
        public static string SRT_PersianData(this DateTime? s, bool includeTime)
            => s.SRT_DateTime_ConverterString(includeTime);
        public static string SRT_DateTime_ConverterString(this DateTime s, bool includeTime)
        {
            return s.SRT_DateTime_Converter().SRT_PersianData(includeTime: includeTime);
        }
        public static string SRT_PersianData(this DateTime s, bool includeTime)
            => s.SRT_DateTime_ConverterString(includeTime);

        #region Number
        public static string SRT_ConvertToPersianNumber(this int englishNumber)
            => SRT_ConvertToPersianNumber(englishNumber.ToString());
        public static string SRT_ConvertToPersianNumber(this long englishNumber)
            => SRT_ConvertToPersianNumber(englishNumber.ToString());
        public static string SRT_ConvertToPersianNumber(this double englishNumber)
        {
            var a = englishNumber.ToString().Split('.');

            var b = SRT_ConvertToPersianNumber(a[0]);
            if (a.Length == 1)
                return b;

            var c = SRT_ConvertToPersianNumber(a[1], septateNumber: false);

            return b + "." + c;
        }
        public static string SRT_ConvertToPersianNumber(this string englishNumber, bool septateNumber = true)
        {
            string[] persianDigits = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
            char[] englishDigits = englishNumber.ToCharArray();

            for (int i = 0; i < englishDigits.Length; i++)
            {
                if (char.IsDigit(englishDigits[i]))
                {
                    int digit = (int)char.GetNumericValue(englishDigits[i]);
                    englishDigits[i] = persianDigits[digit][0];
                }
            }

            // Create a new string from the converted characters
            string persianNumber = new string(englishDigits);

            if (!septateNumber)
                return persianNumber;

            // Format the number with commas (or any separator)
            return FormatWithCommas(persianNumber);
        }
        // Helper method to format the number
        private static string FormatWithCommas(string number)
        {
            // Remove any existing commas
            number = number.Replace("،", "");

            // Split the number into parts
            var parts = number.Split('.');
            string integerPart = parts[0];
            string decimalPart = parts.Length > 1 ? "." + parts[1] : "";

            // Use StringBuilder for efficient string manipulation
            var formattedIntegerPart = new System.Text.StringBuilder();

            for (int i = 0; i < integerPart.Length; i++)
            {
                if (i > 0 && (integerPart.Length - i) % 3 == 0)
                {
                    formattedIntegerPart.Append("،"); // Add comma (Persian comma)
                }
                formattedIntegerPart.Append(integerPart[i]);
            }

            return formattedIntegerPart.ToString() + decimalPart;
        }
        public static string SRT_NumberToShow(this int input)
        {
            return input.ToString().SRT_String_Converter().NumberToShow();
        }
        public static string SRT_NumberToShow(this long input)
        {
            return input.ToString().SRT_String_Converter().NumberToShow();
        }
        public static string SRT_NumberToShow(this double input)
        {
            return input.ToString().SRT_String_Converter().NumberToShow();
        }
        public static string SRT_NumberToShow(this decimal input)
        {
            return input.ToString().SRT_String_Converter().NumberToShow();
        }
        #endregion

        #region Enum
        public static string SRT_Enum_GetName<T>(this T data)
            where T : System.Enum
        {
            return EnumWorker.GetName(data);
        }
        public static string SRT_Enum_GetDescription<T>(this T data)
            where T : System.Enum
        {
            return EnumWorker.GetDescription(data);
        }
        public static List<string> SRT_Enum_ToStringList<T>(this T data, List<T> lstRemove = null)
            where T : System.Enum
        {
            return SRT_Enum_GetDataList(data, lstRemove).Select(q => q.Description).ToList();
        }
        public static List<EnumWorker.EnumItem<T>> SRT_Enum_GetDataList<T>(this T data, List<T> lstRemove = null)
            where T : System.Enum
        {
            return EnumWorker.GetData(lstRemove);
        }

        public static EnumWorker.EnumItem<T> SRT_GetEnumItem<T>(this int data, bool exact = true, List<T> lstRemove = null)
            where T : System.Enum
        {
            var lst = EnumWorker.GetData(lstRemove);
            if (data < 0)
                data = 0;

            var max = Enum.GetValues(typeof(T)).Cast<T>().Last().SRT_Enum_ToInt();

            if (data > max)
                data = max;

            return lst.First(q => q.Value.SRT_Enum_ToInt() == data);
        }
        public static T SRT_Enum_GetValue<T>(this int data, bool exact = true, List<T> lstRemove = null)
            where T : System.Enum
        {
            return data.SRT_GetEnumItem(exact, lstRemove).Value;
        }
        public static int SRT_Enum_ToInt<T>(this T data)
            where T : System.Enum
        {
            try
            {
                Enum test = Enum.Parse(typeof(T), data.ToString()) as Enum;
                int x = Convert.ToInt32(test);

                return x;
            }
            catch
            { throw new Exception("Out Of Range"); }
        }
        #endregion

        #region List
        public static List<List<T>> SRT_SplitList<T>(List<T> lstData, int splitCount)
        {
            try
            {
                return lstData
                        .Select((x, i) => new { Index = i, Value = x })
                        .GroupBy(x => x.Index / splitCount)
                        .Select(x => x.Select(v => v.Value).ToList())
                        .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("SplitList", ex);
            }
        }

        public static List<T> SRT_RemoveDuplicate<T>(List<T> lstData, string nameOfProperty)
        {
            try
            {
                return (
                        from o in lstData
                            //orderby o.Point.X, o.Point.Y, o.Value descending
                        group o by o.SRT_GetPropertyValue(nameOfProperty) into g
                        select g.First()
                       ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("RemoveDuplicate", ex);
            }
        }
        #endregion

        #region Most Needed
        public static bool SRT_StringIsNullOrEmpty(this string data)
        { return data.SRT_String_Converter().StringIsEmpty(); }
        public static string SRT_StringArabicToPersian(this string data)
        { return data.SRT_String_Converter().StringArabicToPersian(); }
        #endregion

        #region Read Content
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response, bool checkStatusCodeOK = false)
        {
            if (checkStatusCodeOK && !response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(dataAsString);
        }
        #endregion
    }
}