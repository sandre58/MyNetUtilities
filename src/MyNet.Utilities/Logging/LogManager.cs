// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MyNet.Utilities.Logging
{
    /// <summary>
    /// Class representing a Logger.
    /// </summary>
    public static class LogManager
    {
        private static ILogger? _logger;

        public static void Initialize(ILogger logger) => _logger = logger;

        /// <summary>
        /// Information the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Info(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "") => _logger?.Info(message, memberName, sourceFilePath);

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Trace(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "") => _logger?.Trace(message, memberName, sourceFilePath);

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Debug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "") => _logger?.Debug(message, memberName, sourceFilePath);

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The resource.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Warning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "") => _logger?.Warning(message, memberName, sourceFilePath);

        /// <summary>
        /// Log Application Error.
        /// </summary>
        /// <param name="message">Non managed exception.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Error(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "") => _logger?.Error(message, memberName, sourceFilePath);

        /// <summary>
        /// Log Application Error.
        /// </summary>
        /// <param name="ex">Non managed exception.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Error(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "") => _logger?.Error(ex, memberName, sourceFilePath);

        /// <summary>
        /// Log Critical Error that can crash application.
        /// </summary>
        /// <param name="ex">Critical non managed exception.</param>
        /// <param name="memberName">The member name.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        public static void Fatal(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "") => _logger?.Fatal(ex, memberName, sourceFilePath);

        public static IDisposable MeasureTime(string title = "", TraceLevel traceLevel = TraceLevel.Trace)
        {
            var message = title;
            if (string.IsNullOrEmpty(message))
            {
                var st = new StackTrace(new StackFrame(1));
                var method = st.GetFrame(0)?.GetMethod();

                if (method != null)
                {
                    message = $"{method.DeclaringType}.{method.Name}({string.Join(", ", method.GetParameters().Select(x => x.Name))})";
                }
            }
            return new PerformanceLogger(message, traceLevel);
        }
    }
}
