// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Authentication
{
    public class AuthenticatedEventArgs(bool success) : EventArgs
    {
        public bool Success { get; } = success;
    }
}
