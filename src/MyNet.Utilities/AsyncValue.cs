// -----------------------------------------------------------------------
// <copyright file="AsyncValue.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities;

public class AsyncValue<T>(Func<T> provideValue)
{
    private T _value = default!;

    public T Value => _value ??= provideValue();
}
