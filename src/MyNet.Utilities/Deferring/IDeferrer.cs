// -----------------------------------------------------------------------
// <copyright file="IDeferrer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.Deferring;

public interface IDeferrer
{
    bool IsDeferred { get; }

    IDisposable Defer();

    void Execute();

    void DeferOrExecute();
}
