// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;

namespace MyNet.Utilities.Deferring
{
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

        public void Bind(Action action) => _action = action;

        internal void Pop() => _trackingScopes.TryPop(out var _);

        internal void Push(DeferScope trackingScope) => _trackingScopes.Push(trackingScope);

        public bool IsDeferred => !_trackingScopes.IsEmpty;

        internal void EndDefer() => DeferOrExecute();

        public IDisposable Defer() => new DeferScope(this);

        public void Execute() => _action?.Invoke();

        public void DeferOrExecute()
        {
            if (IsDeferred) return;
            _action?.Invoke();
        }
    }
}
