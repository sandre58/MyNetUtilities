// -----------------------------------------------------------------------
// <copyright file="QueryLimitExceededException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Utilities.Google.Maps;

public class QueryLimitExceededException : System.Net.WebException
{
    public QueryLimitExceededException()
    {
    }

    public QueryLimitExceededException(string? message)
        : base(message)
    {
    }

    public QueryLimitExceededException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
