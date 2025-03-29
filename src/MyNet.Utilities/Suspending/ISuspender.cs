// -----------------------------------------------------------------------
// <copyright file="ISuspender.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.Suspending;

public interface ISuspender
{
    bool IsSuspended { get; }

    IDisposable Suspend();

    IDisposable Allow();
}
