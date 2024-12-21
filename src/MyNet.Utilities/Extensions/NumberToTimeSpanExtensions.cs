// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyNet.Utilities.DateTimes;
using MyNet.Utilities.Helpers;
using MyNet.Utilities.Units;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Number to TimeSpan extensions
    /// </summary>
    public static class NumberToTimeSpanExtensions
    {
        /// <summary>
        /// Generates <see cref="TimeSpan"/> value for given number of Years.
        /// </summary>
        public static FluentTimeSpan Years(this int years) => new() { Years = years };

        /// <summary>
        /// Generates <see cref="TimeSpan"/> value for given number of Quarters.
        /// </summary>
        /// <param name="quarters"></param>
        /// <returns></returns>
        public static FluentTimeSpan Quarters(this int quarters) => new() { Months = quarters * 3 };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> value for given number of Months.
        /// </summary>
        public static FluentTimeSpan Months(this int months) => new() { Months = months };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Weeks (number of weeks * 7).
        /// </summary>
        public static FluentTimeSpan Weeks(this int weeks) => new() { TimeSpan = TimeSpan.FromDays(weeks * 7) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Weeks (number of weeks * 7).
        /// </summary>
        public static FluentTimeSpan Weeks(this double weeks) => new() { TimeSpan = TimeSpan.FromDays(weeks * 7) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Days.
        /// </summary>
        public static FluentTimeSpan Days(this int days) => new() { TimeSpan = TimeSpan.FromDays(days) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Days.
        /// </summary>
        public static FluentTimeSpan Days(this double days) => new() { TimeSpan = TimeSpan.FromDays(days) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Hours.
        /// </summary>
        public static FluentTimeSpan Hours(this int hours) => new() { TimeSpan = TimeSpan.FromHours(hours) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Hours.
        /// </summary>
        public static FluentTimeSpan Hours(this double hours) => new() { TimeSpan = TimeSpan.FromHours(hours) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Minutes.
        /// </summary>
        public static FluentTimeSpan Minutes(this int minutes) => new() { TimeSpan = TimeSpan.FromMinutes(minutes) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Minutes.
        /// </summary>
        public static FluentTimeSpan Minutes(this double minutes) => new() { TimeSpan = TimeSpan.FromMinutes(minutes) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Seconds.
        /// </summary>
        public static FluentTimeSpan Seconds(this int seconds) => new() { TimeSpan = TimeSpan.FromSeconds(seconds) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Seconds.
        /// </summary>
        public static FluentTimeSpan Seconds(this double seconds) => new() { TimeSpan = TimeSpan.FromSeconds(seconds) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Milliseconds.
        /// </summary>
        public static FluentTimeSpan Milliseconds(this int milliseconds) => new() { TimeSpan = TimeSpan.FromMilliseconds(milliseconds) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of Milliseconds.
        /// </summary>
        public static FluentTimeSpan Milliseconds(this double milliseconds) => new() { TimeSpan = TimeSpan.FromMilliseconds(milliseconds) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of ticks.
        /// </summary>
        public static FluentTimeSpan Ticks(this int ticks) => new() { TimeSpan = TimeSpan.FromTicks(ticks) };

        /// <summary>
        /// Returns <see cref="TimeSpan"/> for given number of ticks.
        /// </summary>
        public static FluentTimeSpan Ticks(this long ticks) => new() { TimeSpan = TimeSpan.FromTicks(ticks) };

        public static FluentTimeSpan Unit(this int value, TimeUnit unit) => unit switch
        {
            TimeUnit.Millisecond => new FluentTimeSpan() { TimeSpan = TimeSpan.FromMilliseconds(value) },
            TimeUnit.Second => new FluentTimeSpan() { TimeSpan = TimeSpan.FromSeconds(value) },
            TimeUnit.Minute => new FluentTimeSpan() { TimeSpan = TimeSpan.FromMinutes(value) },
            TimeUnit.Hour => new FluentTimeSpan() { TimeSpan = TimeSpan.FromHours(value) },
            TimeUnit.Day => new FluentTimeSpan() { TimeSpan = TimeSpan.FromDays(value) },
            TimeUnit.Week => new FluentTimeSpan() { TimeSpan = TimeSpan.FromDays(value * DateTimeHelper.NumberOfDaysInWeek()) },
            TimeUnit.Month => new FluentTimeSpan() { Months = value },
            TimeUnit.Year => new FluentTimeSpan() { Years = value },
            _ => new FluentTimeSpan(),
        };
    }
}
