// -----------------------------------------------------------------------
// <copyright file="AuthenticatedEventArgs.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.Authentication;

public class AuthenticatedEventArgs(bool success) : EventArgs
{
    public bool Success { get; } = success;
}
