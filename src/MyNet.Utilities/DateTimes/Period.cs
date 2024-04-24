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
