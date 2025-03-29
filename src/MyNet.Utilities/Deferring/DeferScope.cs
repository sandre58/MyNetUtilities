// -----------------------------------------------------------------------
// <copyright file="DeferScope.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.Deferring;

internal sealed class DeferScope : IDisposable
{
    private readonly Deferrer _deferrerScopeBase;

    public DeferScope(Deferrer sender)
    {
        _deferrerScopeBase = sender;

        _deferrerScopeBase.Push(this);
    }

    public void Dispose()
    {
        _deferrerScopeBase.Pop();

        _deferrerScopeBase.EndDefer();
    }
}
