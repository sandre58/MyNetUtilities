// -----------------------------------------------------------------------
// <copyright file="IAutoSaveService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.IO.AutoSave;

public interface IAutoSaveService
{
    bool IsEnabled { get; }

    int Interval { get; }

    bool IsSaving { get; }

    void SetInterval(int intervalInSeconds);

    void Disable();

    void Enable();

    void Start();

    void Stop();

    void Cancel();

    IDisposable Suspend();
}
