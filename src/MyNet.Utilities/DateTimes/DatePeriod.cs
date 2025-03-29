// -----------------------------------------------------------------------
// <copyright file="DatePeriod.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Sequences;

namespace MyNet.Utilities.DateTimes;

public class DatePeriod : Interval<DateOnly, DatePeriod>
{
    public DatePeriod(DateOnly start, DateOnly end)
        : base(start, end) { }

    public TimeSpan Duration => End.At(TimeOnly.MinValue) - Start.At(TimeOnly.MinValue);

    public IEnumerable<DateOnly> ToDays() =>
        Enumerable.Range(0, End.At(TimeOnly.MinValue).Subtract(Start.At(TimeOnly.MinValue)).Days + 1).Select(Start.AddDays);

    public bool IsCurrent() => Start == DateTime.UtcNow.ToDate();

    public ImmutableDatePeriod AsImmutable() => new(Start, End);

    protected override DatePeriod CreateInstance(DateOnly start, DateOnly end) => new(start, end);
}

public class ImmutableDatePeriod : DatePeriod
{
    public ImmutableDatePeriod(DateOnly start, DateOnly end)
        : base(start, end) { }

    public override void SetInterval(DateOnly start, DateOnly end) => throw new InvalidOperationException("This period is immutable.");

    protected override DatePeriod CreateInstance(DateOnly start, DateOnly end) => new ImmutableDatePeriod(start, end);
}

public class DatePeriodWithOptionalEnd : IntervalWithOptionalEnd<DateOnly>
{
    public DatePeriodWithOptionalEnd(DateOnly start, DateOnly? end = null)
        : base(start, end) { }

    public TimeSpan? NullableDuration => End is null ? null : End.Value.At(TimeOnly.MinValue) - Start.At(TimeOnly.MinValue);

    public TimeSpan Duration => End is null ? DateTime.UtcNow - Start.At(TimeOnly.MinValue) : End.Value.At(TimeOnly.MinValue) - Start.At(TimeOnly.MinValue);
}
