// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
namespace MyNet.Utilities.Mail.MailKit
{
    public class EmptySenderAddressesException : Exception
    {
        public EmptySenderAddressesException() : this("No sender addresses has been defined.") { }
        public EmptySenderAddressesException(string? message) : base(message) { }
        public EmptySenderAddressesException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
