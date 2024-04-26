// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;

namespace MyNet.Utilities.Logging.NLog
{
    public sealed class LoggerProvider : ILoggerProvider
    {
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName) => new Logger(categoryName);

        public void Dispose()
        {
            // Method intentionally left empty.
        }

    }
}