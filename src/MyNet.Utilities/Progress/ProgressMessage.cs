// -----------------------------------------------------------------------
// <copyright file="ProgressMessage.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Progress;

public class ProgressMessage
{
    public ProgressMessage(string message, params object[] parameters) => (Message, Parameters) = (message, parameters);

    public string Message { get; }

    public object[] Parameters { get; }
}
