// -----------------------------------------------------------------------
// <copyright file="SuspendScope.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.Suspending;

internal sealed class SuspendScope : IDisposable
{
    private readonly Suspender _suspender;

    public SuspendScope(Suspender sender, bool suspend)
    {
        _suspender = sender;
        IsSuspended = suspend;

        _suspender.Push(this);
    }

    public bool IsSuspended { get; }

    public void Dispose() => _suspender.Pop();
}
