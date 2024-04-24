// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Google.Maps
{
    public class QueryLimitExceededException : System.Net.WebException
    {
        public QueryLimitExceededException()
        {
        }

        public QueryLimitExceededException(string? message) : base(message)
        {
        }

        public QueryLimitExceededException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
