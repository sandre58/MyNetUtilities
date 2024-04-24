// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Google.Maps
{
    public class RequestDeniedException : Exception
    {
        public RequestDeniedException()
        {
        }

        public RequestDeniedException(string? message) : base(message) { }

        public RequestDeniedException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
