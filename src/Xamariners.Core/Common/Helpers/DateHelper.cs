using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.Core.Common.Helpers
{
    public static class DateHelper
    {
        public static DateTimeOffset ParseDate(string date, string format = "dd/MM/yyyy")
        {
            var success = DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var result);
            
            return result;
        }
        
        public static DateTimeOffset GetUtcSafe(this DateTimeOffset datetime)
        {
            if (datetime.DateTime.Kind == DateTimeKind.Utc)
            {
                return datetime;
            }
            else
            {
                return datetime.ToUniversalTime();
            }
        }

        public static DateTimeOffset FirstDayOfWeek(this DateTimeOffset dt, bool preserveTime)
        {
            var diff = dt.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
                diff += 7;

            if (preserveTime)
                return dt.AddDays(-diff);
            else
                return dt.AddDays(-diff).Date;
        }

        public static DateTime FirstDayOfWeek(this DateTime dt, bool preserveTime)
        {
            var diff = dt.DayOfWeek - DayOfWeek.Monday;
            if(diff < 0)
                diff += 7;

            if (preserveTime)
                return dt.AddDays(-diff);
            else
                return dt.AddDays(-diff).Date;
        }

        public static DateTimeOffset LastDayOfWeek(this DateTimeOffset dt, bool preserveTime)
        {
            return dt.FirstDayOfWeek(preserveTime).AddDays(6);
        }

        public static DateTime LastDayOfWeek(this DateTime dt, bool preserveTime)
        {
            return dt.FirstDayOfWeek(preserveTime).AddDays(6);
        }

        public static DateTimeOffset FirstDayOfMonth(this DateTimeOffset dt, bool preserveTime)
        {
            if (preserveTime)
                return new DateTime(dt.Year, dt.Month, 1, dt.Hour, dt.Minute, dt.Second);
            else
                return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt, bool preserveTime)
        {
            if (preserveTime)
                return new DateTime(dt.Year, dt.Month, 1, dt.Hour, dt.Minute, dt.Second);
            else
                return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTimeOffset LastDayOfMonth(this DateTimeOffset dt, bool preserveTime)
        {
            return dt.FirstDayOfMonth(preserveTime).AddMonths(1).AddDays(-1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt, bool preserveTime)
        {
            return dt.FirstDayOfMonth(preserveTime).AddMonths(1).AddDays(-1);
        }

        public static bool IsLastDayOfMonth(this DateTimeOffset dt)
        {
            return DateTime.DaysInMonth(dt.Year, dt.Month) == dt.Day;
        }

        public static DateTimeOffset FirstDayOfNextMonth(this DateTimeOffset dt, bool preserveTime)
        {
            return dt.FirstDayOfMonth(preserveTime).AddMonths(1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt, bool preserveTime)
        {
            return dt.FirstDayOfMonth(preserveTime).AddMonths(1);
        }
    }
}
