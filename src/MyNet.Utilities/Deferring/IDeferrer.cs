// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Deferring
{
    public interface IDeferrer
    {
        bool IsDeferred { get; }

        IDisposable Defer();

        void Execute();

        void DeferOrExecute();
    }
}
