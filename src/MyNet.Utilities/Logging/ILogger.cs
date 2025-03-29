// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.Logging;

/// <summary>
/// Interface for logging Library.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Log Trace.
    /// </summary>
    /// <param name="message">The resource.</param>
    void Trace(string message);

    /// <summary>
    /// Log Debug.
    /// </summary>
    /// <param name="message">The resource.</param>
    void Debug(string message);

    /// <summary>
    /// Log information.
    /// </summary>
    /// <param name="message">The resource.</param>
    void Info(string message);

    /// <summary>
    /// Log Warning.
    /// </summary>
    /// <param name="message">The resource.</param>
    void Warning(string message);

    /// <summary>
    /// Log Application Error.
    /// </summary>
    /// <param name="message">Non managed exception.</param>
    void Error(string message);

    /// <summary>
    /// Log Application Error.
    /// </summary>
    /// <param name="ex">Non managed exception.</param>
    void Error(Exception ex);

    /// <summary>
    /// Log Critical Error that can crash application.
    /// </summary>
    /// <param name="message">Critical non managed exception.</param>
    void Fatal(string message);

    /// <summary>
    /// Log Critical Error that can crash application.
    /// </summary>
    /// <param name="ex">Critical non managed exception.</param>
    void Fatal(Exception ex);
}
