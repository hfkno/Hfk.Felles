using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class DateTimes
    {



        #region Validation and content



        /// <summary>
        ///     Checks a DateTime to ensure that is has recieved a value
        ///     and is not equal to DateTime.Min.
        /// </summary>
        /// <param name="dateTime">The DateTime to check.</param>
        /// <returns>Whether the provided DateTime has a value other than DateTime.Min.</returns>
        public static bool Exists(this DateTime dateTime)
        {
            return dateTime != DateTime.MinValue;
        }

        /// <summary>
        ///     Checks a DateTime to ensure that is has recieved a value
        ///     and is not equal to DateTime.Min.
        /// </summary>
        /// <param name="dateTime">The DateTime to check.</param>
        /// <returns>Whether the provided DateTime has a value other than DateTime.Min.</returns>
        public static bool Exists(this DateTime? dateTime)
        {
            return dateTime.HasValue && dateTime.Value.Exists();
        }

        /// <summary>
        ///     Returns whether the provided date is inbetween two other dates.
        /// </summary>
        /// <param name="when">The time to check.</param>
        /// <param name="start">The start time.</param>
        /// <param name="end">The end time.</param>
        /// <returns>Whether the provided date falls within the provided date range.</returns>
        public static bool IsBetween(this DateTime when, DateTime start, DateTime end)
        {
            return start <= when && when <= end;
        }



        #endregion



        #region Relative date operations



        /// <summary>
        ///     Gets a DateTime representing the first day in a specified month.
        /// </summary>
        /// <param name="input">The date to check.</param>
        /// <returns></returns>
        public static DateTime FirstOfMonth(this DateTime input)
        {
            return input.AddDays(1 - input.Day);
        }

        /// <summary>
        ///     Gets a DateTime representing the last day in the current month.
        /// </summary>
        /// <param name="input">The date to check.</param>
        /// <returns>The last day in a given month.</returns>
        public static DateTime LastOfMonth(this DateTime input)
        {
            var daysInMonth = DateTime.DaysInMonth(input.Year, input.Month);
            return input.FirstOfMonth().AddDays(daysInMonth - 1);
        }

        /// <summary>
        ///     Gets a DateTime representing the first incident of the specified weekday in the month to check.
        /// </summary>
        /// <param name="input">The date to check.</param>
        /// <param name="dayOfWeek">The day of week to check for.</param>
        /// <returns>The first day in the given month.</returns>
        public static DateTime FirstDayInMonth(this DateTime input, DayOfWeek dayOfWeek)
        {
            var first = input.FirstOfMonth();
            if (first.DayOfWeek != dayOfWeek)
            {
                first = first.Next(dayOfWeek);
            }
            return first;
        }

        /// <summary>
        ///     Gets a DateTime representing the last specified day in the current month.
        /// </summary>
        /// <param name="input">The date to check.</param>
        /// <param name="dayOfWeek">The current day of week.</param>
        /// <returns>The last day in the given month.</returns>
        public static DateTime LastDayInMonth(this DateTime input, DayOfWeek dayOfWeek)
        {
            var lastOfMonth = input.LastOfMonth();
            var dayDiff = dayOfWeek - lastOfMonth.DayOfWeek;
            if (dayDiff > 0) dayDiff -= 7;
            return lastOfMonth.AddDays(dayDiff);
        }

        /// <summary>
        ///     Gets the first day-of-week following the provided date.
        /// </summary>
        /// <param name="input">The date to check.</param>
        /// <param name="dayOfWeek">The next day of week to get.</param>
        /// <returns>The next instance of the provided day.</returns>
        public static DateTime Next(this DateTime input, DayOfWeek dayOfWeek)
        {
            var offsetDays = dayOfWeek - input.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }
            return input.AddDays(offsetDays);
        }

        /// <summary>
        ///     Gets the week number of a given date within the given year.
        /// </summary>
        /// <param name="dayToCheck">The date to find the week number for.</param>
        /// <returns>The week number of the provided date.</returns>
        public static int WeekOfYear(this DateTime dayToCheck)
        {
            var ciCurr = CultureInfo.CurrentCulture;
            var weekNum = ciCurr.Calendar.GetWeekOfYear(dayToCheck, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
            return weekNum;
        }

        /// <summary>
        ///     Returns a list of the first day of each month in the previous 12 months.
        /// </summary>
        /// <param name="date">The date to begin the calculation from.</param>
        /// <returns>A datetime list of the first day of each of the 12 previous months.</returns>
        public static List<DateTime> Preceding12Months(this DateTime date)
        {
            const int monthsToTake = 12;
            return date.PrecedingMonths(monthsToTake);
        }

        /// <summary>
        ///     Returns a list of the first day in the months previous to a given date.
        /// </summary>
        /// <param name="date">The date to begin the calculation from.</param>
        /// <param name="monthsToTake"></param>
        /// <returns></returns>
        public static List<DateTime> PrecedingMonths(this DateTime date, int monthsToTake)
        {
            var list = new List<DateTime>();
            if (monthsToTake <= 0) return list;

            var endDate = new DateTime(date.Year, date.AddMonths(1).Month, 1);

            var months = 1.To(monthsToTake).Reverse();
            months.ForEach(m => list.Add(endDate.AddMonths(-m)));

            return list;
        }

        /// <summary>
        ///     Returns a list of the first day of each month in the given interval.
        /// </summary>
        /// <param name="fra"></param>
        /// <param name="til"></param>
        /// <returns></returns>
        public static List<DateTime> EnsuingMonthsTo(this DateTime fra, DateTime til)
        {
            var list = new List<DateTime>();
            if (til < fra) return list;

            var monthDiff = ((til.Year - fra.Year)*12) + til.Month - fra.Month;
            var start = new DateTime(fra.Year, fra.Month, 1);

            var months = 0.To(monthDiff);
            months.ForEach(m => list.Add(start.AddMonths(m)));

            return list;
        }



        #endregion



        #region Conversions


        /// <summary>
        ///     Converts a datetime to a string in an unambiguious date format for SqlServer.
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>An SqlServer friendly date.</returns>
        public static string ToSqlDate(this DateTime date)
        {
            return string.Format("{0:yyyyMMdd}", date);
        }

        /// <summary>
        ///     Converts a datetime to a string in an unambiguious time format for SqlServer.
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>An SqlServer friendly time.</returns>
        public static string ToSqlTime(this DateTime date)
        {
            return string.Format("{0:HH:mm:ss}", date);
        }

        /// <summary>
        ///     Converts a datetime to a string in an unambiguious datetime format for SqlServer.
        /// </summary>
        /// <param name="date">The datetime to convert.</param>
        /// <returns>An SqlServer friendly datetime.</returns>
        public static string ToSqlDateTime(this DateTime date)
        {
            return string.Format("{0:yyyyMMdd HH:mm:ss}", date);
        }



        #endregion



        #region Pretty printing of date age



        internal const int one_minute_seconds = 60;
        internal const int two_minutes_seconds = 120;
        internal const int one_hours_seconds = 3600;
        internal const int two_hours_seconds = 7200;
        internal const int one_days_seconds = 86400;

        internal const int one_day = 1;
        internal const int one_week_days = 7;
        internal const int one_month_days = 31;

        internal const int one_week = 1;

        /// <summary>
        ///     Gives a user-friendly text string for the time since a given datetime.
        /// </summary>
        /// <param name="d">The incident datetime to communicate.</param>
        /// <returns>A user friendly text string showing time since the given incident.</returns>
        public static string PrettyTimeSince(this DateTime d)
        {
            var elapsed = DateTime.Now.Subtract(d);
            var dayDiff = (int) elapsed.TotalDays;
            var secDiff = (int) elapsed.TotalSeconds;
            var ret = "";

            switch (dayDiff)
            {
                case 0:
                    if (secDiff < one_minute_seconds)
                        ret = "just now";

                    else if (secDiff < two_minutes_seconds)
                        ret = "1 minute ago";

                    else if (secDiff < one_hours_seconds)
                        ret = string.Format("{0} minutes ago", Math.Floor((double) secDiff/one_minute_seconds));

                    else if (secDiff < two_hours_seconds)
                        ret = "1 hour ago";

                    else if (secDiff < one_days_seconds)
                        ret = string.Format("{0} hours ago", Math.Floor((double) secDiff/one_hours_seconds));
                    break;

                case one_day:
                    ret = "yesterday";
                    break;

                default:
                    if (dayDiff < one_week_days)
                        ret = string.Format("{0} days ago", dayDiff);

                    else if (dayDiff == one_week_days)
                        ret = "1 week ago";

                    else if (dayDiff < one_month_days)
                    {
                        var weekCount = (int) Math.Ceiling((double) dayDiff/one_week_days);
                        ret = string.Format("{0} weeks ago", weekCount);
                    }
                    break;
            }

            return ret.Exists() ? ret : "several weeks ago";
        }


        /// <summary>
        ///     Gives a user-friendly text string for the time since a given datetime in Norwegian.
        /// </summary>
        /// <param name="d">The incident datetime to communicate.</param>
        /// <returns>A user friendly text string showing time since the given incident in Norwegian.</returns>
        public static string PrettyTimeSinceInNorwegian(this DateTime d)
        {
            var elapsed = DateTime.Now.Subtract(d);
            var dayDiff = (int) elapsed.TotalDays;
            var secDiff = (int) elapsed.TotalSeconds;
            var ret = "";

            switch (dayDiff)
            {
                case 0:
                    if (secDiff < one_minute_seconds)
                        ret = "nå nylig";

                    else if (secDiff < two_minutes_seconds)
                        ret = "1 minutt siden";

                    else if (secDiff < one_hours_seconds)
                        ret = string.Format("{0} minutter siden", Math.Floor((double) secDiff/one_minute_seconds));

                    else if (secDiff < two_hours_seconds)
                        ret = "1 time siden";

                    else if (secDiff < one_days_seconds)
                        ret = string.Format("{0} timer siden", Math.Floor((double) secDiff/one_hours_seconds));
                    break;

                case one_day:
                    ret = "i går";
                    break;

                default:
                    if (dayDiff < one_week_days)
                        ret = string.Format("{0} dager siden", dayDiff);

                    else if (dayDiff == one_week_days)
                        ret = "1 uke siden";

                    else if (dayDiff < one_month_days)
                    {
                        var weekCount = (int) Math.Ceiling((double) dayDiff/one_week_days);
                        ret = string.Format("{0} uker siden", weekCount);
                    }
                    break;
            }

            return ret.Exists() ? ret : "flere uker siden";
        }



        #endregion



    }
}