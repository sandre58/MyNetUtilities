// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.IO.AutoSave
{
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
}
