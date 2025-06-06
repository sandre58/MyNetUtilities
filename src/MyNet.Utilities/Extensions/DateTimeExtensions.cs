﻿// -----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using MyNet.Utilities.DateTimes;
using MyNet.Utilities.Helpers;
using MyNet.Utilities.Localization;
using MyNet.Utilities.Units;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// <see cref="DateTime"/> extensions related to spatial or temporal relations.
/// </summary>
public static class DateTimeExtensions
{
    public static DateTime ToTimeZone(this DateTime dateTime, TimeZoneInfo timeZoneInfo) => TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);

    public static DateTime ToLocalTime(this DateTime date, TimeSpan time) => date.ToLocalTime().BeginningOfDay().Add(time);

    public static DateTime ToUniversalTime(this DateTime date, TimeSpan time) => date.ToLocalTime().BeginningOfDay().Add(time).ToUniversalTime();

    public static DateTime ToCurrentTime(this DateTime dateTime) => TimeZoneInfo.ConvertTime(dateTime, GlobalizationService.Current.TimeZone);

    public static DateOnly ToDate(this DateTime dateTime) => DateOnly.FromDateTime(dateTime);

    public static TimeOnly ToTime(this DateTime dateTime) => TimeOnly.FromDateTime(dateTime);

    public static Period ToPeriod(this DateTime dateTime, FluentTimeSpan timeSpan) => new(dateTime, dateTime.AddFluentTimeSpan(timeSpan));

    public static Period ToPeriod(this DateTime dateTime, DateTime otherDateTime) => new(DateTimeHelper.Min(dateTime, otherDateTime), DateTimeHelper.Max(dateTime, otherDateTime));

    public static DateTime Add(this DateTime date, int value, TimeUnit timeUnitToGet) => date.AddFluentTimeSpan(value.ToTimeSpan(timeUnitToGet));

    /// <summary>
    /// Returns a new <see cref="DateTime"/> that adds the value of the specified <see cref="FluentTimeSpan"/> to the value of this instance.
    /// </summary>
    public static DateTime AddFluentTimeSpan(this DateTime dateTime, FluentTimeSpan timeSpan) => dateTime.AddMonths(timeSpan.Months)
        .AddYears(timeSpan.Years)
        .Add(timeSpan.TimeSpan);

    /// <summary>
    /// Returns a new <see cref="DateTime"/> that subtracts the value of the specified <see cref="FluentTimeSpan"/> to the value of this instance.
    /// </summary>
    public static DateTime SubtractFluentTimeSpan(this DateTime dateTime, FluentTimeSpan timeSpan) => dateTime.AddMonths(-timeSpan.Months)
        .AddYears(-timeSpan.Years)
        .Subtract(timeSpan.TimeSpan);

    /// <summary>
    /// Returns the very end of the given day (the last millisecond of the last hour for the given <see cref="DateTime"/>).
    /// </summary>
    public static DateTime EndOfHour(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999, date.Kind);

    /// <summary>
    /// Returns the very end of the given day (the last millisecond of the last hour for the given <see cref="DateTime"/>).
    /// </summary>
    public static DateTime EndOfDay(this DateTime date) => new(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);

    /// <summary>
    /// Returns the timezone-adjusted very end of the given day (the last millisecond of the last hour for the given <see cref="DateTime"/>).
    /// </summary>
    public static DateTime EndOfDay(this DateTime date, int timeZoneOffset) => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind)
        .AddHours(timeZoneOffset);

    /// <summary>
    /// Returns the last day of the week changing the time to the very end of the day. Eg, 2011-12-24T06:40:20.005 => 2011-12-25T23:59:59.999.
    /// </summary>
    public static DateTime EndOfWeek(this DateTime date, DayOfWeek? firstDayOfWeek = null) => date.LastDayOfWeek(firstDayOfWeek).EndOfDay();

    /// <summary>
    /// Returns the last day of the week changing the time to the very end of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2011-12-25T23:59:59.999.
    /// </summary>
    public static DateTime EndOfWeek(this DateTime date, int timeZoneOffset, DayOfWeek? firstDayOfWeek = null) => date.LastDayOfWeek(firstDayOfWeek).EndOfDay(timeZoneOffset);

    /// <summary>
    /// Returns the last day of the month changing the time to the very end of the day. Eg, 2011-12-24T06:40:20.005 => 2011-12-31T23:59:59.999.
    /// </summary>
    public static DateTime EndOfMonth(this DateTime date) => date.LastDayOfMonth().EndOfDay();

    /// <summary>
    /// Returns the last day of the month changing the time to the very end of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2011-12-31T23:59:59.999.
    /// </summary>
    public static DateTime EndOfMonth(this DateTime date, int timeZoneOffset) => date.LastDayOfMonth().EndOfDay(timeZoneOffset);

    /// <summary>
    /// Returns the last day of the quarter changing the time to the very end of the day. Eg, 2011-12-24T06:40:20.005 => 2011-12-31T23:59:59.999.
    /// </summary>
    public static DateTime EndOfQuarter(this DateTime date) => date.LastDayOfQuarter().EndOfDay();

    /// <summary>
    /// Returns the last day of the quarter changing the time to the very end of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2011-12-31T23:59:59.999.
    /// </summary>
    public static DateTime EndOfQuarter(this DateTime date, int timeZoneOffset) => date.LastDayOfQuarter().EndOfDay(timeZoneOffset);

    /// <summary>
    /// Returns the last day of the year changing the time to the very end of the day. Eg, 2011-12-24T06:40:20.005 => 2011-12-31T23:59:59.999.
    /// </summary>
    public static DateTime EndOfYear(this DateTime date) => date.LastDayOfYear().EndOfDay();

    /// <summary>
    /// Returns the last day of the year changing the time to the very end of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2011-12-31T23:59:59.999.
    /// </summary>
    public static DateTime EndOfYear(this DateTime date, int timeZoneOffset) => date.LastDayOfYear().EndOfDay(timeZoneOffset);

    /// <summary>
    /// Returns the last day of the year changing the time to the very end of the day. Eg, 2011-12-24T06:40:20.005 => 2020-12-31T23:59:59.999.
    /// </summary>
    public static DateTime EndOfDecade(this DateTime date) => date.SetYear(date.Year - (date.Year % 10) + 9).EndOfYear();

    /// <summary>
    /// Returns the Start of the given day (the first millisecond of the given <see cref="DateTime"/>).
    /// </summary>
    public static DateTime BeginningOfHour(this DateTime date) => new(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Kind);

    /// <summary>
    /// Returns the Start of the given day (the first millisecond of the given <see cref="DateTime"/>).
    /// </summary>
    public static DateTime BeginningOfDay(this DateTime date) => new(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind);

    /// <summary>
    /// Returns the timezone-adjusted Start of the given day (the first millisecond of the given <see cref="DateTime"/>).
    /// </summary>
    public static DateTime BeginningOfDay(this DateTime date, int timezoneOffset) => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind)
        .AddHours(timezoneOffset);

    /// <summary>
    /// Returns the Start day of the week changing the time to the very start of the day. Eg, 2011-12-24T06:40:20.005 => 2011-12-19T00:00:00.000. <see cref="DateTime"/>.
    /// </summary>
    public static DateTime BeginningOfWeek(this DateTime date, DayOfWeek? firstDayOfWeek = null) => date.FirstDayOfWeek(firstDayOfWeek).BeginningOfDay();

    /// <summary>
    /// Returns the Start day of the week changing the time to the very start of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2011-12-19T00:00:00.000. <see cref="DateTime"/>.
    /// </summary>
    public static DateTime BeginningOfWeek(this DateTime date, int timezoneOffset, DayOfWeek? firstDayOfWeek = null) => date.FirstDayOfWeek(firstDayOfWeek).BeginningOfDay(timezoneOffset);

    /// <summary>
    /// Returns the Start day of the month changing the time to the very start of the day. Eg, 2011-12-24T06:40:20.005 => 2011-12-01T00:00:00.000. <see cref="DateTime"/>.
    /// </summary>
    public static DateTime BeginningOfMonth(this DateTime date) => date.FirstDayOfMonth().BeginningOfDay();

    /// <summary>
    /// Returns the Start day of the month changing the time to the very start of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2011-12-01T00:00:00.000. <see cref="DateTime"/>.
    /// </summary>
    public static DateTime BeginningOfMonth(this DateTime date, int timezoneOffset) => date.FirstDayOfMonth().BeginningOfDay(timezoneOffset);

    /// <summary>
    /// Returns the Start day of the quarter changing the time to the very start of the day. Eg, 2011-12-24T06:40:20.005 => 2011-10-01T00:00:00.000. <see cref="DateTime"/>.
    /// </summary>
    public static DateTime BeginningOfQuarter(this DateTime date) => date.FirstDayOfQuarter().BeginningOfDay();

    /// <summary>
    /// Returns the Start day of the quarter changing the time to the very start of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2011-10-01T00:00:00.000. <see cref="DateTime"/>.
    /// </summary>
    public static DateTime BeginningOfQuarter(this DateTime date, int timezoneOffset) => date.FirstDayOfQuarter().BeginningOfDay(timezoneOffset);

    /// <summary>
    /// Returns the Start day of the year changing the time to the very start of the day. Eg, 2011-12-24T06:40:20.005 => 2011-01-01T00:00:00.000. <see cref="DateTime"/>.
    /// </summary>
    public static DateTime BeginningOfYear(this DateTime date) => date.FirstDayOfYear().BeginningOfDay();

    /// <summary>
    /// Returns the Start day of the year changing the time to the very start of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2011-01-01T00:00:00.000. <see cref="DateTime"/>.
    /// </summary>
    public static DateTime BeginningOfYear(this DateTime date, int timezoneOffset) => date.FirstDayOfYear().BeginningOfDay(timezoneOffset);

    /// <summary>
    /// Returns the Start day of the year changing the time to the very start of the day with timezone-adjusted. Eg, 2011-12-24T06:40:20.005 => 2010-01-01T00:00:00.000. <see cref="DateTime"/>.
    /// </summary>
    public static DateTime BeginningOfDecade(this DateTime date) => date.SetYear(date.Year - (date.Year % 10)).BeginningOfYear();

    /// <summary>
    /// Returns the same date (same Day, Month, Hour, Minute, Second etc.) in the next calendar year.
    /// If that day does not exist in next year in same month, number of missing days is added to the last day in same month next year.
    /// </summary>
    public static DateTime NextYear(this DateTime start)
    {
        var nextYear = start.Year + 1;
        var numberOfDaysInSameMonthNextYear = DateTime.DaysInMonth(nextYear, start.Month);

        if (numberOfDaysInSameMonthNextYear >= start.Day)
            return new DateTime(nextYear, start.Month, start.Day, start.Hour, start.Minute, start.Second, start.Millisecond, start.Kind);

        var differenceInDays = start.Day - numberOfDaysInSameMonthNextYear;
        var dateTime = new DateTime(nextYear, start.Month, numberOfDaysInSameMonthNextYear, start.Hour, start.Minute, start.Second, start.Millisecond, start.Kind);
        return dateTime + differenceInDays.Days();
    }

    /// <summary>
    /// Returns the same date (same Day, Month, Hour, Minute, Second etc.) in the previous calendar year.
    /// If that day does not exist in previous year in same month, number of missing days is added to the last day in same month previous year.
    /// </summary>
    public static DateTime PreviousYear(this DateTime start)
    {
        var previousYear = start.Year - 1;
        var numberOfDaysInSameMonthPreviousYear = DateTime.DaysInMonth(previousYear, start.Month);

        if (numberOfDaysInSameMonthPreviousYear >= start.Day)
            return new DateTime(previousYear, start.Month, start.Day, start.Hour, start.Minute, start.Second, start.Millisecond, start.Kind);

        var differenceInDays = start.Day - numberOfDaysInSameMonthPreviousYear;
        var dateTime = new DateTime(previousYear, start.Month, numberOfDaysInSameMonthPreviousYear, start.Hour, start.Minute, start.Second, start.Millisecond, start.Kind);
        return dateTime + differenceInDays.Days();
    }

    /// <summary>
    /// Returns <see cref="DateTime"/> increased by 24 hours ie Next Day.
    /// </summary>
    public static DateTime NextDay(this DateTime start) => start + 1.Days();

    /// <summary>
    /// Returns <see cref="DateTime"/> decreased by 24h period ie Previous Day.
    /// </summary>
    public static DateTime PreviousDay(this DateTime start) => start - 1.Days();

    /// <summary>
    /// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
    /// </summary>
    public static DateTime Next(this DateTime start, DayOfWeek day)
    {
        do
        {
            start = start.NextDay();
        }
        while (start.DayOfWeek != day);

        return start;
    }

    /// <summary>
    /// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
    /// </summary>
    public static DateTime Next(this DateTime start, int day, int month)
    {
        do
        {
            start = start.NextDay();
        }
        while (start.Month != month || start.Day != day);

        return start;
    }

    /// <summary>
    /// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
    /// </summary>
    public static DateTime Previous(this DateTime start, DayOfWeek day)
    {
        do
        {
            start = start.PreviousDay();
        }
        while (start.DayOfWeek != day);

        return start;
    }

    /// <summary>
    /// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
    /// </summary>
    public static DateTime Previous(this DateTime start, int day, int month)
    {
        do
        {
            start = start.PreviousDay();
        }
        while (start.Month != month || start.Day != day);

        return start;
    }

    /// <summary>
    /// Increases supplied <see cref="DateTime"/> for 7 days ie returns the Next Week.
    /// </summary>
    public static DateTime WeekAfter(this DateTime start) => start + 1.Weeks();

    /// <summary>
    /// Decreases supplied <see cref="DateTime"/> for 7 days ie returns the Previous Week.
    /// </summary>
    public static DateTime WeekEarlier(this DateTime start) => start - 1.Weeks();

    /// <summary>
    /// Increases the <see cref="DateTime"/> object with given <see cref="TimeSpan"/> value.
    /// </summary>
    public static DateTime IncreaseTime(this DateTime startDate, TimeSpan toAdd) => startDate + toAdd;

    /// <summary>
    /// Decreases the <see cref="DateTime"/> object with given <see cref="TimeSpan"/> value.
    /// </summary>
    public static DateTime DecreaseTime(this DateTime startDate, TimeSpan toSubtract) => startDate - toSubtract;

    /// <summary>
    /// Returns the original <see cref="DateTime"/> with Hour part changed to supplied hour parameter.
    /// </summary>
    public static DateTime At(this DateTime originalDate, TimeOnly time) => new(originalDate.Year, originalDate.Month, originalDate.Day, time.Hour, time.Minute, time.Second, time.Minute, originalDate.Kind);

    /// <summary>
    /// Returns the original <see cref="DateTime"/> with Hour part changed to supplied hour parameter.
    /// </summary>
    public static DateTime At(this DateTime originalDate, TimeSpan time) => new(originalDate.Year, originalDate.Month, originalDate.Day, time.Hours, time.Minutes, time.Seconds, time.Milliseconds, originalDate.Kind);

    /// <summary>
    /// Returns the original <see cref="DateTime"/> with Hour and Minute parts changed to supplied hour and minute parameters.
    /// </summary>
    public static DateTime At(this DateTime originalDate, int hour, int minute) => new(originalDate.Year, originalDate.Month, originalDate.Day, hour, minute, 0, 0, originalDate.Kind);

    /// <summary>
    /// Returns the original <see cref="DateTime"/> with Hour, Minute and Second parts changed to supplied hour, minute and second parameters.
    /// </summary>
    public static DateTime At(this DateTime originalDate, int hour, int minute, int second) => new(originalDate.Year, originalDate.Month, originalDate.Day, hour, minute, second, 0, originalDate.Kind);

    /// <summary>
    /// Returns the original <see cref="DateTime"/> with Hour, Minute, Second and Millisecond parts changed to supplied hour, minute, second and millisecond parameters.
    /// </summary>
    public static DateTime At(this DateTime originalDate, int hour, int minute, int second, int millisecond) => new(originalDate.Year, originalDate.Month, originalDate.Day, hour, minute, second, millisecond, originalDate.Kind);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Hour part.
    /// </summary>
    public static DateTime SetHour(this DateTime originalDate, int hour) => new(originalDate.Year, originalDate.Month, originalDate.Day, hour, originalDate.Minute, originalDate.Second, originalDate.Millisecond, originalDate.Kind);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Minute part.
    /// </summary>
    public static DateTime SetMinute(this DateTime originalDate, int minute) => new(originalDate.Year, originalDate.Month, originalDate.Day, originalDate.Hour, minute, originalDate.Second, originalDate.Millisecond, originalDate.Kind);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Second part.
    /// </summary>
    public static DateTime SetSecond(this DateTime originalDate, int second) => new(originalDate.Year, originalDate.Month, originalDate.Day, originalDate.Hour, originalDate.Minute, second, originalDate.Millisecond, originalDate.Kind);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Millisecond part.
    /// </summary>
    public static DateTime SetMillisecond(this DateTime originalDate, int millisecond) => new(originalDate.Year, originalDate.Month, originalDate.Day, originalDate.Hour, originalDate.Minute, originalDate.Second, millisecond, originalDate.Kind);

    /// <summary>
    /// Returns original <see cref="DateTime"/> value with time part set to midnight (alias for <see cref="BeginningOfDay(DateTime)"/> method).
    /// </summary>
    public static DateTime Midnight(this DateTime value) => value.BeginningOfDay();

    /// <summary>
    /// Returns original <see cref="DateTime"/> value with time part set to Noon (12:00:00h).
    /// </summary>
    /// <param name="value">The <see cref="DateTime"/> find Noon for.</param>
    /// <returns>A <see cref="DateTime"/> value with time part set to Noon (12:00:00h).</returns>
    public static DateTime Noon(this DateTime value) => value.At(12, 0, 0, 0);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Year part.
    /// </summary>
    public static DateTime SetDate(this DateTime value, int year) => new(year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond, value.Kind);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Year and Month part.
    /// </summary>
    public static DateTime SetDate(this DateTime value, int year, int month) => new(year, month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond, value.Kind);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Year, Month and Day part.
    /// </summary>
    public static DateTime SetDate(this DateTime value, int year, int month, int day) => new(year, month, day, value.Hour, value.Minute, value.Second, value.Millisecond, value.Kind);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Year part.
    /// </summary>
    public static DateTime SetYear(this DateTime value, int year) => new(year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond, value.Kind);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Month part.
    /// </summary>
    public static DateTime SetMonth(this DateTime value, int month) => new(value.Year, month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond, value.Kind);

    /// <summary>
    /// Returns <see cref="DateTime"/> with changed Day part.
    /// </summary>
    public static DateTime SetDay(this DateTime value, int day) => new(value.Year, value.Month, day, value.Hour, value.Minute, value.Second, value.Millisecond, value.Kind);

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> is before then current value.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="toCompareWith">Value to compare with.</param>
    /// <returns>
    ///     <c>true</c> if the specified current is before; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsBefore(this DateTime current, DateTime toCompareWith) => current < toCompareWith;

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is After then current value.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="toCompareWith">Value to compare with.</param>
    /// <returns>
    ///     <c>true</c> if the specified current is after; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsAfter(this DateTime current, DateTime toCompareWith) => current > toCompareWith;

    public static bool IsBetween(this DateTime current, DateTime toCompareFrom, DateTime toCompareTo) => current >= DateTimeHelper.Min(toCompareFrom, toCompareTo) && current <= DateTimeHelper.Max(toCompareTo, toCompareFrom);

    /// <summary>
    /// Sets the day of the <see cref="DateTime"/> to the first day in that calendar quarter.
    /// credit to http://www.devcurry.com/2009/05/find-first-and-last-day-of-current.html.
    /// </summary>
    /// <returns>given <see cref="DateTime"/> with the day part set to the first day in the quarter.</returns>
    public static DateTime FirstDayOfQuarter(this DateTime current)
    {
        var currentQuarter = ((current.Month - 1) / 3) + 1;
        var firstDay = new DateTime(current.Year, (3 * currentQuarter) - 2, 1, 0, 0, 0, current.Kind);

        return current.SetDate(firstDay.Year, firstDay.Month, firstDay.Day);
    }

    /// <summary>
    /// Sets the day of the <see cref="DateTime"/> to the first day in that month.
    /// </summary>
    /// <param name="current">The current <see cref="DateTime"/> to be changed.</param>
    /// <returns>given <see cref="DateTime"/> with the day part set to the first day in that month.</returns>
    public static DateTime FirstDayOfMonth(this DateTime current) => current.SetDay(1);

    /// <summary>
    /// Sets the day of the <see cref="DateTime"/> to the last day in that calendar quarter.
    /// credit to http://www.devcurry.com/2009/05/find-first-and-last-day-of-current.html.
    /// </summary>
    /// <returns>given <see cref="DateTime"/> with the day part set to the last day in the quarter.</returns>
    public static DateTime LastDayOfQuarter(this DateTime current)
    {
        var currentQuarter = ((current.Month - 1) / 3) + 1;
        var firstDay = current.SetDate(current.Year, (3 * currentQuarter) - 2, 1);
        return firstDay.SetMonth(firstDay.Month + 2).LastDayOfMonth();
    }

    /// <summary>
    /// Sets the day of the <see cref="DateTime"/> to the last day in that month.
    /// </summary>
    /// <param name="current">The current DateTime to be changed.</param>
    /// <returns>given <see cref="DateTime"/> with the day part set to the last day in that month.</returns>
    public static DateTime LastDayOfMonth(this DateTime current) => current.SetDay(DateTime.DaysInMonth(current.Year, current.Month));

    /// <summary>
    /// Adds the given number of business days to the <see cref="DateTime"/>.
    /// </summary>
    /// <param name="current">The date to be changed.</param>
    /// <param name="days">Number of business days to be added.</param>
    /// <returns>A <see cref="DateTime"/> increased by a given number of business days.</returns>
    public static DateTime AddBusinessDays(this DateTime current, int days)
    {
        var sign = Math.Sign(days);
        var unsignedDays = Math.Abs(days);
        for (var i = 0; i < unsignedDays; i++)
        {
            do
            {
                current = current.AddDays(sign);
            }
            while (current.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday);
        }

        return current;
    }

    /// <summary>
    /// Subtracts the given number of business days to the <see cref="DateTime"/>.
    /// </summary>
    /// <param name="current">The date to be changed.</param>
    /// <param name="days">Number of business days to be subtracted.</param>
    /// <returns>A <see cref="DateTime"/> increased by a given number of business days.</returns>
    public static DateTime SubtractBusinessDays(this DateTime current, int days) => AddBusinessDays(current, -days);

    /// <summary>
    /// Determine if a <see cref="DateTime"/> is in the future.
    /// </summary>
    /// <param name="dateTime">The date to be checked.</param>
    /// <returns><c>true</c> if <paramref name="dateTime"/> is in the future; otherwise <c>false</c>.</returns>
    public static bool IsInFuture(this DateTime dateTime) => dateTime.ToUniversalTime() > DateTime.UtcNow;

    /// <summary>
    /// Determine if a <see cref="DateTime"/> is in the past.
    /// </summary>
    /// <param name="dateTime">The date to be checked.</param>
    /// <returns><c>true</c> if <paramref name="dateTime"/> is in the past; otherwise <c>false</c>.</returns>
    public static bool IsInPast(this DateTime dateTime) => dateTime.ToUniversalTime() < DateTime.UtcNow;

    /// <summary>
    /// Rounds <paramref name="dateTime"/> to the nearest <see cref="RoundTo"/>.
    /// </summary>
    /// <returns>The rounded <see cref="DateTime"/>.</returns>
    public static DateTime Round(this DateTime dateTime, RoundTo rt)
    {
        DateTime rounded;

        switch (rt)
        {
            case RoundTo.Second:
                {
                    rounded = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
                    if (dateTime.Millisecond >= 500)
                    {
                        rounded = rounded.AddSeconds(1);
                    }

                    break;
                }

            case RoundTo.Minute:
                {
                    rounded = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, dateTime.Kind);
                    if (dateTime.Second >= 30)
                    {
                        rounded = rounded.AddMinutes(1);
                    }

                    break;
                }

            case RoundTo.Hour:
                {
                    rounded = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, dateTime.Kind);
                    if (dateTime.Minute >= 30)
                    {
                        rounded = rounded.AddHours(1);
                    }

                    break;
                }

            case RoundTo.Day:
                {
                    rounded = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind);
                    if (dateTime.Hour >= 12)
                    {
                        rounded = rounded.AddDays(1);
                    }

                    break;
                }

            default:
                {
                    throw new ArgumentOutOfRangeException(nameof(rt));
                }
        }

        return rounded;
    }

    /// <summary>
    /// Returns a DateTime adjusted to the beginning of the week.
    /// </summary>
    /// <returns>A DateTime instance adjusted to the beginning of the current week.</returns>
    /// <remarks>the beginning of the week is controlled by the current Culture.</remarks>
    public static DateTime FirstDayOfWeek(this DateTime dateTime, DayOfWeek? firstDayOfWeek = null)
    {
        var currentCulture = CultureInfo.CurrentCulture;
        var firstDay = firstDayOfWeek ?? currentCulture.DateTimeFormat.FirstDayOfWeek;
        var offset = dateTime.DayOfWeek - firstDay < 0 ? 7 : 0;
        var numberOfDaysSinceBeginningOfTheWeek = dateTime.DayOfWeek + offset - firstDay;

        return dateTime.AddDays(-numberOfDaysSinceBeginningOfTheWeek);
    }

    /// <summary>
    /// Returns the first day of the year keeping the time component intact. Eg, 2011-02-04T06:40:20.005 => 2011-01-01T06:40:20.005.
    /// </summary>
    /// <param name="current">The DateTime to adjust.</param>
    public static DateTime FirstDayOfYear(this DateTime current) => current.SetDate(current.Year, 1, 1);

    /// <summary>
    /// Returns the last day of the week keeping the time component intact. Eg, 2011-12-24T06:40:20.005 => 2011-12-25T06:40:20.005.
    /// </summary>
    public static DateTime LastDayOfWeek(this DateTime current, DayOfWeek? firstDayOfWeek = null) => current.FirstDayOfWeek(firstDayOfWeek).AddDays(6);

    /// <summary>
    /// Returns the last day of the year keeping the time component intact. Eg, 2011-12-24T06:40:20.005 => 2011-12-31T06:40:20.005.
    /// </summary>
    /// <param name="current">The DateTime to adjust.</param>
    public static DateTime LastDayOfYear(this DateTime current) => current.SetDate(current.Year, 12, 31);

    public static bool IsLastDayOfWeek(this DateTime current, DayOfWeek? firstDayOfWeek = null) => current.DayOfWeek == current.LastDayOfWeek(firstDayOfWeek).DayOfWeek;

    public static bool IsFirstDayOfWeek(this DateTime current, DayOfWeek? firstDayOfWeek = null) => current.DayOfWeek == current.FirstDayOfWeek(firstDayOfWeek).DayOfWeek;

    public static bool IsWeekend(this DateTime current) => current.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>
    /// Returns the previous month keeping the time component intact. Eg, 2010-01-20T06:40:20.005 => 2009-12-20T06:40:20.005
    /// If the previous month doesn't have that many days the last day of the previous month is used. Eg, 2009-03-31T06:40:20.005 => 2009-02-28T06:40:20.005.
    /// </summary>
    /// <param name="current">The DateTime to adjust.</param>
    public static DateTime PreviousMonth(this DateTime current)
    {
        var year = current.Month == 1 ? current.Year - 1 : current.Year;

        var month = current.Month == 1 ? 12 : current.Month - 1;

        var firstDayOfPreviousMonth = current.SetDate(year, month, 1);

        var lastDayOfPreviousMonth = firstDayOfPreviousMonth.LastDayOfMonth().Day;

        var day = current.Day > lastDayOfPreviousMonth ? lastDayOfPreviousMonth : current.Day;

        return firstDayOfPreviousMonth.SetDay(day);
    }

    /// <summary>
    /// Returns the next month keeping the time component intact. Eg, 2012-12-05T06:40:20.005 => 2013-01-05T06:40:20.005
    /// If the next month doesn't have that many days the last day of the next month is used. Eg, 2013-01-31T06:40:20.005 => 2013-02-28T06:40:20.005.
    /// </summary>
    /// <param name="current">The DateTime to adjust.</param>
    public static DateTime NextMonth(this DateTime current)
    {
        var year = current.Month == 12 ? current.Year + 1 : current.Year;

        var month = current.Month == 12 ? 1 : current.Month + 1;

        var firstDayOfNextMonth = current.SetDate(year, month, 1);

        var lastDayOfPreviousMonth = firstDayOfNextMonth.LastDayOfMonth().Day;

        var day = current.Day > lastDayOfPreviousMonth ? lastDayOfPreviousMonth : current.Day;

        return firstDayOfNextMonth.SetDay(day);
    }

    public static bool IsToday(this DateTime current) => current.Kind switch
    {
        DateTimeKind.Utc => current.SameDay(DateTime.UtcNow),
        DateTimeKind.Local => current.SameDay(DateTime.Now),
        DateTimeKind.Unspecified => current.SameDay(GlobalizationService.Current.Date),
        _ => current.SameDay(GlobalizationService.Current.Date)
    };

    public static bool SameMilliSecond(this DateTime current, DateTime date) => SameSecond(current, date) && current.Millisecond == date.Millisecond;

    public static bool SameSecond(this DateTime current, DateTime date) => SameMinute(current, date) && current.Second == date.Second;

    public static bool SameMinute(this DateTime current, DateTime date) => SameHour(current, date) && current.Minute == date.Minute;

    public static bool SameHour(this DateTime current, DateTime date) => SameDay(current, date) && current.Hour == date.Hour;

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is exactly the same day (day + month + year) then current.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="date">Value to compare with.</param>
    /// <returns>
    ///     <c>true</c> if the specified date is exactly the same year then current; otherwise, <c>false</c>.
    /// </returns>
    public static bool SameDay(this DateTime current, DateTime date) => current.Date == date.Date;

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is exactly the same month (month + year) then current. Eg, 2015-12-01 and 2014-12-01 => False.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="date">Value to compare with.</param>
    /// <returns>
    ///     <c>true</c> if the specified date is exactly the same month and year then current; otherwise, <c>false</c>.
    /// </returns>
    public static bool SameWeek(this DateTime current, DateTime date) => date.IsAfter(current.BeginningOfWeek()) && date.IsBefore(current.EndOfWeek());

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is exactly the same month (month + year) then current. Eg, 2015-12-01 and 2014-12-01 => False.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="date">Value to compare with.</param>
    /// <returns>
    ///     <c>true</c> if the specified date is exactly the same month and year then current; otherwise, <c>false</c>.
    /// </returns>
    public static bool SameMonth(this DateTime current, DateTime date) => current.Month == date.Month && current.Year == date.Year;

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is exactly the same year then current. Eg, 2015-12-01 and 2015-01-01 => True.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="date">Value to compare with.</param>
    /// <returns>
    ///     <c>true</c> if the specified date is exactly the same date then current; otherwise, <c>false</c>.
    /// </returns>
    public static bool SameYear(this DateTime current, DateTime date) => current.Year == date.Year;

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> value is exactly the same year then current. Eg, 2012-12-01 and 2015-01-01 => True.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="date">Value to compare with.</param>
    /// <returns>
    ///     <c>true</c> if the specified date is exactly the same date then current; otherwise, <c>false</c>.
    /// </returns>
    public static bool SameDecade(this DateTime current, DateTime date) => current.BeginningOfDecade() == date.BeginningOfDecade();

    public static int NumberOfDays(this DateTime dateFrom, DateTime dateTo) => Math.Abs((dateFrom - dateTo).Days);

    public static int CompareWeek(this DateTime dateFrom, DateTime dateTo)
    {
        var ts = dateTo.Subtract(dateFrom);
        return ts.Days / 7;
    }

    public static int NumberOfWeeks(this DateTime dt1, DateTime dt2) => Math.Abs(CompareWeek(dt1, dt2));

    /// <summary>
    /// Compare two dates and return difference of month (positive or negative).
    /// </summary>
    public static int CompareMonth(this DateTime dt1, DateTime dt2) => ((dt2.Year - dt1.Year) * 12) + (dt2.Month - dt1.Month);

    /// <summary>
    /// Compare two dates and return difference of month.
    /// </summary>
    public static int NumberOfMonths(this DateTime dt1, DateTime dt2) => Math.Abs(CompareMonth(dt1, dt2));

    /// <summary>
    /// Compare two dates and return difference of month (positive or negative).
    /// </summary>
    public static int CompareYear(this DateTime dt1, DateTime dt2) => dt2.Year - dt1.Year;

    /// <summary>
    /// Compare two dates and return difference of month.
    /// </summary>
    public static int NumberOfYears(this DateTime dt1, DateTime dt2) => Math.Abs(CompareYear(dt1, dt2));

    public static DateTime DiscardDayTime(this DateTime d) => new(d.Year, d.Month, 1, 0, 0, 0, d.Kind);

    public static DateTime DiscardTime(this DateTime d) => d.Date;

    public static bool InRange(this DateTime date, DateTime start, DateTime end, bool discardTime = true) => (discardTime && (date.SameDay(start) || date.SameDay(end))) || (date.IsAfter(start) && date.IsBefore(end)) || (date.IsAfter(end) && date.IsBefore(start));

    public static int GetAge(this DateTime birthdate)
    {
        var today = DateTime.UtcNow;

        var age = today.Year - birthdate.Year;

        if (today.Month < birthdate.Month || (today.Month == birthdate.Month && today.Day < birthdate.Day))
            age--;

        return age;
    }
}
