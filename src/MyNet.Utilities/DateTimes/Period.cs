﻿// -----------------------------------------------------------------------
// <copyright file="Period.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Helpers;
using MyNet.Utilities.Localization;
using MyNet.Utilities.Sequences;

namespace MyNet.Utilities.DateTimes;

public class Period(DateTime start, DateTime end) : Interval<DateTime, Period>(start, end)
{
    public TimeSpan Duration => End - Start;

    public IEnumerable<DateTime> ToDates() =>
        Enumerable.Range(0, End.Date.Subtract(Start.Date).Days + 1)
            .Select(offset => Start.Date.AddDays(offset));

    public IEnumerable<Period> ByDays() =>
        Enumerable.Range(0, End.Date.Subtract(Start.Date).Days + 1)
            .Select(offset => Start.Date.AddDays(offset))
            .Select(x => new Period(DateTimeHelper.Max(x.BeginningOfDay(), Start), DateTimeHelper.Min(x.EndOfDay(), End)));

    public bool IsCurrent() => Start.Kind switch
    {
        DateTimeKind.Utc => Contains(DateTime.UtcNow),
        DateTimeKind.Local => Contains(DateTime.Now),
        DateTimeKind.Unspecified => Contains(GlobalizationService.Current.Date),
        _ => Contains(GlobalizationService.Current.Date)
    };

    public Period ToUtc() => new(Start.ToUniversalTime(), End.ToUniversalTime());

    public Period ToLocal() => new(Start.ToLocalTime(), End.ToLocalTime());

    public Period AddAfter(TimeSpan offset) => new(Start, End.AddFluentTimeSpan(offset));

    public Period AddBefore(TimeSpan offset) => new(Start.AddFluentTimeSpan(offset), End);

    public Period SubstractBefore(TimeSpan offset) => new(Start, End.SubtractFluentTimeSpan(offset));

    public Period SubstractAfter(TimeSpan offset) => new(Start.SubtractFluentTimeSpan(offset), End);

    public Period ShiftLater(TimeSpan offset) => new(Start.AddFluentTimeSpan(offset), End.AddFluentTimeSpan(offset));

    public Period ShiftEarlier(TimeSpan offset) => new(Start.SubtractFluentTimeSpan(offset), End.SubtractFluentTimeSpan(offset));

    public IEnumerable<Period> Intersect(TimePeriod interval)
        => ByDays().Select(x => x.Intersect(new Period(x.Start.At(interval.Start), x.Start.At(interval.End)))).NotNull();

    public ImmutablePeriod AsImmutable() => new(Start, End);

    protected override Period CreateInstance(DateTime start, DateTime end) => new(start, end);
}

public class ImmutablePeriod(DateTime start, DateTime end) : Period(start, end)
{
    public override void SetInterval(DateTime start, DateTime end) => throw new InvalidOperationException("This period is immutable.");

    protected override Period CreateInstance(DateTime start, DateTime end) => new ImmutablePeriod(start, end);
}

public class PeriodWithOptionalEnd(DateTime start, DateTime? end = null) : IntervalWithOptionalEnd<DateTime>(start, end)
{
    public TimeSpan? NullableDuration => End is null ? null : End.Value.ToUniversalTime() - Start.ToUniversalTime();

    public TimeSpan Duration => End is null ? DateTime.UtcNow - Start.ToUniversalTime() : End.Value.ToUniversalTime() - Start.ToUniversalTime();
}
