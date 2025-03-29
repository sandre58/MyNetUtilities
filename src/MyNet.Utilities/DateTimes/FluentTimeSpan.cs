// -----------------------------------------------------------------------
// <copyright file="FluentTimeSpan.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace MyNet.Utilities.DateTimes;

[StructLayout(LayoutKind.Sequential)]
public readonly struct FluentTimeSpan :
    IEquatable<FluentTimeSpan>,
    IComparable<TimeSpan>,
    IComparable<FluentTimeSpan>
{
    private const int DaysPerYear = 365;

    public int Months { get; init; }

    public int Years { get; init; }

    public TimeSpan TimeSpan { get; init; }

    /// <summary>
    /// Gets the number of ticks that represent the value of the current <see cref="TimeSpan"/> structure.
    /// </summary>
    public long Ticks => ((TimeSpan)this).Ticks;

    public int Days => ((TimeSpan)this).Days;

    public int Hours => ((TimeSpan)this).Hours;

    public int Milliseconds => ((TimeSpan)this).Milliseconds;

    public int Minutes => ((TimeSpan)this).Minutes;

    public int Seconds => ((TimeSpan)this).Seconds;

    public double TotalDays => ((TimeSpan)this).TotalDays;

    public double TotalHours => ((TimeSpan)this).TotalHours;

    public double TotalMilliseconds => ((TimeSpan)this).TotalMilliseconds;

    public double TotalMinutes => ((TimeSpan)this).TotalMinutes;

    public double TotalSeconds => ((TimeSpan)this).TotalSeconds;

    /// <summary>
    /// Performs an explicit conversion from <see cref="FluentTimeSpan"/> to <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="fluentTimeSpan">The FluentTimeSpan.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator TimeSpan(FluentTimeSpan fluentTimeSpan)
    {
        var daysFromYears = DaysPerYear * fluentTimeSpan.Years;
        var daysFromMonths = 30 * fluentTimeSpan.Months;
        var days = daysFromMonths + daysFromYears;
        return new TimeSpan(days, 0, 0, 0) + fluentTimeSpan.TimeSpan;
    }

    /// <summary>
    /// Performs an implicit conversion from a <see cref="TimeSpan"/> to <see cref="FluentTimeSpan"/>.
    /// </summary>
    /// <param name="timeSpan">The <see cref="TimeSpan"/> that will be converted.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator FluentTimeSpan(TimeSpan timeSpan) => new() { TimeSpan = timeSpan };

    /// <summary>
    /// Overload of the operator +.
    /// </summary>
    /// <param name="left">The left hand <see cref="FluentTimeSpan"/>.</param>
    /// <param name="right">The right hand <see cref="FluentTimeSpan"/>.</param>
    /// <returns>The result of the addition.</returns>
    public static FluentTimeSpan operator +(FluentTimeSpan left, FluentTimeSpan right) => AddInternal(left, right);

    public static FluentTimeSpan operator +(FluentTimeSpan left, TimeSpan right) => AddInternal(left, right);

    public static FluentTimeSpan operator +(TimeSpan left, FluentTimeSpan right) => AddInternal(left, right);

    /// <summary>
    /// Overload of the operator -.
    /// </summary>
    /// <param name="left">The left hand <see cref="FluentTimeSpan"/>.</param>
    /// <param name="right">The right hand <see cref="FluentTimeSpan"/>.</param>
    /// <returns>The result of the subtraction.</returns>
    public static FluentTimeSpan operator -(FluentTimeSpan left, FluentTimeSpan right) => SubtractInternal(left, right);

    public static FluentTimeSpan operator -(TimeSpan left, FluentTimeSpan right) => SubtractInternal(left, right);

    public static FluentTimeSpan operator -(FluentTimeSpan left, TimeSpan right) => SubtractInternal(left, right);

    /// <summary>
    /// Equals operator.
    /// </summary>
    /// <param name="left">The left hand side.</param>
    /// <param name="right">The right hand side.</param>
    /// <returns><c>true</c> is <paramref name="left"/> is equal to <paramref name="right"/>; otherwise <c>false</c>.</returns>
    public static bool operator ==(FluentTimeSpan left, FluentTimeSpan right) => left.Years == right.Years &&
        left.Months == right.Months &&
        left.TimeSpan == right.TimeSpan;

    public static bool operator ==(TimeSpan left, FluentTimeSpan right) => (FluentTimeSpan)left == right;

    public static bool operator ==(FluentTimeSpan left, TimeSpan right) => left == (FluentTimeSpan)right;

    /// <summary>
    /// Not Equals operator.
    /// </summary>
    /// <param name="left">The left hand side.</param>
    /// <param name="right">The right hand side.</param>
    /// <returns><c>true</c> is <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise <c>false</c>.</returns>
    public static bool operator !=(FluentTimeSpan left, FluentTimeSpan right) => !(left == right);

    public static bool operator !=(TimeSpan left, FluentTimeSpan right) => !(left == right);

    public static bool operator !=(FluentTimeSpan left, TimeSpan right) => !(left == right);

    public static FluentTimeSpan operator -(FluentTimeSpan value) => value.Negate();

    public static bool operator <(FluentTimeSpan left, FluentTimeSpan right) => (TimeSpan)left < (TimeSpan)right;

    public static bool operator <(FluentTimeSpan left, TimeSpan right) => (TimeSpan)left < right;

    public static bool operator <(TimeSpan left, FluentTimeSpan right) => left < (TimeSpan)right;

    public static bool operator <=(FluentTimeSpan left, FluentTimeSpan right) => (TimeSpan)left <= (TimeSpan)right;

    public static bool operator <=(FluentTimeSpan left, TimeSpan right) => (TimeSpan)left <= right;

    public static bool operator <=(TimeSpan left, FluentTimeSpan right) => left <= (TimeSpan)right;

    public static bool operator >(FluentTimeSpan left, FluentTimeSpan right) => (TimeSpan)left > (TimeSpan)right;

    public static bool operator >(FluentTimeSpan left, TimeSpan right) => (TimeSpan)left > right;

    public static bool operator >(TimeSpan left, FluentTimeSpan right) => left > (TimeSpan)right;

    public static bool operator >=(FluentTimeSpan left, FluentTimeSpan right) => (TimeSpan)left >= (TimeSpan)right;

    public static bool operator >=(FluentTimeSpan left, TimeSpan right) => (TimeSpan)left >= right;

    public static bool operator >=(TimeSpan left, FluentTimeSpan right) => left >= (TimeSpan)right;

    /// <summary>
    /// Performs an implicit conversion from a <see cref="TimeSpan"/> to <see cref="FluentTimeSpan"/>.
    /// </summary>
    /// <param name="timeSpan">The <see cref="TimeSpan"/> that will be converted.</param>
    /// <returns>The result of the conversion.</returns>
    public static FluentTimeSpan ToFluentTimeSpan(TimeSpan timeSpan) => new() { TimeSpan = timeSpan };

    /// <summary>
    /// Performs an explicit conversion from <see cref="FluentTimeSpan"/> to <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="fluentTimeSpan">The FluentTimeSpan.</param>
    /// <returns>The result of the conversion.</returns>
    public static TimeSpan FromFluentTimeSpan(FluentTimeSpan fluentTimeSpan)
    {
        var daysFromYears = DaysPerYear * fluentTimeSpan.Years;
        var daysFromMonths = 30 * fluentTimeSpan.Months;
        var days = daysFromMonths + daysFromYears;
        return new TimeSpan(days, 0, 0, 0) + fluentTimeSpan.TimeSpan;
    }

    /// <summary>
    /// Adds two <see cref="FluentTimeSpan"/> according operator +.
    /// </summary>
    /// <param name="number">The number to add to this <see cref="FluentTimeSpan"/>.</param>
    /// <returns>The result of the addition.</returns>
    public FluentTimeSpan Add(FluentTimeSpan number) => AddInternal(this, number);

    /// <summary>
    /// Returns a new <see cref="FluentTimeSpan"/> that adds the value of the specified <see cref="TimeSpan"/> to the value of this instance.
    /// </summary>
    /// <param name="timeSpan">The <see cref="TimeSpan"/> to add to this <see cref="FluentTimeSpan"/>.</param>
    /// <returns>The result of the addition.</returns>
    public FluentTimeSpan Add(TimeSpan timeSpan) => AddInternal(this, timeSpan);

    /// <summary>
    /// Subtracts the number according operator -.
    /// </summary>
    /// <param name="fluentTimeSpan">The matrix to subtract from this <see cref="FluentTimeSpan"/>.</param>
    /// <returns>The result of the subtraction.</returns>
    public FluentTimeSpan Subtract(FluentTimeSpan fluentTimeSpan) => SubtractInternal(this, fluentTimeSpan);

    /// <summary>
    /// Returns a new <see cref="FluentTimeSpan"/> that subtracts the value of the specified <see cref="TimeSpan"/> to the value of this instance.
    /// </summary>
    /// <param name="timeSpan">The <see cref="TimeSpan"/> to subtract from this <see cref="FluentTimeSpan"/>.</param>
    /// <returns>The result of the subtraction.</returns>
    public FluentTimeSpan Subtract(TimeSpan timeSpan) => SubtractInternal(this, timeSpan);

    public int CompareTo(TimeSpan other) => ((TimeSpan)this).CompareTo(other);

    public int CompareTo(object value) => value is TimeSpan timeSpan
        ? ((TimeSpan)this).CompareTo(timeSpan)
        : throw new ArgumentException(@"Value must be a TimeSpan", nameof(value));

    public int CompareTo(FluentTimeSpan other) => ((TimeSpan)this).CompareTo(other);

    public TimeSpan Negate() => new FluentTimeSpan
    {
        TimeSpan = -TimeSpan,
        Months = -Months,
        Years = -Years
    };

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    public object Clone() => new FluentTimeSpan
    {
        TimeSpan = TimeSpan,
        Months = Months,
        Years = Years
    };

    /// <inheritdoc />
    public override string ToString() => ((TimeSpan)this).ToString();

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <c>true</c> if the current object is equal to the other parameter; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(FluentTimeSpan other) => this == other;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        var type = obj.GetType();
        return type == typeof(FluentTimeSpan) ? this == (FluentTimeSpan)obj : type == typeof(TimeSpan) && this == (TimeSpan)obj;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Months.GetHashCode() ^ Years.GetHashCode() ^ TimeSpan.GetHashCode();

    internal static FluentTimeSpan SubtractInternal(TimeSpan left, FluentTimeSpan right) => new()
    {
        Months = -right.Months,
        Years = -right.Years,
        TimeSpan = left - right.TimeSpan
    };

    private static FluentTimeSpan AddInternal(FluentTimeSpan left, TimeSpan right) => left with { TimeSpan = left.TimeSpan + right };

    private static FluentTimeSpan AddInternal(FluentTimeSpan left, FluentTimeSpan right) => new()
    {
        Years = left.Years + right.Years,
        Months = left.Months + right.Months,
        TimeSpan = left.TimeSpan + right.TimeSpan
    };

    private static FluentTimeSpan SubtractInternal(FluentTimeSpan left, TimeSpan right) => left with { TimeSpan = left.TimeSpan - right };

    private static FluentTimeSpan SubtractInternal(FluentTimeSpan left, FluentTimeSpan right) => new()
    {
        Years = left.Years - right.Years,
        Months = left.Months - right.Months,
        TimeSpan = left.TimeSpan - right.TimeSpan
    };
}
