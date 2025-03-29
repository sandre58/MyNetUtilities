// -----------------------------------------------------------------------
// <copyright file="Suspender.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;

namespace MyNet.Utilities.Suspending;

public class Suspender : ISuspender
{
    private readonly ConcurrentStack<SuspendScope> _trackingScopes = new();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "We don't want dispose")]
    public bool IsSuspended => !(_trackingScopes.IsEmpty || (_trackingScopes.TryPeek(out var peek) && !peek.IsSuspended));

    public IDisposable Suspend() => new SuspendScope(this, true);

    public IDisposable Allow() => new SuspendScope(this, false);

    internal void Pop() => _trackingScopes.TryPop(out _);

    internal void Push(SuspendScope trackingScope) => _trackingScopes.Push(trackingScope);
}
