// -----------------------------------------------------------------------
// <copyright file="ComparableExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyNet.Utilities.Comparers;
using MyNet.Utilities.Comparison;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ComparableExtensions
{
    public static bool Compare(this IComparable? x, IComparable? y, ComparableOperator sign)
    {
        if (x == null || y == null)
            return false;

        var compare = x.CompareTo(y);

        return sign switch
        {
            ComparableOperator.EqualsTo => compare == 0,
            ComparableOperator.NotEqualsTo => compare != 0,
            ComparableOperator.LessThan => compare < 0,
            ComparableOperator.GreaterThan => compare > 0,
            ComparableOperator.LessEqualThan => compare <= 0,
            ComparableOperator.GreaterEqualThan => compare >= 0,
            _ => throw new ArgumentException(null, nameof(sign))
        };
    }

    public static bool Compare(this IComparable? x, IComparable? from, IComparable? to, ComplexComparableOperator sign)
    {
        if (x == null || from == null || to == null)
            return false;

        var compareFrom = x.CompareTo(from);
        var compareTo = x.CompareTo(to);

        var result = compareFrom >= 0 && compareTo <= 0;

        return sign switch
        {
            ComplexComparableOperator.IsBetween => result,
            ComplexComparableOperator.IsNotBetween => !result,
            ComplexComparableOperator.EqualsTo => compareFrom == 0,
            ComplexComparableOperator.NotEqualsTo => compareFrom != 0,
            ComplexComparableOperator.LessThan => compareTo < 0,
            ComplexComparableOperator.GreaterThan => compareFrom > 0,
            ComplexComparableOperator.LessEqualThan => compareTo <= 0,
            ComplexComparableOperator.GreaterEqualThan => compareFrom >= 0,
            _ => throw new ArgumentException(null, nameof(sign))
        };
    }

    public static bool Compare<T>(this IComparable<T> x, T? from, T? to, ComplexComparableOperator sign)
        where T : struct, IComparable<T>
    {
        var compareFrom = x.CompareTo(from);
        var compareTo = x.CompareTo(to);

        var result = compareFrom >= 0 && compareTo <= 0;

        return sign switch
        {
            ComplexComparableOperator.IsBetween => result,
            ComplexComparableOperator.IsNotBetween => !result,
            ComplexComparableOperator.EqualsTo => compareFrom == 0,
            ComplexComparableOperator.NotEqualsTo => compareFrom != 0,
            ComplexComparableOperator.LessThan => compareTo < 0,
            ComplexComparableOperator.GreaterThan => compareFrom > 0,
            ComplexComparableOperator.LessEqualThan => compareTo <= 0,
            ComplexComparableOperator.GreaterEqualThan => compareFrom >= 0,
            _ => throw new ArgumentException(null, nameof(sign))
        };
    }

    public static int CompareTo<T>(this IComparable<T>? obj1, T obj2)
        where T : struct, IComparable<T>
        => new NullableComparer<T>().Compare(obj1, obj2);

    public static int CompareTo<T>(this IComparable<T>? obj1, T? obj2)
        where T : struct, IComparable<T>
        => new NullableComparer<T>().Compare(obj1, obj2);
}
