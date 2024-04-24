// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Suspending
{
    internal sealed class SuspendScope : IDisposable
    {
        private readonly Suspender _suspender;

        public bool IsSuspended { get; }

        public SuspendScope(Suspender sender, bool suspend)
        {
            _suspender = sender;
            IsSuspended = suspend;

            _suspender.Push(this);
        }

        public void Dispose() => _suspender.Pop();
    }
}
