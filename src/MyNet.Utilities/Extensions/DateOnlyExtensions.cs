// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using MyNet.Utilities.Helpers;

namespace MyNet.Utilities
{
    /// <summary>
    /// <see cref="DateOnly"/> extensions related to spatial or temporal relations
    /// </summary>
    public static class DateOnlyExtensions
    {
        /// <summary>
        /// Returns the last day of the year changing the time to the very end of the day. Eg, 2011-12-24T06:40:20.005 => 2020-12-31T23:59:59.999
        /// </summary>
        public static DateOnly EndOfDecade(this DateOnly date) => date.SetYear(date.Year - (date.Year % 10) + 9).EndOfYear();

        /// <summary>
        /// Returns the Start day of the year changing the time to the very start of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2010-01-01T00:00:00.000. <see cref="DateTime"/>
        /// </summary>
        public static DateOnly BeginningOfDecade(this DateOnly date) => date.SetYear(date.Year - (date.Year % 10)).BeginningOfYear();

        /// <summary>
        /// Returns the same date (same Day, Month, Hour, Minute, Second etc) in the next calendar year.
        /// If that day does not exist in next year in same month, number of missing days is added to the last day in same month next year.
        /// </summary>
        public static DateOnly NextYear(this DateOnly start)
        {
            var nextYear = start.Year + 1;
            var numberOfDaysInSameMonthNextYear = DateTime.DaysInMonth(nextYear, start.Month);

            if (numberOfDaysInSameMonthNextYear < start.Day)
            {
                var differenceInDays = start.Day - numberOfDaysInSameMonthNextYear;
                var dateTime = new DateOnly(nextYear, start.Month, numberOfDaysInSameMonthNextYear);
                return dateTime.AddDays(differenceInDays);
            }
            return new DateOnly(nextYear, start.Month, start.Day);
        }

        /// <summary>
        /// Returns the same date (same Day, Month, Hour, Minute, Second etc) in the previous calendar year.
        /// If that day does not exist in previous year in same month, number of missing days is added to the last day in same month previous year.
        /// </summary>
        public static DateOnly PreviousYear(this DateOnly start)
        {
            var previousYear = start.Year - 1;
            var numberOfDaysInSameMonthPreviousYear = DateTime.DaysInMonth(previousYear, start.Month);

            if (numberOfDaysInSameMonthPreviousYear < start.Day)
            {
                var differenceInDays = start.Day - numberOfDaysInSameMonthPreviousYear;
                var dateTime = new DateOnly(previousYear, start.Month, numberOfDaysInSameMonthPreviousYear);
                return dateTime.AddDays(differenceInDays);
            }
            return new DateOnly(previousYear, start.Month, start.Day);
        }

        /// <summary>
        /// Returns <see cref="DateTime"/> increased by 24 hours ie Next Day.
        /// </summary>
        public static DateOnly NextDay(this DateOnly start) => start.AddDays(1);

        /// <summary>
        /// Returns <see cref="DateTime"/> decreased by 24h period ie Previous Day.
        /// </summary>
        public static DateOnly PreviousDay(this DateOnly start) => start.AddDays(-1);

        /// <summary>
        /// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
        /// </summary>
        public static DateOnly Next(this DateOnly start, DayOfWeek day)
        {
            do
            {
                start = start.NextDay();
            } while (start.DayOfWeek != day);

            return start;
        }

        /// <summary>
        /// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
        /// </summary>
        public static DateOnly Next(this DateOnly start, int day, int month)
        {
            do
            {
                start = start.NextDay();
            } while (start.Month != month || start.Day != day);

            return start;
        }

        /// <summary>
        /// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
        /// </summary>
        public static DateOnly Previous(this DateOnly start, DayOfWeek day)
        {
            do
            {
                start = start.PreviousDay();
            } while (start.DayOfWeek != day);

            return start;
        }

        /// <summary>
        /// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
        /// </summary>
        public static DateOnly Previous(this DateOnly start, int day, int month)
        {
            do
            {
                start = start.PreviousDay();
            } while (start.Month != month || start.Day != day);

            return start;
        }

        /// <summary>
        /// Increases supplied <see cref="DateTime"/> for 7 days ie returns the Next Week.
        /// </summary>
        public static DateOnly WeekAfter(this DateOnly start) => start.AddDays(DateTimeHelper.NumberOfDaysInWeek());

        /// <summary>
        /// Decreases supplied <see cref="DateTime"/> for 7 days ie returns the Previous Week.
        /// </summary>
        public static DateOnly WeekEarlier(this DateOnly start) => start.AddDays(-DateTimeHelper.NumberOfDaysInWeek());

        /// <summary>
        /// Returns <see cref="DateTime"/> with changed Year part.
        /// </summary>
        public static DateOnly SetYear(this DateOnly value, int year) => new(year, value.Month, value.Day);

        /// <summary>
        /// Returns <see cref="DateTime"/> with changed Month part.
        /// </summary>
        public static DateOnly SetMonth(this DateOnly value, int month) => new(value.Year, month, value.Day);

        /// <summary>
        /// Returns <see cref="DateTime"/> with changed Day part.
        /// </summary>
        public static DateOnly SetDay(this DateOnly value, int day) => new(value.Year, value.Month, day);


        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> is before then current value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="toCompareWith">Value to compare with.</param>
        /// <returns>
        /// 	<c>true</c> if the specified current is before; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBefore(this DateOnly current, DateOnly toCompareWith) => current < toCompareWith;

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> is before then current value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="toCompareWith">Value to compare with.</param>
        /// <returns>
        /// 	<c>true</c> if the specified current is before; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBefore(this DateOnly current, DateTime toCompareWith) => current.IsBefore(toCompareWith.ToDate());

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> value is After then current value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="toCompareWith">Value to compare with.</param>
        /// <returns>
        /// 	<c>true</c> if the specified current is after; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAfter(this DateOnly current, DateOnly toCompareWith) => current > toCompareWith;

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> value is After then current value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="toCompareWith">Value to compare with.</param>
        /// <returns>
        /// 	<c>true</c> if the specified current is after; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAfter(this DateOnly current, DateTime toCompareWith) => current.IsAfter(toCompareWith.ToDate());

        public static bool IsBetween(this DateOnly current, DateOnly toCompareFrom, DateOnly toCompareTo) => current >= DateTimeHelper.Min(toCompareFrom, toCompareTo) && current <= DateTimeHelper.Max(toCompareTo, toCompareFrom);

        /// <summary>
        /// Returns the given <see cref="DateTime"/> with hour and minutes set At given values.
        /// </summary>
        /// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
        /// <param name="hour">The hour to set time to.</param>
        /// <param name="minute">The minute to set time to.</param>
        /// <returns><see cref="DateTime"/> with hour and minute set to given values.</returns>
        public static DateTime At(this DateOnly current, TimeOnly time) => current.ToDateTime(time);

        /// <summary>
        /// Returns the given <see cref="DateTime"/> with hour and minutes set At given values.
        /// </summary>
        /// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
        /// <param name="hour">The hour to set time to.</param>
        /// <param name="minute">The minute to set time to.</param>
        /// <returns><see cref="DateTime"/> with hour and minute set to given values.</returns>
        public static DateTime At(this DateOnly current, int hour, int minute) => current.At(new TimeOnly(hour, minute));

        /// <summary>
        /// Returns the given <see cref="DateTime"/> with hour and minutes and seconds set At given values.
        /// </summary>
        /// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
        /// <param name="hour">The hour to set time to.</param>
        /// <param name="minute">The minute to set time to.</param>
        /// <param name="second">The second to set time to.</param>
        /// <returns><see cref="DateTime"/> with hour and minutes and seconds set to given values.</returns>
        public static DateTime At(this DateOnly current, int hour, int minute, int second) => current.At(new TimeOnly(hour, minute, second));

        /// <summary>
        /// Returns the given <see cref="DateTime"/> with hour and minutes and seconds and milliseconds set At given values.
        /// </summary>
        /// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
        /// <param name="hour">The hour to set time to.</param>
        /// <param name="minute">The minute to set time to.</param>
        /// <param name="second">The second to set time to.</param>
        /// <param name="milliseconds">The milliseconds to set time to.</param>
        /// <returns><see cref="DateTime"/> with hour and minutes and seconds set to given values.</returns>
        public static DateTime At(this DateOnly current, int hour, int minute, int second, int milliseconds) => current.At(new TimeOnly(hour, minute, second, milliseconds));

        public static DateTime BeginningOfDay(this DateOnly current) => current.At(TimeOnly.MinValue);

        public static DateTime EndOfDay(this DateOnly current) => current.At(TimeOnly.MaxValue);

        /// <summary>
        /// Sets the day of the <see cref="DateTime"/> to the first day in that calendar quarter.
        /// credit to http://www.devcurry.com/2009/05/find-first-and-last-day-of-current.html
        /// </summary>
        /// <param name="current"></param>
        /// <returns>given <see cref="DateTime"/> with the day part set to the first day in the quarter.</returns>
        public static DateOnly BeginningOfQuarter(this DateOnly current)
        {
            var currentQuarter = (current.Month - 1) / 3 + 1;
            return new DateOnly(current.Year, 3 * currentQuarter - 2, 1);
        }

        /// <summary>
        /// Sets the day of the <see cref="DateTime"/> to the first day in that month.
        /// </summary>
        /// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
        /// <returns>given <see cref="DateTime"/> with the day part set to the first day in that month.</returns>
        public static DateOnly BeginningOfMonth(this DateOnly current) => current.SetDay(1);

        /// <summary>
        /// Sets the day of the <see cref="DateTime"/> to the last day in that calendar quarter.
        /// credit to http://www.devcurry.com/2009/05/find-first-and-last-day-of-current.html
        /// </summary>
        /// <param name="current"></param>
        /// <returns>given <see cref="DateTime"/> with the day part set to the last day in the quarter.</returns>
        public static DateOnly EndOfQuarter(this DateOnly current)
        {
            var currentQuarter = (current.Month - 1) / 3 + 1;
            var firstDay = new DateOnly(current.Year, 3 * currentQuarter - 2, 1);
            return new DateOnly(firstDay.Year, firstDay.Month + 2, 1);
        }

        /// <summary>
        /// Sets the day of the <see cref="DateTime"/> to the last day in that month.
        /// </summary>
        /// <param name="current">The current DateTime to be changed.</param>
        /// <returns>given <see cref="DateTime"/> with the day part set to the last day in that month.</returns>
        public static DateOnly EndOfMonth(this DateOnly current) => current.SetDay(DateTime.DaysInMonth(current.Year, current.Month));

        /// <summary>
        /// Adds the given number of business days to the <see cref="DateTime"/>.
        /// </summary>
        /// <param name="current">The date to be changed.</param>
        /// <param name="days">Number of business days to be added.</param>
        /// <returns>A <see cref="DateTime"/> increased by a given number of business days.</returns>
        public static DateOnly AddBusinessDays(this DateOnly current, int days)
        {
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                } while (current.DayOfWeek == DayOfWeek.Saturday ||
                         current.DayOfWeek == DayOfWeek.Sunday);
            }
            return current;
        }

        /// <summary>
        /// Subtracts the given number of business days to the <see cref="DateTime"/>.
        /// </summary>
        /// <param name="current">The date to be changed.</param>
        /// <param name="days">Number of business days to be subtracted.</param>
        /// <returns>A <see cref="DateTime"/> increased by a given number of business days.</returns>
        public static DateOnly SubtractBusinessDays(this DateOnly current, int days) => AddBusinessDays(current, -days);

        /// <summary>
        /// Determine if a <see cref="DateTime"/> is in the future.
        /// </summary>
        /// <param name="dateTime">The date to be checked.</param>
        /// <returns><c>true</c> if <paramref name="dateTime"/> is in the future; otherwise <c>false</c>.</returns>
        public static bool IsInFuture(this DateOnly dateTime) => dateTime.At(TimeOnly.MinValue) > DateTime.UtcNow;

        /// <summary>
        /// Determine if a <see cref="DateTime"/> is in the past.
        /// </summary>
        /// <param name="dateTime">The date to be checked.</param>
        /// <returns><c>true</c> if <paramref name="dateTime"/> is in the past; otherwise <c>false</c>.</returns>
        public static bool IsInPast(this DateOnly dateTime) => dateTime.At(TimeOnly.MinValue) < DateTime.UtcNow;

        /// <summary>
        /// Returns a DateTime adjusted to the beginning of the week.
        /// </summary>
        /// <returns>A DateTime instance adjusted to the beginning of the current week</returns>
        /// <remarks>the beginning of the week is controlled by the current Culture</remarks>
        public static DateOnly BeginningOfWeek(this DateOnly dateTime, DayOfWeek? firstDayOfWeek = null)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var firstDay = firstDayOfWeek ?? currentCulture.DateTimeFormat.FirstDayOfWeek;
            var offset = dateTime.DayOfWeek - firstDay < 0 ? 7 : 0;
            var numberOfDaysSinceBeginningOfTheWeek = dateTime.DayOfWeek + offset - firstDay;

            return dateTime.AddDays(-numberOfDaysSinceBeginningOfTheWeek);
        }

        /// <summary>
        /// Returns the first day of the year keeping the time component intact. Eg, 2011-02-04T06:40:20.005 => 2011-01-01T06:40:20.005
        /// </summary>
        /// <param name="current">The DateTime to adjust</param>
        /// <returns></returns>
        public static DateOnly BeginningOfYear(this DateOnly current) => new(current.Year, 1, 1);

        /// <summary>
        /// Returns the last day of the week keeping the time component intact. Eg, 2011-12-24T06:40:20.005 => 2011-12-25T06:40:20.005
        /// </summary>
        /// <returns></returns>
        public static DateOnly EndOfWeek(this DateOnly current, DayOfWeek? firstDayOfWeek = null) => current.BeginningOfWeek(firstDayOfWeek).AddDays(6);

        /// <summary>
        /// Returns the last day of the year keeping the time component intact. Eg, 2011-12-24T06:40:20.005 => 2011-12-31T06:40:20.005
        /// </summary>
        /// <param name="current">The DateTime to adjust</param>
        /// <returns></returns>
        public static DateOnly EndOfYear(this DateOnly current) => new(current.Year, 12, 31);

        public static bool IsLastDayOfWeek(this DateOnly current, DayOfWeek? firstDayOfWeek = null) => current.DayOfWeek == current.EndOfWeek(firstDayOfWeek).DayOfWeek;

        public static bool IsFirstDayOfWeek(this DateOnly current, DayOfWeek? firstDayOfWeek = null) => current.DayOfWeek == current.EndOfWeek(firstDayOfWeek).DayOfWeek;

        public static bool IsWeekend(this DateOnly current) => current.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

        /// <summary>
        /// Returns the previous month keeping the time component intact. Eg, 2010-01-20T06:40:20.005 => 2009-12-20T06:40:20.005
        /// If the previous month doesn't have that many days the last day of the previous month is used. Eg, 2009-03-31T06:40:20.005 => 2009-02-28T06:40:20.005
        /// </summary>
        /// <param name="current">The DateTime to adjust</param>
        /// <returns></returns>
        public static DateOnly PreviousMonth(this DateOnly current)
        {
            var year = current.Month == 1 ? current.Year - 1 : current.Year;

            var month = current.Month == 1 ? 12 : current.Month - 1;

            var firstDayOfPreviousMonth = new DateOnly(year, month, 1);

            var lastDayOfPreviousMonth = firstDayOfPreviousMonth.EndOfMonth().Day;

            var day = current.Day > lastDayOfPreviousMonth ? lastDayOfPreviousMonth : current.Day;

            return firstDayOfPreviousMonth.SetDay(day);
        }

        /// <summary>
        /// Returns the next month keeping the time component intact. Eg, 2012-12-05T06:40:20.005 => 2013-01-05T06:40:20.005
        /// If the next month doesn't have that many days the last day of the next month is used. Eg, 2013-01-31T06:40:20.005 => 2013-02-28T06:40:20.005
        /// </summary>
        /// <param name="current">The DateTime to adjust</param>
        /// <returns></returns>
        public static DateOnly NextMonth(this DateOnly current)
        {
            var year = current.Month == 12 ? current.Year + 1 : current.Year;

            var month = current.Month == 12 ? 1 : current.Month + 1;

            var firstDayOfNextMonth = new DateOnly(year, month, 1);

            var lastDayOfPreviousMonth = firstDayOfNextMonth.EndOfMonth().Day;

            var day = current.Day > lastDayOfPreviousMonth ? lastDayOfPreviousMonth : current.Day;

            return firstDayOfNextMonth.SetDay(day);
        }

        public static bool IsToday(this DateOnly current) => current.Day == DateTime.UtcNow.Day;

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> value is exactly the same month (month + year) then current. Eg, 2015-12-01 and 2014-12-01 => False
        /// </summary>
        /// <param name="current">The current value</param>
        /// <param name="date">Value to compare with</param>
        /// <returns>
        /// 	<c>true</c> if the specified date is exactly the same month and year then current; otherwise, <c>false</c>.
        /// </returns>
        public static bool SameWeek(this DateOnly current, DateOnly date) => date.IsAfter(current.BeginningOfWeek()) && date.IsBefore(current.EndOfWeek());

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> value is exactly the same month (month + year) then current. Eg, 2015-12-01 and 2014-12-01 => False
        /// </summary>
        /// <param name="current">The current value</param>
        /// <param name="date">Value to compare with</param>
        /// <returns>
        /// 	<c>true</c> if the specified date is exactly the same month and year then current; otherwise, <c>false</c>.
        /// </returns>
        public static bool SameMonth(this DateOnly current, DateOnly date) => current.Month == date.Month && current.Year == date.Year;

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> value is exactly the same year then current. Eg, 2015-12-01 and 2015-01-01 => True
        /// </summary>
        /// <param name="current">The current value</param>
        /// <param name="date">Value to compare with</param>
        /// <returns>
        /// 	<c>true</c> if the specified date is exactly the same date then current; otherwise, <c>false</c>.
        /// </returns>
        public static bool SameYear(this DateOnly current, DateOnly date) => current.Year == date.Year;

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> value is exactly the same year then current. Eg, 2012-12-01 and 2015-01-01 => True
        /// </summary>
        /// <param name="current">The current value</param>
        /// <param name="date">Value to compare with</param>
        /// <returns>
        /// 	<c>true</c> if the specified date is exactly the same date then current; otherwise, <c>false</c>.
        /// </returns>
        public static bool SameDecade(this DateOnly current, DateOnly date) => current.BeginningOfDecade() == date.BeginningOfDecade();

        public static int NumberOfDays(this DateOnly dateFrom, DateOnly dateTo) => Math.Abs((dateFrom.ToDateTime(TimeOnly.MinValue) - dateTo.ToDateTime(TimeOnly.MinValue)).Days);

        public static int CompareWeek(this DateOnly dateFrom, DateOnly dateTo)
        {
            var ts = dateTo.ToDateTime(TimeOnly.MinValue).Subtract(dateFrom.ToDateTime(TimeOnly.MinValue));
            return ts.Days / 7;
        }

        public static int NumberOfWeeks(this DateOnly dt1, DateOnly dt2) => Math.Abs(CompareWeek(dt1, dt2));

        /// <summary>
        /// Compare two dates and return difference of month (positif or negatif).
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static int CompareMonth(this DateOnly dt1, DateTime dt2) => ((dt2.Year - dt1.Year) * 12) + (dt2.Month - dt1.Month);

        /// <summary>
        /// Compare two dates and return difference of month.
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static int NumberOfMonths(this DateOnly dt1, DateTime dt2) => Math.Abs(CompareMonth(dt1, dt2));

        /// <summary>
        /// Compare two dates and return difference of month (positif or negatif).
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static int CompareYear(this DateOnly dt1, DateTime dt2) => dt2.Year - dt1.Year;

        /// <summary>
        /// Compare two dates and return difference of month.
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static int NumberOfYears(this DateOnly dt1, DateTime dt2) => Math.Abs(CompareYear(dt1, dt2));

        public static bool InRange(this DateOnly date, DateOnly start, DateOnly end) => date == start || date == end || (date.IsAfter(start) && date.IsBefore(end)) || (date.IsAfter(end) && date.IsBefore(start));

        public static int GetAge(this DateOnly birthdate)
        {
            var today = DateTime.UtcNow;

            var age = today.Year - birthdate.Year;

            if (today.Month < birthdate.Month || today.Month == birthdate.Month && today.Day < birthdate.Day)
                age--;

            return age;
        }
    }
}
