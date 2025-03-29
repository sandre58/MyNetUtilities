// -----------------------------------------------------------------------
// <copyright file="ReferenceEqualityComparer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MyNet.Utilities.Comparers;

public static class ReferenceEqualityComparer
{
    public static ReferenceEqualityComparer<object> Instance { get; } = new();
}

public class ReferenceEqualityComparer<T> : IEqualityComparer, IEqualityComparer<T>
{
    internal ReferenceEqualityComparer() { }

    public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);

    bool IEqualityComparer.Equals(object? x, object? y) => ReferenceEquals(x, y);

    bool IEqualityComparer<T>.Equals(T? x, T? y) => ReferenceEquals(x, y);

    int IEqualityComparer<T>.GetHashCode(T? obj) => RuntimeHelpers.GetHashCode(obj!);
}
