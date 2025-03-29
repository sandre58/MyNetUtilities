// -----------------------------------------------------------------------
// <copyright file="TranslatableException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;

namespace MyNet.Utilities.Exceptions;

[System.Runtime.InteropServices.ComVisible(true)]
public class TranslatableException(string? message, Exception? innerException, string resourceKey, params object?[] stringFormatParameters) : Exception(string.Format(CultureInfo.CurrentCulture, message.OrEmpty(), stringFormatParameters), innerException)
{
    public TranslatableException()
        : this(string.Empty) { }

    public TranslatableException(string message, Exception innerException)
        : this(message, innerException, string.Empty) { }

    public TranslatableException(string message)
        : this(message, null, string.Empty) { }

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

    public string ResourceKey { get; } = resourceKey;

    public object?[]? Parameters { get; } = stringFormatParameters;
}
