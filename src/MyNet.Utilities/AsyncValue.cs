// -----------------------------------------------------------------------
// <copyright file="AsyncValue.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities;

public class AsyncValue<T>
{
    private readonly Func<T> _provideValue;
    private T _value = default!;

    public AsyncValue(Func<T> provideValue) => _provideValue = provideValue;

    public T Value => _value ??= _provideValue();
}
