﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using MyNet.Utilities.Exceptions;

namespace MyNet.Utilities.Sequences
{
    public class AcceptableValueRange<T> where T : struct, IComparable
    {
        public AcceptableValueRange(T? min, T? max)
        {
            Min = min;
            Max = max;
        }

        public T? Min { get; }

        public T? Max { get; }

        public T MinOrDefault() => Min ?? default;

        public T MaxOrDefault() => Max ?? default;

        public bool IsValid(T? value) => !value.HasValue || (!Min.HasValue || Min.Value.CompareTo(value) <= 0) && (!Max.HasValue || Max.Value.CompareTo(value) >= 0);

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
}
