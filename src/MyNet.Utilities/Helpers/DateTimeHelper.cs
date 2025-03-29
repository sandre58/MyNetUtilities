// -----------------------------------------------------------------------
// <copyright file="DateTimeHelper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using MyNet.Utilities.Units;

namespace MyNet.Utilities.Helpers;

public static class DateTimeHelper
{
    public static DateTime Max(DateTime date1, DateTime date2) =>
        date1 > date2
            ? date1
            : date2;

    public static DateOnly Max(DateOnly date1, DateOnly date2) =>
        date1 > date2
            ? date1
            : date2;

    public static TimeOnly Max(TimeOnly time1, TimeOnly time2) =>
        time1 > time2
            ? time1
            : time2;

    public static TimeSpan Max(TimeSpan time1, TimeSpan time2) =>
        time1 > time2
            ? time1
            : time2;

    public static DateTime Min(DateTime date1, DateTime date2) =>
        date1 > date2
            ? date2
            : date1;

    public static DateOnly Min(DateOnly date1, DateOnly date2) =>
        date1 > date2
            ? date2
            : date1;

    public static TimeOnly Min(TimeOnly time1, TimeOnly time2) =>
        time1 > time2
            ? time2
            : time1;

    public static TimeSpan Min(TimeSpan time1, TimeSpan time2) =>
        time1 > time2
            ? time2
            : time1;

    public static IEnumerable<DateTime> Range(DateTime min, DateTime max, int step = 1, TimeUnit unit = TimeUnit.Day)
    {
        Func<DateTime, DateTime> increment = unit switch
        {
            TimeUnit.Millisecond => x => x.AddMilliseconds(step),
            TimeUnit.Second => x => x.AddSeconds(step),
            TimeUnit.Minute => x => x.AddMinutes(step),
            TimeUnit.Hour => x => x.AddHours(step),
            TimeUnit.Day => x => x.AddDays(step),
            TimeUnit.Week => x => x.AddDays(step * 7),
            TimeUnit.Month => x => x.AddMonths(step),
            TimeUnit.Year => x => x.AddYears(step),
            _ => null!
        };

        for (var i = min; i <= max; i = increment.Invoke(i))
            yield return i;
    }

    public static IEnumerable<DateOnly> Range(DateOnly min, DateOnly max, int step = 1, TimeUnit unit = TimeUnit.Day)
    {
        Func<DateOnly, DateOnly> increment = unit switch
        {
            TimeUnit.Day => x => x.AddDays(step),
            TimeUnit.Week => x => x.AddDays(step * 7),
            TimeUnit.Month => x => x.AddMonths(step),
            TimeUnit.Year => x => x.AddYears(step),
            _ => x => x.AddDays(step)
        };
        for (var i = min; i <= max; i = increment.Invoke(i))
            yield return i;
    }

    public static IEnumerable<TimeOnly> Range(TimeOnly min, TimeOnly max, int step = 1, TimeUnit unit = TimeUnit.Hour)
    {
        Func<TimeOnly, TimeOnly> increment = unit switch
        {
            TimeUnit.Millisecond => x => x.Add(step.Milliseconds()),
            TimeUnit.Second => x => x.Add(step.Seconds()),
            TimeUnit.Minute => x => x.AddMinutes(step),
            _ => x => x.AddHours(step)
        };
        for (var i = min; i <= max; i = increment.Invoke(i))
            yield return i;
    }

    public static int NumberOfDaysInWeek() => Enum.GetValues<DayOfWeek>().Length;
}
