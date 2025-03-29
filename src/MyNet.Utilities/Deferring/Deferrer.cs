// -----------------------------------------------------------------------
// <copyright file="Deferrer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;

namespace MyNet.Utilities.Deferring;

/// <summary>
/// Base class that implements <see cref="IDeferrer"/>.
/// </summary>
/// <remarks>
/// Although it provides all implementations, this class is abstract so that one derives a specific class aimed at specific usages.
/// </remarks>
public class Deferrer : IDeferrer
{
    private readonly ConcurrentStack<DeferScope> _trackingScopes = new();
    private Action? _action;

    public Deferrer(Action action) => Bind(action);

    public Deferrer() { }

    public bool IsDeferred => !_trackingScopes.IsEmpty;

    public void Bind(Action action) => _action = action;

    public IDisposable Defer() => new DeferScope(this);

    public void Execute() => _action?.Invoke();

    public void DeferOrExecute()
    {
        if (IsDeferred) return;
        _action?.Invoke();
    }

    internal void Pop() => _trackingScopes.TryPop(out _);

    internal void Push(DeferScope trackingScope) => _trackingScopes.Push(trackingScope);

    internal void EndDefer() => DeferOrExecute();
}
