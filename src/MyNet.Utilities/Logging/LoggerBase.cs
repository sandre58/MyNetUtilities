// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace MyNet.Utilities.Logging
{
    /// <summary>
    /// Logger Base.
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
        /// <summary>
        /// Log Trace.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Trace(string message,
            [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Debug.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Debug(string message,
            [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log information.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Info(string message,
            [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Warning.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Warning(string message, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Application Error.
        /// </summary>
        /// <param name="message">Non managed exception.</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Error(string message, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Application Error.
        /// </summary>
        /// <param name="ex">Non managed exception.</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Error(Exception ex, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Critical Error that can crash application.
        /// </summary>
        /// <param name="message">Critical non managed exception.</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Fatal(string message, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");

        /// <summary>
        /// Log Critical Error that can crash application.
        /// </summary>
        /// <param name="ex">Critical non managed exception.</param>
        /// <param name="memberName">The member Name.</param>
        /// <param name="sourceFilePath">The source File Path.</param>
        public abstract void Fatal(Exception ex, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "");
    }
}
