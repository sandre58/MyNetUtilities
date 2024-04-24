// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Exceptions
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class TranslatableException : Exception
    {
        public string ResourceKey { get; }

        public object?[]? Parameters { get; }

        public TranslatableException(string? message, Exception? innerException, string resourceKey, params object?[] stringFormatParameters)
            : base(message, innerException)
        {
            ResourceKey = resourceKey;
            Parameters = stringFormatParameters;
        }

        public TranslatableException(Exception? innerException, string resourceKey, params object?[] stringFormatParameters)
            : this(null, innerException, resourceKey, stringFormatParameters)
        {
        }

        public TranslatableException(string? message, string resourceKey, params object?[] stringFormatParameters)
            : this(message, null, resourceKey, stringFormatParameters)
        {
        }

        public TranslatableException(string resourceKey, params object?[] stringFormatParameters)
            : this(null, null, resourceKey, stringFormatParameters)
        {
        }
    }
}
