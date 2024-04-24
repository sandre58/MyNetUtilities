﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyNet.Utilities.DateTimes;
using MyNet.Utilities.Units;

namespace MyNet.Utilities
{
    /// <summary>
    /// Static class containing Fluent <see cref="TimeSpan"/> extension methods.
    /// </summary>
    public static class TimeSpanExtensions
    {
        public const int DaysInAWeek = 7;
        public const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
        public const double DaysInAMonth = DaysInAYear / 12;

        /// <summary>
        /// Adds the given <see cref="FluentTimeSpan"/> from a <see cref="TimeSpan"/> and returns resulting <see cref="FluentTimeSpan"/>.
        /// </summary>
        public static FluentTimeSpan AddFluentTimeSpan(this TimeSpan timeSpan, FluentTimeSpan fluentTimeSpan) => fluentTimeSpan.Add(timeSpan);

        /// <summary>
        /// Subtracts the given <see cref="FluentTimeSpan"/> from a <see cref="TimeSpan"/> and returns resulting <see cref="FluentTimeSpan"/>.
        /// </summary>
        public static FluentTimeSpan SubtractFluentTimeSpan(this TimeSpan timeSpan, FluentTimeSpan fluentTimeSpan) => FluentTimeSpan.SubtractInternal(timeSpan, fluentTimeSpan);

        /// <summary>
        /// Subtracts given <see cref="TimeSpan"/> from current date (<see cref="DateTime.Now"/>) and returns resulting <see cref="DateTime"/> in the past.
        /// </summary>
        public static DateTime Ago(this TimeSpan from) => from.Before(DateTime.Now);

        /// <summary>
        /// Subtracts given <see cref="FluentTimeSpan"/> from current date (<see cref="DateTime.Now"/>) and returns resulting <see cref="DateTime"/> in the past.
        /// </summary>
        public static DateTime Ago(this FluentTimeSpan from) => from.Before(DateTime.Now);

        /// <summary>
        /// Subtracts given <see cref="TimeSpan"/> from <paramref name="originalValue"/> <see cref="DateTime"/> and returns resulting <see cref="DateTime"/> in the past.
        /// </summary>
        public static DateTime Ago(this TimeSpan from, DateTime originalValue) => from.Before(originalValue);

        /// <summary>
        /// Subtracts given <see cref="TimeSpan"/> from <paramref name="originalValue"/> <see cref="DateTime"/> and returns resulting <see cref="DateTime"/> in the past.
        /// </summary>
        public static DateTime Ago(this FluentTimeSpan from, DateTime originalValue) => from.Before(originalValue);

        /// <summary>
        /// Subtracts given <see cref="TimeSpan"/> from <paramref name="originalValue"/> <see cref="DateTime"/> and returns resulting <see cref="DateTime"/> in the past.
        /// </summary>
        public static DateTime Before(this TimeSpan from, DateTime originalValue) => originalValue - from;

        /// <summary>
        /// Subtracts given <see cref="TimeSpan"/> from <paramref name="originalValue"/> <see cref="DateTime"/> and returns resulting <see cref="DateTime"/> in the past.
        /// </summary>
        public static DateTime Before(this FluentTimeSpan from, DateTime originalValue) => originalValue.AddMonths(-from.Months).AddYears(-from.Years).Add(-from.TimeSpan);

        /// <summary>
        /// Adds given <see cref="TimeSpan"/> to current <see cref="DateTime.Now"/> and returns resulting <see cref="DateTime"/> in the future.
        /// </summary>
        public static DateTime FromNow(this TimeSpan from) => from.From(DateTime.Now);

        /// <summary>
        /// Adds given <see cref="TimeSpan"/> to current <see cref="DateTime.Now"/> and returns resulting <see cref="DateTime"/> in the future.
        /// </summary>
        public static DateTime FromNow(this FluentTimeSpan from) => from.From(DateTime.Now);

        /// <summary>
        /// Adds given <see cref="TimeSpan"/> to supplied <paramref name="originalValue"/> <see cref="DateTime"/> and returns resulting <see cref="DateTime"/> in the future.
        /// </summary>
        public static DateTime From(this TimeSpan from, DateTime originalValue) => originalValue + from;

        /// <summary>
        /// Adds given <see cref="TimeSpan"/> to supplied <paramref name="originalValue"/> <see cref="DateTime"/> and returns resulting <see cref="DateTime"/> in the future.
        /// </summary>
        public static DateTime From(this FluentTimeSpan from, DateTime originalValue) => originalValue.AddMonths(from.Months).AddYears(from.Years).Add(from.TimeSpan);

        /// <summary>
        /// Adds given <see cref="TimeSpan"/> to supplied <paramref name="originalValue"/> <see cref="DateTime"/> and returns resulting <see cref="DateTime"/> in the future.
        /// </summary>
        /// <seealso cref="From(TimeSpan, DateTime)"/>
        /// <remarks>
        /// Synonym of <see cref="From(TimeSpan, DateTime)"/> method.
        /// </remarks>
        public static DateTime Since(this TimeSpan from, DateTime originalValue) => From(from, originalValue);

        /// <summary>
        /// Adds given <see cref="TimeSpan"/> to supplied <paramref name="originalValue"/> <see cref="DateTime"/> and returns resulting <see cref="DateTime"/> in the future.
        /// </summary>
        /// <seealso cref="From(FluentTimeSpan, DateTime)"/>
        /// <remarks>
        /// Synonym of <see cref="From(FluentTimeSpan, DateTime)"/> method.
        /// </remarks>
        public static DateTime Since(this FluentTimeSpan from, DateTime originalValue) => From(from, originalValue);

        public static TimeSpan Round(this TimeSpan timeSpan, RoundTo rt)
        {
            TimeSpan rounded;

            switch (rt)
            {
                case RoundTo.Second:
                    {
                        rounded = new TimeSpan(timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                        if (timeSpan.Milliseconds >= 500)
                        {
                            rounded += 1.Seconds();
                        }
                        break;
                    }
                case RoundTo.Minute:
                    {
                        rounded = new TimeSpan(timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, 0);
                        if (timeSpan.Seconds >= 30)
                        {
                            rounded += 1.Minutes();
                        }
                        break;
                    }
                case RoundTo.Hour:
                    {
                        rounded = new TimeSpan(timeSpan.Days, timeSpan.Hours, 0, 0);
                        if (timeSpan.Minutes >= 30)
                        {
                            rounded += 1.Hours();
                        }
                        break;
                    }
                case RoundTo.Day:
                    {
                        rounded = new TimeSpan(timeSpan.Days, 0, 0, 0);
                        if (timeSpan.Hours >= 12)
                        {
                            rounded += 1.Days();
                        }
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            return rounded;
        }

        public static TimeSpan Add(this TimeSpan timespan, int value, TimeUnit timeUnitToGet) => timespan.Add(value.ToTimeSpan(timeUnitToGet));

        public static int To(this TimeSpan timespan, TimeUnit timeUnitToGet) => timeUnitToGet switch
        {
            TimeUnit.Millisecond => (int)timespan.TotalMilliseconds,
            TimeUnit.Second => (int)timespan.TotalSeconds,
            TimeUnit.Minute => (int)timespan.TotalMinutes,
            TimeUnit.Hour => (int)timespan.TotalHours,
            TimeUnit.Day => (int)timespan.TotalDays,
            TimeUnit.Week => (int)Math.Round(timespan.TotalDays / DaysInAWeek),
            TimeUnit.Month => (int)Math.Round(timespan.TotalDays / DaysInAMonth),
            TimeUnit.Year => (int)Math.Round(timespan.TotalDays / DaysInAYear),
            _ => 0,
        };

        public static TimeSpan ToTimeSpan(this int value, TimeUnit timeUnitToGet) => timeUnitToGet switch
        {
            TimeUnit.Millisecond => new TimeSpan(0, 0, 0, 0, value),
            TimeUnit.Second => new TimeSpan(0, 0, 0, value, 0),
            TimeUnit.Minute => new TimeSpan(0, 0, value, 0, 0),
            TimeUnit.Hour => new TimeSpan(0, value, 0, 0, 0),
            TimeUnit.Day => new TimeSpan(value, 0, 0, 0, 0),
            TimeUnit.Week => new TimeSpan(value * DaysInAWeek, 0, 0, 0, 0),
            TimeUnit.Month => new TimeSpan((int)Math.Round(value * DaysInAMonth), 0, 0, 0, 0),
            TimeUnit.Year => new TimeSpan((int)Math.Round(value * DaysInAYear), 0, 0, 0, 0),
            _ => new TimeSpan(),
        };
    }
}