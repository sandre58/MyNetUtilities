// -----------------------------------------------------------------------
// <copyright file="ISettable.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities;

public interface ISettable<in T>
{
    void SetFrom(T? from);
}

public interface ISettable
{
    void SetFrom(object? from);
}
