﻿// -----------------------------------------------------------------------
// <copyright file="PredicateEqualityComparer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MyNet.Utilities.Comparers;

public class PredicateEqualityComparer<T>(Func<T, T, bool> predicate) : IEqualityComparer, IEqualityComparer<T>
{
    public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);

    bool IEqualityComparer.Equals(object? x, object? y) => x is not null && y is not null && !ReferenceEquals(x, y) && predicate.Invoke((T)x, (T)y);

    bool IEqualityComparer<T>.Equals(T? x, T? y) => x is not null && y is not null && !ReferenceEquals(x, y) && predicate.Invoke(x, y);

    int IEqualityComparer<T>.GetHashCode(T? obj) => RuntimeHelpers.GetHashCode(obj!);
}
