// -----------------------------------------------------------------------
// <copyright file="ISimilar.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities;

public interface ISimilar
{
    bool IsSimilar(object? obj);
}

public interface ISimilar<in T>
{
    bool IsSimilar(T? obj);
}
