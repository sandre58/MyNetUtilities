// -----------------------------------------------------------------------
// <copyright file="IIdentifiable.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities;

public interface IIdentifiable<out T>
{
    T Id { get; }
}
