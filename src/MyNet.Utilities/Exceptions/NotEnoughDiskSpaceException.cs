// -----------------------------------------------------------------------
// <copyright file="NotEnoughDiskSpaceException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Exceptions;

public class NotEnoughDiskSpaceException : TranslatableException
{
    public NotEnoughDiskSpaceException()
        : base("Not enough disk space.", "NotEnoughSpaceDisk") { }

    public NotEnoughDiskSpaceException(string message, System.Exception innerException)
        : base(message, innerException) { }

    public NotEnoughDiskSpaceException(string message)
        : base(message) { }
}
