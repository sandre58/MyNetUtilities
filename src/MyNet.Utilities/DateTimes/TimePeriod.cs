// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyNet.Utilities.Sequences;

namespace MyNet.Utilities.DateTimes
{
    public class TimePeriod : Interval<TimeSpan>
    {
        public TimePeriod(TimeSpan start, TimeSpan end, DateTimeKind kind) : base(start, end) => Kind = kind;

        public DateTimeKind Kind { get; }

        public TimeSpan Duration => End - Start;

        public bool IsCurrent() => Contains(Kind == DateTimeKind.Local ? DateTime.Now.TimeOfDay : DateTime.UtcNow.TimeOfDay);

        public bool Contains(DateTime date) => Contains(Kind == DateTimeKind.Local ? date.ToLocalTime().TimeOfDay : date.ToUniversalTime().TimeOfDay);

        public TimePeriod ToUniversalTime(DateTime? targetDate = null)
            => Kind == DateTimeKind.Local
            ? new(Start.ToUniversalTime(targetDate), End.ToUniversalTime(targetDate), DateTimeKind.Utc)
            : this;

        public TimePeriod ToLocalTime(DateTime? targetDate = null)
            => Kind == DateTimeKind.Utc
            ? new(Start.ToLocalTime(targetDate), End.ToLocalTime(targetDate), DateTimeKind.Local)
            : this;

        public TimePeriod AddAfter(TimeSpan offset) => new(Start, End.AddFluentTimeSpan(offset), Kind);

        public TimePeriod AddBefore(TimeSpan offset) => new(Start.AddFluentTimeSpan(offset), End, Kind);

        public TimePeriod SubstractBefore(TimeSpan offset) => new(Start, End.SubtractFluentTimeSpan(offset), Kind);

        public TimePeriod SubstractAfter(TimeSpan offset) => new(Start.SubtractFluentTimeSpan(offset), End, Kind);

        public TimePeriod ShiftLater(TimeSpan offset) => new(Start.AddFluentTimeSpan(offset), End.AddFluentTimeSpan(offset), Kind);

        public TimePeriod ShiftEarlier(TimeSpan offset) => new(Start.SubtractFluentTimeSpan(offset), End.SubtractFluentTimeSpan(offset), Kind);
    }
}
