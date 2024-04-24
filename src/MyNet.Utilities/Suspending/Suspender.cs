// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;

namespace MyNet.Utilities.Suspending
{
    public class Suspender : ISuspender
    {
        private readonly ConcurrentStack<SuspendScope> _trackingScopes = new();

        internal void Pop() => _trackingScopes.TryPop(out var _);

        internal void Push(SuspendScope trackingScope) => _trackingScopes.Push(trackingScope);

        public bool IsSuspended => !(_trackingScopes.IsEmpty || _trackingScopes.TryPeek(out var peek) && !peek.IsSuspended);

        public IDisposable Suspend() => new SuspendScope(this, true);

        public IDisposable Allow() => new SuspendScope(this, false);
    }
}
