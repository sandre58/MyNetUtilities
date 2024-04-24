// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Suspending
{
    public interface ISuspender
    {
        bool IsSuspended { get; }

        IDisposable Suspend();

        IDisposable Allow();
    }
}
