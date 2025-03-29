// -----------------------------------------------------------------------
// <copyright file="FutureDateException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Exceptions;

public class FutureDateException : TranslatableException
{
    public FutureDateException() { }

    public FutureDateException(string message, System.Exception innerException)
        : base(message, innerException) { }

    public FutureDateException(string property)
        : base("The field {0} must be in past.", "FieldXMustBeInPastError", property) { }
}
