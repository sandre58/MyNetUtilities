// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities.Progress
{
    public class Progresser : Progresser<ProgressMessage>, IProgresser
    {
        public IProgressStep<ProgressMessage> New(string message, params object[] parameters)
            => New(new ProgressMessage(message, parameters));

        public IProgressStep<ProgressMessage> New(IEnumerable<double> subStepDefinitions, string message, params object[] parameters)
            => New(new ProgressMessage(message, parameters));

        public IProgressStep<ProgressMessage> New(int numberOfSteps, string message, params object[] parameters)
            => New(new ProgressMessage(message, parameters));

        public IProgressStep<ProgressMessage> New(Action cancelAction, string message, params object[] parameters)
            => New(new ProgressMessage(message, parameters), cancelAction);

        public IProgressStep<ProgressMessage> New(Action cancelAction, IEnumerable<double> subStepDefinitions, string message, params object[] parameters)
            => New(new ProgressMessage(message, parameters), cancelAction);

        public IProgressStep<ProgressMessage> New(Action cancelAction, int numberOfSteps, string message, params object[] parameters)
            => New(new ProgressMessage(message, parameters), cancelAction);

        public IProgressStep<ProgressMessage> Start(string message, params object[] parameters)
            => Start(new ProgressMessage(message, parameters), false);

        public IProgressStep<ProgressMessage> Start(IEnumerable<double> subStepDefinitions, string message, params object[] parameters)
            => Start(subStepDefinitions, new ProgressMessage(message, parameters), false);

        public IProgressStep<ProgressMessage> Start(int numberOfSteps, string message, params object[] parameters)
            => Start(numberOfSteps, new ProgressMessage(message, parameters), false);

        public IProgressStep<ProgressMessage> StartCancellable(string message, params object[] parameters)
            => Start(new ProgressMessage(message, parameters));

        public IProgressStep<ProgressMessage> StartCancellable(IEnumerable<double> subStepDefinitions, string message, params object[] parameters)
            => Start(subStepDefinitions, new ProgressMessage(message, parameters));

        public IProgressStep<ProgressMessage> StartCancellable(int numberOfSteps, string message, params object[] parameters)
            => Start(numberOfSteps, new ProgressMessage(message, parameters));
    }

    public class Progresser<T> : IProgresser<T>
    {
        private readonly HashSet<IProgress<(double progress, IEnumerable<T> messages, Action? cancelAction, bool canCancel)>> _progressSubscribers = [];
        private readonly ConcurrentStack<IProgressStep<T>> _steps = new();

        public IProgressStep<T> New(T message, Action? cancelAction = null)
        {
            _steps.Clear();
            return new ProgressStep<T>(this, null, message, [], cancelAction, true);
        }

        public IProgressStep<T> New(IEnumerable<double> subStepDefinitions, T message, Action? cancelAction = null)
        {
            _steps.Clear();
            return new ProgressStep<T>(this, null, message, subStepDefinitions, cancelAction, true);
        }

        public IProgressStep<T> New(int numberOfSteps, T message, Action? cancelAction = null)
        {
            _steps.Clear();
            return new ProgressStep<T>(this, null, message, Enumerable.Range(1, numberOfSteps).Select(x => 1.0D / numberOfSteps).ToList(), cancelAction, true);
        }

        public IProgressStep<T> Start(T message, bool canCancel = true)
            => new ProgressStep<T>(this, GetCurrent(), message, [], null, canCancel);

        public IProgressStep<T> Start(IEnumerable<double> subStepDefinitions, T message, bool canCancel = true)
            => new ProgressStep<T>(this, GetCurrent(), message, subStepDefinitions, null, canCancel);

        public IProgressStep<T> Start(int numberOfSteps, T message, bool canCancel = true)
            => new ProgressStep<T>(this, GetCurrent(), message, Enumerable.Range(1, numberOfSteps).Select(x => 1.0D / numberOfSteps).ToList(), null, canCancel);

        public void Subscribe(IProgress<(double progress, IEnumerable<T> messages, Action? cancelAction, bool canCancel)> progress) => _progressSubscribers.Add(progress);

        public void Unsubscribe(IProgress<(double progress, IEnumerable<T> messages, Action? cancelAction, bool canCancel)> progress) => _progressSubscribers.Remove(progress);

        internal void Pop() => _steps.TryPop(out var _);

        internal void Push(IProgressStep<T> progressStep) => _steps.Push(progressStep);

        internal void Report()
        {
            var messages = _steps.Where(x => x.Message is not null).Reverse().Select(x => x.Message!).ToList();
            var value = _steps.Last().Progress;
            var cancelAction = _steps.LastOrDefault()?.CancelAction;
            var canCancel = _steps.All(x => x.CanCancel);

            _progressSubscribers.ToList().ForEach(x => x.Report((value, messages, cancelAction, canCancel)));
        }

        private IProgressStep GetCurrent() => _steps.TryPeek(out var result) ? result : throw new InvalidOperationException("Impossible to start a progress step before to create a root step with New()");
    }
}
