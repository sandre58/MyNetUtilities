// -----------------------------------------------------------------------
// <copyright file="RegistryEntry.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace MyNet.Utilities.IO.Registry;

public class RegistryEntry<T> : IRegistry
{
    public RegistryEntry(string path, T item)
    {
        var split = path.Split('\\');
        Key = split.LastOrDefault() ?? string.Empty;
        Parent = string.Join('\\', split.Take(split.Length - 1));
        Item = item;
    }

    public RegistryEntry(string key, string parent, T item)
    {
        Key = key;
        Parent = parent;
        Item = item;
    }

    public string Key { get; }

    public string Parent { get; }

    public T Item { get; }
}
