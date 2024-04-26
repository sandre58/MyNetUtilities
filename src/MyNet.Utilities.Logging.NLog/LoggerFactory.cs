// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Logging.NLog
{
    public static class LoggerFactory
    {
        public static ILogger CreateLogger(string categoryName) => new Logger(categoryName);

        public static ILogger CreateLogger() => new Logger();
    }
}
