// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyNet.Utilities.Extensions;

namespace MyNet.Utilities.Sequences
{
    public class Interval<T> : ValueObject, IComparable where T : struct, IComparable
    {
        public Interval(T start, T end) => SetIntervalInternal(start, end);

        public T Start { get; private set; }

        public T End { get; private set; }

        private void SetIntervalInternal(T start, T end)
        {
            Start = start.IsRequiredOrThrow(nameof(Start)).IsLowerOrEqualThanOrThrow(end, nameof(Start));
            End = end.IsRequiredOrThrow(nameof(End)).IsUpperOrEqualThanOrThrow(start, nameof(End));
        }

        public virtual void SetInterval(T start, T end) => SetIntervalInternal(start, end);

        public bool Contains(T value) => value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;

        public bool Intersect(Interval<T> interval) => Start.CompareTo(interval.End) < 0 && interval.Start.CompareTo(End) < 0;

        public override bool Equals(object? obj) => obj is Interval<T> vm && Start.Equals(vm.Start) && End.Equals(vm.End);

        public override int GetHashCode() => Start.GetHashCode();

        public override string ToString() => $"{Start} - {End}";

        #region IComparable

        public virtual int CompareTo(object? obj) => obj is Interval<T> interval ? !Equals(Start, interval.Start) ? Start.CompareTo(interval.Start) : End.CompareTo(interval.End) : 1;

        public static bool operator ==(Interval<T> left, Interval<T> right) => left?.Equals(right) ?? right is null;

        public static bool operator >(Interval<T> left, Interval<T> right) => left.CompareTo(right) > 0;

        public static bool operator <(Interval<T> left, Interval<T> right) => left.CompareTo(right) < 0;

        public static bool operator >=(Interval<T> left, Interval<T> right) => left.CompareTo(right) >= 0;

        public static bool operator <=(Interval<T> left, Interval<T> right) => left.CompareTo(right) <= 0;

        public static bool operator !=(Interval<T> left, Interval<T> right) => !(left == right);

        #endregion
    }

    public class ImmutableInterval<T> : Interval<T> where T : struct, IComparable
    {
        public ImmutableInterval(T start, T end) : base(start, end) { }

        public override void SetInterval(T start, T end) => throw new InvalidOperationException("This interval is immutable.");
    }

    public class IntervalWithOptionalEnd<T> : ValueObject where T : struct, IComparable
    {
        public IntervalWithOptionalEnd(T start, T? end = null) => SetIntervalInternal(start, end);

        public T Start { get; private set; }

        public T? End { get; private set; }

        private void SetIntervalInternal(T start, T? end = null) => SetInterval(start, end);

        public virtual void SetInterval(T start, T? end = null)
        {
            Start = end is null ? start : start.IsLowerOrEqualThanOrThrow(end.Value);
            End = end is null ? end : end.Value.IsUpperOrEqualThanOrThrow(start);
        }

        public bool Contains(T value) => value.CompareTo(Start) >= 0 && (End is null || value.CompareTo(End) <= 0);

        public override string ToString() => $"{Start} - {End}";
    }
}
