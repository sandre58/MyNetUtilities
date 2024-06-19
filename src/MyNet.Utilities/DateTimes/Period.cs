// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Sequences;

namespace MyNet.Utilities.DateTimes
{
    public class Period : Interval<DateTime>
    {
        public Period(DateTime start, DateTime end) : base(start, end) { }

        public TimeSpan Duration => End - Start;

        public IEnumerable<DateTime> ToDates() =>
            Enumerable.Range(0, End.Date.Subtract(Start.Date).Days + 1)
                      .Select(offset => Start.Date.AddDays(offset));

        public bool IsCurrent() => Contains(DateTime.Today);

        public Period ToUniversalTime() => new(Start.ToUniversalTime(), End.ToUniversalTime());

        public Period ToLocalTime() => new(Start.ToLocalTime(), End.ToLocalTime());

        public Period AddAfter(TimeSpan offset) => new(Start, End.AddFluentTimeSpan(offset));

        public Period AddBefore(TimeSpan offset) => new(Start.AddFluentTimeSpan(offset), End);

        public Period SubstractBefore(TimeSpan offset) => new(Start, End.SubtractFluentTimeSpan(offset));

        public Period SubstractAfter(TimeSpan offset) => new(Start.SubtractFluentTimeSpan(offset), End);

        public Period ShiftLater(TimeSpan offset) => new(Start.AddFluentTimeSpan(offset), End.AddFluentTimeSpan(offset));

        public Period ShiftEarlier(TimeSpan offset) => new(Start.SubtractFluentTimeSpan(offset), End.SubtractFluentTimeSpan(offset));

        public ImmutablePeriod AsImmutable() => new(Start, End);
    }

    public class ImmutablePeriod : Period
    {
        public ImmutablePeriod(DateTime start, DateTime end) : base(start, end) { }

        public override void SetInterval(DateTime start, DateTime end) => throw new InvalidOperationException("This period is immutable.");
    }

    public class PeriodWithOptionalEnd : IntervalWithOptionalEnd<DateTime>
    {
        public PeriodWithOptionalEnd(DateTime start, DateTime? end = null) : base(start, end) { }

        public TimeSpan? NullableDuration => End is null ? null : End.Value.ToUniversalTime() - Start.ToUniversalTime();

        public TimeSpan Duration => End is null ? DateTime.UtcNow - Start.ToUniversalTime() : End.Value.ToUniversalTime() - Start.ToUniversalTime();
    }
}
