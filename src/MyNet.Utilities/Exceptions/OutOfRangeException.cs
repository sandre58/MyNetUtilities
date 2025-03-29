// -----------------------------------------------------------------------
// <copyright file="OutOfRangeException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Exceptions;

public class OutOfRangeException : TranslatableException
{
    public OutOfRangeException()
        : base("the specified value is out of range.") { }

    public OutOfRangeException(string message, System.Exception innerException)
        : base(message, innerException) { }

    public OutOfRangeException(string message)
        : base(message) { }

    public OutOfRangeException(string property, object min, object max)
        : base("the field '{0}' is out of range.", "FieldXMustBeBetweenYAndZError", property, min, max) { }
}
