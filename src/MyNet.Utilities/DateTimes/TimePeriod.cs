// -----------------------------------------------------------------------
// <copyright file="TimePeriod.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyNet.Utilities.Sequences;

namespace MyNet.Utilities.DateTimes;

public class TimePeriod(TimeOnly start, TimeOnly end) : Interval<TimeOnly, TimePeriod>(start, end)
{
    public TimeSpan Duration => End - Start;

    public bool IsCurrent() => Contains(DateTime.UtcNow.ToTime());

    public ImmutableTimePeriod AsImmutable() => new(Start, End);

    protected override TimePeriod CreateInstance(TimeOnly start, TimeOnly end) => new(start, end);
}

public class ImmutableTimePeriod(TimeOnly start, TimeOnly end) : TimePeriod(start, end)
{
    public override void SetInterval(TimeOnly start, TimeOnly end) => throw new InvalidOperationException("This period is immutable.");

    protected override TimePeriod CreateInstance(TimeOnly start, TimeOnly end) => new ImmutableTimePeriod(start, end);
}

public class TimePeriodWithOptionalEnd(TimeOnly start, TimeOnly? end = null) : IntervalWithOptionalEnd<TimeOnly>(start, end)
{
    public TimeSpan? NullableDuration => End is null ? null : End.Value - Start;

    public TimeSpan Duration => End is null ? DateTime.UtcNow.ToTime() - Start : End.Value - Start;
}
