// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Mail.MailKit
{
    public class UndefinedServerException : Exception
    {
        public UndefinedServerException() : this("No server has been defined.") { }
        public UndefinedServerException(string? message) : base(message) { }
        public UndefinedServerException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
