// Ignore Spelling: SRT Nullable

using GeneralDLL.SRTExceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GeneralDLL.SRTExtensions.SRTExtensionsDetails
{
    public class SRTDateTimeFunction
    {
        DateTime? date;

        public SRTDateTimeFunction(DateTime? date)
        {
            this.date = date;
        }

        #region Date Time To Persian
        public string SRT_PersianData(bool includeTime = true)
        {
            if (date is null)
                return null;

            var dateTime = date.Value;

            var persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);

            if (includeTime)
            {
                int hour = dateTime.Hour;
                int minute = dateTime.Minute;
                int second = dateTime.Second;

                return $"{year}/{month:D2}/{day:D2} {hour:D2}:{minute:D2}:{second:D2}";
            }
            return $"{year}/{month:D2}/{day:D2}";
        }
        #endregion
    }
}
