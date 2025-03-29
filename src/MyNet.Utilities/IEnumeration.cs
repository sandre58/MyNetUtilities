// -----------------------------------------------------------------------
// <copyright file="IEnumeration.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities;

public interface IEnumeration
{
    string ResourceKey { get; }

    string Name { get; }

    object Value { get; }
}
