// -----------------------------------------------------------------------
// <copyright file="IProgressStep.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.Progress;

public interface IProgressStep<T> : IProgressStep
{
    T? Message { get; }

    void UpdateMessage(T? message);
}

public interface IProgressStep : IDisposable
{
    double Progress { get; }

    bool CanCancel { get; }

    Action? CancelAction { get; }

    void UpdateProgress(double value);

    internal void SetChildProgress(double progress);
}
