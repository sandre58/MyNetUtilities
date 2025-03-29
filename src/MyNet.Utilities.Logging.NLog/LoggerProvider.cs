// -----------------------------------------------------------------------
// <copyright file="LoggerProvider.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace MyNet.Utilities.Logging.NLog;

public sealed class LoggerProvider : ILoggerProvider
{
    public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName) => new Logger(categoryName);

    public void Dispose()
    {
        // Method intentionally left empty.
    }
}
