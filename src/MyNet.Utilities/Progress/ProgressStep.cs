﻿// -----------------------------------------------------------------------
// <copyright file="ProgressStep.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MyNet.Utilities.Progress;

internal sealed class ProgressStep<T> : IProgressStep<T>
{
    private readonly Progresser<T> _progresser;
    private readonly IProgressStep? _parent;
    private readonly List<ProgressStepValue> _children = [];

    internal ProgressStep(Progresser<T> progresser, IProgressStep? parent, T? message, IEnumerable<double> subStepWeightings, Action? cancelAction, bool canCancel)
    {
        _progresser = progresser;
        _parent = parent;
        CancelAction = cancelAction;
        CanCancel = canCancel;

        foreach (var weighting in subStepWeightings)
            _children.Add(new ProgressStepValue(weighting, 0.0D));

        _progresser.Push(this);

        UpdateProgress(0.0D);
        UpdateMessage(message);
    }

    public double Progress { get; private set; }

    public T? Message { get; private set; }

    public bool CanCancel { get; }

    public Action? CancelAction { get; }

    public void UpdateMessage(T? message)
    {
        if (Equals(Message, message)) return;
        Message = message;
        _progresser.Report();
    }

    public void UpdateProgress(double value)
    {
        if (NearlyEqual(Progress, value)) return;
        var positiveValue = value < 0.0D ? 0.0D : value;
        Progress = value > 1 ? 1.0D : positiveValue;

        if (_parent is not null)
            _parent.SetChildProgress(Progress);
        else
            _progresser.Report();
    }

    public void SetChildProgress(double progress)
    {
        var currentChild = _children.Find(x => x.Progress < 1.0D && progress > x.Progress);
        if (currentChild is null) return;

        currentChild.Progress = progress;
        ComputeProgress();
    }

    public void Dispose()
    {
        UpdateProgress(1.0D);
        _progresser.Pop();
    }

    private static bool NearlyEqual(double value1, double value2)
    {
        var diff = Math.Abs(value1 - value2);

        return diff < double.Epsilon;
    }

    private void ComputeProgress()
    {
        var value = _children.Sum(x => x.Weighting * x.Progress);

        UpdateProgress(value);
    }

    [DebuggerDisplay("Weighting:{Weighting}, Progress:{Progress}")]
    private sealed class ProgressStepValue(double weighting, double progress)
    {
        public double Weighting { get; } = weighting;

        public double Progress { get; set; } = progress;
    }
}
