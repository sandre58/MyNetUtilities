﻿// -----------------------------------------------------------------------
// <copyright file="IExecuteWithObjectAndResult.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Messaging;

/// <summary>
/// This interface is meant for the <see cref="WeakFunc{T}" /> class and can be
/// useful if you store multiple WeakFunc{T} instances but don't know in advance
/// what type T represents.
/// </summary>
public interface IExecuteWithObjectAndResult
{
    /// <summary>
    /// Executes a Func and returns the result.
    /// </summary>
    /// <param name="parameter">A parameter passed as an object,
    /// to be casted to the appropriate type.</param>
    /// <returns>The result of the operation.</returns>
    object? ExecuteWithObject(object? parameter);
}
