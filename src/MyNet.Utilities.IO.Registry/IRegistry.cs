// -----------------------------------------------------------------------
// <copyright file="IRegistry.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.IO.Registry;

public interface IRegistry
{
    string Key { get; }

    string Parent { get; }
}
