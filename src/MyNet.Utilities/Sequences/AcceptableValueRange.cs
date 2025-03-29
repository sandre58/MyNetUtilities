// -----------------------------------------------------------------------
// <copyright file="AcceptableValueRange.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using MyNet.Utilities.Exceptions;

namespace MyNet.Utilities.Sequences;

public class AcceptableValueRange<T>(T? min, T? max)
    where T : struct, IComparable
{
    public T? Min { get; } = min;

    public T? Max { get; } = max;

    public T MinOrDefault() => Min ?? default;

    public T MaxOrDefault() => Max ?? default;

    public bool IsValid(T? value) => !value.HasValue || ((!Min.HasValue || Min.Value.CompareTo(value) <= 0) && (!Max.HasValue || Max.Value.CompareTo(value) >= 0));

    public T ValidateOrThrow(T value, [CallerMemberName] string propertyName = null!)
        => Min.HasValue && Min.Value.CompareTo(value) > 0
            ? throw new IsNotUpperOrEqualsThanException(propertyName, Min.Value)
            : Max.HasValue && Max.Value.CompareTo(value) < 0
                ? throw new IsNotLowerOrEqualsThanException(propertyName, Max.Value)
                : value;

    public T? ValidateOrThrow(T? value, [CallerMemberName] string propertyName = null!)
        => !value.HasValue
            ? value
            : ValidateOrThrow(value.Value, propertyName);

    public T ValidateValue(T value)
        => Min.HasValue && Min.Value.CompareTo(value) > 0
            ? Min.Value
            : Max.HasValue && Max.Value.CompareTo(value) < 0
                ? Max.Value
                : value;

    public T? ValidateValue(T? value)
        => !value.HasValue
            ? value
            : ValidateValue(value.Value);
}
