// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyNet.Utilities;
using MyNet.Utilities.Localization;

namespace MyNet.Observable
{
    public class GlobalizableDateTime : IEquatable<DateTime>, IComparable<DateTime>, IComparable
    {
        public GlobalizableDateTime(DateTime date) : this(date, date.TimeOfDay) { }

        public GlobalizableDateTime(DateTime date, TimeZoneInfo timeZoneInfo) : this(date, date.TimeOfDay, timeZoneInfo) { }

        public GlobalizableDateTime(DateTime date, TimeSpan time) : this(date, time, date.Kind switch { DateTimeKind.Utc => TimeZoneInfo.Utc, DateTimeKind.Local => TimeZoneInfo.Local, _ => GlobalizationService.Current.TimeZone }) { }

        public GlobalizableDateTime(DateTime date, TimeSpan time, TimeZoneInfo timeZoneInfo)
        {
            DateTime = date.SetTime(time);
            TimeZone = timeZoneInfo;
        }

        public DateTime DateTime { get; }

        public DateTime Date => DateTime.Date;

        public TimeSpan Time => DateTime.TimeOfDay;

        public TimeZoneInfo TimeZone { get; }

        public DateTime Local => TimeZoneInfo.ConvertTime(DateTime, TimeZone, TimeZoneInfo.Local);

        public DateTime Utc => TimeZoneInfo.ConvertTime(DateTime, TimeZone, TimeZoneInfo.Utc);

        public DateTime Current => TimeZoneInfo.ConvertTime(DateTime, TimeZone, GlobalizationService.Current.TimeZone);

        public override string? ToString() => DateTime.ToString();

        public override bool Equals(object? obj) => (obj is GlobalizableDateTime item && item.DateTime == DateTime) || (obj is DateTime datetime && DateTime == datetime);

        public virtual bool Equals(DateTime other) => DateTime.Equals(other);

        public int CompareTo(DateTime? other) => DateTime.CompareTo(other);

        public int CompareTo(DateTime other) => DateTime.CompareTo(other);

        public int CompareTo(object? obj)
            => obj switch
            {
                DateTime obj1 => DateTime.CompareTo(obj1),
                GlobalizableDateTime obj2 => DateTime.CompareTo(obj2.DateTime),
                _ => DateTime.CompareTo(obj)
            };

        public int CompareTo(GlobalizableDateTime? other) => CompareTo(other?.DateTime);

        public static implicit operator DateTime?(GlobalizableDateTime value) => value.DateTime;

        public override int GetHashCode() => DateTime.GetHashCode();

        public static bool operator ==(GlobalizableDateTime left, GlobalizableDateTime right) => left is null ? right is null : left.DateTime.Equals(right.DateTime);
        public static bool operator !=(GlobalizableDateTime left, GlobalizableDateTime right) => !(left == right);
        public static bool operator <(GlobalizableDateTime left, GlobalizableDateTime right) => left is null ? right is not null : left.DateTime.CompareTo(right.DateTime) < 0;
        public static bool operator <=(GlobalizableDateTime left, GlobalizableDateTime right) => left is null || left.DateTime.CompareTo(right.DateTime) <= 0;
        public static bool operator >(GlobalizableDateTime left, GlobalizableDateTime right) => left is not null && left.DateTime.CompareTo(right.DateTime) > 0;
        public static bool operator >=(GlobalizableDateTime left, GlobalizableDateTime right) => left is null ? right is null : left.DateTime.CompareTo(right.DateTime) >= 0;
    }
}
