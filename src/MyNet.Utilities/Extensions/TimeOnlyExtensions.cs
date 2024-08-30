// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities
{
    /// <summary>
    /// <see cref="TimeOnly"/> extensions related to spatial or temporal relations
    /// </summary>
    public static class TimeOnlyExtensions
    {
        /// <summary>
        /// Returns the Start of the given day (the first millisecond of the given <see cref="DateTime"/>).
        /// </summary>
        public static TimeOnly BeginningOfHour(this TimeOnly date) => new(date.Hour, 0, 0, 0);

        /// <summary>
        /// Returns the very end of the given day (the last millisecond of the last hour for the given <see cref="DateTime"/>).
        /// </summary>
        public static TimeOnly EndOfHour(this TimeOnly date) => new(date.Hour, 59, 59, 999);

        /// <summary>
        /// Returns the Start of the given day (the first millisecond of the given <see cref="DateTime"/>).
        /// </summary>
        public static TimeOnly BeginningOfMinute(this TimeOnly date) => new(date.Hour, date.Minute, 0, 0);

        /// <summary>
        /// Returns the very end of the given day (the last millisecond of the last hour for the given <see cref="DateTime"/>).
        /// </summary>
        public static TimeOnly EndOfMinute(this TimeOnly date) => new(date.Hour, date.Minute, 59, 999);

        /// <summary>
        /// Returns <see cref="DateTime"/> with changed Hour part.
        /// </summary>
        public static TimeOnly SetHour(this TimeOnly originalDate, int hour) => new(hour, originalDate.Minute, originalDate.Second, originalDate.Millisecond);

        /// <summary>
        /// Returns <see cref="DateTime"/> with changed Minute part.
        /// </summary>
        public static TimeOnly SetMinute(this TimeOnly originalDate, int minute) => new(originalDate.Hour, minute, originalDate.Second, originalDate.Millisecond);

        /// <summary>
        /// Returns <see cref="DateTime"/> with changed Second part.
        /// </summary>
        public static TimeOnly SetSecond(this TimeOnly originalDate, int second) => new(originalDate.Hour, originalDate.Minute, second, originalDate.Millisecond);

        /// <summary>
        /// Returns <see cref="DateTime"/> with changed Millisecond part.
        /// </summary>
        public static TimeOnly SetMillisecond(this TimeOnly originalDate, int millisecond) => new(originalDate.Hour, originalDate.Minute, originalDate.Second, millisecond);

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> is before then current value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="toCompareWith">Value to compare with.</param>
        /// <returns>
        /// 	<c>true</c> if the specified current is before; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBefore(this TimeOnly current, TimeOnly toCompareWith) => current < toCompareWith;

        /// <summary>
        /// Determines whether the specified <see cref="DateTime"/> value is After then current value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="toCompareWith">Value to compare with.</param>
        /// <returns>
        /// 	<c>true</c> if the specified current is after; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAfter(this TimeOnly current, TimeOnly toCompareWith) => current > toCompareWith;

        public static bool SameMilliSecond(this TimeOnly current, TimeOnly date) => SameSecond(current, date) && current.Millisecond == date.Millisecond;

        public static bool SameSecond(this TimeOnly current, TimeOnly date) => SameMinute(current, date) && current.Second == date.Second;

        public static bool SameMinute(this TimeOnly current, TimeOnly date) => SameHour(current, date) && current.Minute == date.Minute;

        public static bool SameHour(this TimeOnly current, TimeOnly date) => current.Hour == date.Hour;

        public static bool InRange(this TimeOnly date, TimeOnly start, TimeOnly end) => date == start || date == end || (date.IsAfter(start) && date.IsBefore(end)) || (date.IsAfter(end) && date.IsBefore(start));
    }
}
