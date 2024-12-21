// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Extensions;

namespace MyNet.Utilities.Sequences
{
    public class Interval<T> : Interval<T, Interval<T>>
        where T : struct, IComparable
    {
        public Interval(T start, T end) : base(start, end) { }

        protected override Interval<T> CreateInstance(T start, T end) => new(start, end);
    }

    public abstract class Interval<T, TClass> : ValueObject, IComparable, ICloneable<TClass>
        where T : struct, IComparable
        where TClass : Interval<T, TClass>
    {
        protected Interval(T start, T end) => SetIntervalInternal(start, end);

        public T Start { get; private set; }

        public T End { get; private set; }

        private void SetIntervalInternal(T start, T end)
        {
            Start = start.IsRequiredOrThrow(nameof(Start)).IsLowerOrEqualThanOrThrow(end, nameof(Start));
            End = end.IsRequiredOrThrow(nameof(End)).IsUpperOrEqualThanOrThrow(start, nameof(End));
        }

        public virtual void SetInterval(T start, T end) => SetIntervalInternal(start, end);

        public virtual bool Contains(T value) => value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;

        public virtual bool Contains(TClass interval) => interval.Start.CompareTo(Start) >= 0 && interval.End.CompareTo(End) <= 0;

        public virtual bool IntersectWith(TClass interval)
            => Start.CompareTo(interval.End) < 0 && interval.Start.CompareTo(End) < 0;

        public virtual TClass? Intersect(TClass interval)
            => Contains(interval)
                ? interval.Clone()
                : interval.Contains(this.CastIn<TClass>())
                ? Clone()
                : interval.Start.CompareTo(Start) >= 0 && interval.Start.CompareTo(End) < 0
                ? CreateInstance(interval.Start, End)
                : interval.End.CompareTo(Start) > 0 && interval.End.CompareTo(End) <= 0
                ? CreateInstance(Start, interval.End)
                : null;

        public virtual TClass? Union(TClass interval)
            => IntersectWith(interval)
                ? CreateInstance(new List<T> { Start, interval.Start }.Min(), new List<T> { End, interval.End }.Max())
                : null;

        public virtual IEnumerable<TClass> Exclude(TClass interval)
            => Contains(interval)
                ? [CreateInstance(Start, interval.Start), CreateInstance(interval.End, End)]
                : interval.Contains(this.CastIn<TClass>())
                ? []
                : interval.Start.CompareTo(Start) >= 0 && interval.Start.CompareTo(End) < 0
                ? [CreateInstance(Start, interval.Start)]
                : interval.End.CompareTo(Start) > 0 && interval.End.CompareTo(End) <= 0
                ? [CreateInstance(interval.End, End)]
                : [Clone()];

        protected abstract TClass CreateInstance(T start, T end);

        public TClass Clone() => CreateInstance(Start, End);

        public override bool Equals(object? obj) => obj is TClass vm && Start.Equals(vm.Start) && End.Equals(vm.End);

        public override int GetHashCode() => Start.GetHashCode();

        public override string ToString() => $"{Start} - {End}";

        #region IComparable

        public virtual int CompareTo(object? obj) => obj is TClass interval ? !Equals(Start, interval.Start) ? Start.CompareTo(interval.Start) : End.CompareTo(interval.End) : 1;

        public static bool operator ==(Interval<T, TClass> left, Interval<T, TClass> right) => left?.Equals(right) ?? right is null;

        public static bool operator >(Interval<T, TClass> left, Interval<T, TClass> right) => left.CompareTo(right) > 0;

        public static bool operator <(Interval<T, TClass> left, Interval<T, TClass> right) => left.CompareTo(right) < 0;

        public static bool operator >=(Interval<T, TClass> left, Interval<T, TClass> right) => left.CompareTo(right) >= 0;

        public static bool operator <=(Interval<T, TClass> left, Interval<T, TClass> right) => left.CompareTo(right) <= 0;

        public static bool operator !=(Interval<T, TClass> left, Interval<T, TClass> right) => !(left == right);

        #endregion
    }

    public class ImmutableInterval<T> : Interval<T> where T : struct, IComparable
    {
        public ImmutableInterval(T start, T end) : base(start, end) { }

        public override void SetInterval(T start, T end) => throw new InvalidOperationException("This interval is immutable.");

        protected override Interval<T> CreateInstance(T start, T end) => new ImmutableInterval<T>(start, end);
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
