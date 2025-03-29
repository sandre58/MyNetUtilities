// -----------------------------------------------------------------------
// <copyright file="InvalidPhoneException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Exceptions;

public class InvalidPhoneException : TranslatableException
{
    public InvalidPhoneException() { }

    public InvalidPhoneException(string message, System.Exception innerException)
        : base(message, innerException) { }

    public InvalidPhoneException(string propertyName)
        : base("Field {0} must be a valid phone number.", "FieldXMustBeValidPhoneNumberError", propertyName) { }
}
