// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MyNet.Utilities.Comparers
{
    public class PredicateEqualityComparer<T>(Func<T, T, bool> predicate) : IEqualityComparer, IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _predicate = predicate;

        public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);

        bool IEqualityComparer.Equals(object? x, object? y) => x is not null && y is not null && !ReferenceEquals(x, y) && _predicate.Invoke((T)x, (T)y);

        bool IEqualityComparer<T>.Equals(T? x, T? y) => x is not null && y is not null && !ReferenceEquals(x, y) && _predicate.Invoke(x, y);

        int IEqualityComparer<T>.GetHashCode(T? obj) => RuntimeHelpers.GetHashCode(obj!);

    }
}
