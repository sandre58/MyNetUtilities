// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace MyNet.Utilities.Progress
{
    public interface IProgresser : IProgresser<ProgressMessage>
    {

    }

    public interface IProgresser<T>
    {
        IProgressStep<T> New(T message, Action? cancelAction = null);

        IProgressStep<T> New(int numberOfSteps, T message, Action? cancelAction = null);

        IProgressStep<T> New(IEnumerable<double> subStepDefinitions, T message, Action? cancelAction = null);

        IProgressStep<T> Start(T message, bool canCancel = true);

        IProgressStep<T> Start(int numberOfSteps, T message, bool canCancel = true);

        IProgressStep<T> Start(IEnumerable<double> subStepDefinitions, T message, bool canCancel = true);

        void Subscribe(IProgress<(double progress, IEnumerable<T> messages, Action? cancelAction, bool canCancel)> progress);

        void Unsubscribe(IProgress<(double progress, IEnumerable<T> messages, Action? cancelAction, bool canCancel)> progress);
    }
}
