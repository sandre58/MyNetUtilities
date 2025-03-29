// -----------------------------------------------------------------------
// <copyright file="IsNotLowerOrEqualsThanException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.Exceptions;

public class IsNotLowerOrEqualsThanException : TranslatableException
{
    public IsNotLowerOrEqualsThanException() { }

    public IsNotLowerOrEqualsThanException(string message, Exception innerException)
        : base(message, innerException) { }

    public IsNotLowerOrEqualsThanException(string message)
        : base(message) { }

    public IsNotLowerOrEqualsThanException(string property, object? target)
        : base("the value of {0} must be lower than {1}.", "FieldXMustBeLowerOrEqualsThanYError", property, target is DateTime date1 ? date1.ToLocalTime() : target) { }
}
