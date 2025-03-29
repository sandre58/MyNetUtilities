// -----------------------------------------------------------------------
// <copyright file="LoggerFactory.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Logging.NLog;

public static class LoggerFactory
{
    public static ILogger CreateLogger(string categoryName) => new Logger(categoryName);

    public static ILogger CreateLogger() => new Logger();
}
