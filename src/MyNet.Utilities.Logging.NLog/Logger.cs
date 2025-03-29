// -----------------------------------------------------------------------
// <copyright file="Logger.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.Extensions.Logging;
using NLog;
using NLogLogger = NLog.Logger;
using NLogLogManager = NLog.LogManager;

namespace MyNet.Utilities.Logging.NLog;

/// <summary>
/// Class representing a Logger.
/// </summary>
public sealed class Logger : ILogger, Microsoft.Extensions.Logging.ILogger
{
    private readonly NLogLogger _logger;

    #region ----- Constructors -----

    /// <summary>
    /// Initializes a new instance of the <see cref="Logger"/> class.
    /// </summary>
    public Logger() => _logger = NLogLogManager.GetCurrentClassLogger();

    /// <summary>
    /// Initializes a new instance of the <see cref="Logger"/> class.
    /// </summary>
    public Logger(string name) => _logger = NLogLogManager.GetLogger(name);

    public static void LoadConfiguration(string configFile) => NLogLogManager.Setup().LoadConfigurationFromFile(configFile);

    #endregion ----- Constructors -----

    /// <summary>
    /// Information the specified message.
    /// </summary>
    /// <param name="message">The resource.</param>
    public void Info(string message) => _logger.Info(message);

    /// <summary>
    /// Traces the specified message.
    /// </summary>
    /// <param name="message">The resource.</param>
    public void Trace(string message) => _logger.Trace(message);

    /// <summary>
    /// Debugs the specified message.
    /// </summary>
    /// <param name="message">The resource.</param>
    public void Debug(string message) => _logger.Debug(message);

    /// <summary>
    /// Warnings the specified message.
    /// </summary>
    /// <param name="message">The resource.</param>
    public void Warning(string message) => _logger.Warn(message);

    /// <summary>
    /// Log Application Error.
    /// </summary>
    /// <param name="message">Non managed exception.</param>
    public void Error(string message) => _logger.Error(message);

    /// <summary>
    /// Log Application Error.
    /// </summary>
    /// <param name="ex">Non managed exception.</param>
    public void Error(Exception ex) => _logger.Error(ex);

    /// <summary>
    /// Log Critical Error that can crash application.
    /// </summary>
    /// <param name="ex">Critical non managed exception.</param>
    public void Fatal(Exception ex) => _logger.Fatal(ex);

    /// <summary>
    /// Log Critical Error that can crash application.
    /// </summary>
    /// <param name="message">Critical non managed exception.</param>
    public void Fatal(string message) => _logger.Fatal(message);

    #region ILogger

    public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);

        switch (logLevel)
        {
            case Microsoft.Extensions.Logging.LogLevel.Critical:
                if (exception is not null)
                    Fatal(exception);
                break;

            case Microsoft.Extensions.Logging.LogLevel.Trace:
                Trace(message);
                break;

            case Microsoft.Extensions.Logging.LogLevel.Debug:
                Debug(message);
                break;

            case Microsoft.Extensions.Logging.LogLevel.Information:
                Info(message);
                break;

            case Microsoft.Extensions.Logging.LogLevel.Warning:
                Warning(message);
                break;

            case Microsoft.Extensions.Logging.LogLevel.Error:
                if (exception is not null)
                    Error(exception);
                break;

            case Microsoft.Extensions.Logging.LogLevel.None:
                Info(message);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
        }
    }

    public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => true;

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
        => null!;

    #endregion ILogger
}
