// -----------------------------------------------------------------------
// <copyright file="FileAlreadyUsedException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Exceptions;

public class FileAlreadyUsedException : TranslatableException
{
    public FileAlreadyUsedException() { }

    public FileAlreadyUsedException(string message, System.Exception innerException)
        : base(message, innerException) { }

    public FileAlreadyUsedException(string filename)
        : base("File {0} is used by another process", "FileXAlreadyUsedError", filename) { }
}
