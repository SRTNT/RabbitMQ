using System;
using System.Globalization;

namespace GeneralDLL
{
    public class DateTimePersian
    {
        private readonly PersianCalendar _persianCalendar;

        public DateTime DateMilady { get; private set; }

        #region Constructors
        public DateTimePersian()
        {
            DateMilady = DateTime.Now;
            _persianCalendar = new PersianCalendar();
        }

        public DateTimePersian(int year, int month, int day, int hour = 0, int min = 0)
        {
            DateMilady = DateTime.Now;
            _persianCalendar = new PersianCalendar();

            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = min;
        }

        public DateTimePersian(DateTime date) : this()
        {
            DateMilady = date;
        }
        #endregion

        #region Properties

        public int Year
        {
            get => _persianCalendar.GetYear(DateMilady);
            set => UpdateDateMilady(value, Month, Day, Hour, Minute, DateMilady.Second, DateMilady.Millisecond);
        }

        public int Month
        {
            get => _persianCalendar.GetMonth(DateMilady);
            set => UpdateDateMilady(Year, value, Day, Hour, Minute, DateMilady.Second, DateMilady.Millisecond);
        }

        public int Day
        {
            get => _persianCalendar.GetDayOfMonth(DateMilady);
            set => UpdateDateMilady(Year, Month, value, Hour, Minute, DateMilady.Second, DateMilady.Millisecond);
        }

        public int Hour
        {
            get => _persianCalendar.GetHour(DateMilady);
            set => UpdateDateMilady(Year, Month, Day, value, Minute, DateMilady.Second, DateMilady.Millisecond);
        }

        public int Minute
        {
            get => _persianCalendar.GetMinute(DateMilady);
            set => UpdateDateMilady(Year, Month, Day, Hour, value, DateMilady.Second, DateMilady.Millisecond);
        }

        #endregion

        #region Add Methods

        public DateTimePersian AddYears(int value)
        {
            DateMilady = DateMilady.AddYears(value);
            return this;
        }

        public DateTimePersian AddMonths(int value)
        {
            DateMilady = DateMilady.AddMonths(value);
            return this;
        }

        public DateTimePersian AddDays(int value)
        {
            DateMilady = DateMilady.AddDays(value);
            return this;
        }

        public DateTimePersian AddHours(int value)
        {
            DateMilady = DateMilady.AddHours(value);
            return this;
        }

        public DateTimePersian AddMinutes(int value)
        {
            DateMilady = DateMilady.AddMinutes(value);
            return this;
        }

        #endregion

        #region Conversion

        private void UpdateDateMilady(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            DateMilady = _persianCalendar.ToDateTime(year, month, day, hour, minute, second, millisecond);
        }

        public int GetCurrentMonthDays()
        {
            return _persianCalendar.GetDaysInMonth(Year, Month);
        }

        #endregion

        #region Operator Overloading

        public static bool operator ==(DateTimePersian a, DateTimePersian b)
        {
            if (a is null ^ b is null)
                return false;
            if (a is null && b is null)
                return true;

            if (a.Equals(b))
                return true;

            if (a.DateMilady == b.DateMilady)
                return true;

            return a.ToString(includeTime: true) == b.ToString(includeTime: true);
        }

        public static bool operator !=(DateTimePersian a, DateTimePersian b) => !(a == b);

        public static bool operator <(DateTimePersian a, DateTimePersian b) => a.DateMilady < b.DateMilady;

        public static bool operator <=(DateTimePersian a, DateTimePersian b) => a < b || a == b;

        public static bool operator >(DateTimePersian a, DateTimePersian b) => a.DateMilady > b.DateMilady;

        public static bool operator >=(DateTimePersian a, DateTimePersian b) => a > b || a == b;

        #endregion

        #region Override Methods

        public override bool Equals(object obj)
        {
            if (obj is DateTimePersian other)
            {
                return DateMilady == other.DateMilady;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Year, Month, Day, Hour, Minute);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public string ToString(bool includeTime)
        {
            var persianDate = $"{Year}/{Month:D2}/{Day:D2}";
            if (includeTime)
            {
                return $"{persianDate} {Hour:D2}:{Minute:D2}:{DateMilady.Second:D2}";
            }
            return persianDate;
        }

        #endregion
    }
}
