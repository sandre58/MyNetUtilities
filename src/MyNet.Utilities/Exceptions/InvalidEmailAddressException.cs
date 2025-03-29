// -----------------------------------------------------------------------
// <copyright file="InvalidEmailAddressException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Exceptions;

public class InvalidEmailAddressException : TranslatableException
{
    public InvalidEmailAddressException() { }

    public InvalidEmailAddressException(string message, System.Exception innerException)
        : base(message, innerException) { }

    public InvalidEmailAddressException(string propertyName)
        : base("Field {0} must be a valid email address.", "FieldXMustBeValidEmailAddressError", propertyName) { }
}
