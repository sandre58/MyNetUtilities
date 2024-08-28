// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyNet.Utilities.Sequences;

namespace MyNet.Utilities.DateTimes
{
    public class TimePeriod : Interval<TimeOnly, TimePeriod>
    {
        public TimePeriod(TimeOnly start, TimeOnly end) : base(start, end) { }

        public TimeSpan Duration => End - Start;

        public ImmutableTimePeriod AsImmutable() => new(Start, End);

        protected override TimePeriod CreateInstance(TimeOnly start, TimeOnly end) => new(start, end);
    }

    public class ImmutableTimePeriod : TimePeriod
    {
        public ImmutableTimePeriod(TimeOnly start, TimeOnly end) : base(start, end) { }

        public override void SetInterval(TimeOnly start, TimeOnly end) => throw new InvalidOperationException("This period is immutable.");

        protected override TimePeriod CreateInstance(TimeOnly start, TimeOnly end) => new ImmutableTimePeriod(start, end);
    }

    public class TimePeriodWithOptionalEnd : IntervalWithOptionalEnd<TimeOnly>
    {
        public TimePeriodWithOptionalEnd(TimeOnly start, TimeOnly? end = null) : base(start, end) { }

        public TimeSpan? NullableDuration => End is null ? null : End.Value - Start;

        public TimeSpan Duration => End is null ? DateTime.UtcNow.ToTime() - Start : End.Value - Start;
    }
}
