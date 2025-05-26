// -----------------------------------------------------------------------
// <copyright file="IRegistryBaseExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.IO.Registry;

public static class IRegistryBaseExtensions
{
    public static string GetItemFullKey(this IRegistry registryFile) => $@"{registryFile.Parent}\{registryFile.Key}";
}
