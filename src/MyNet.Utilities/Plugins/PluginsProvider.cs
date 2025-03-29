// -----------------------------------------------------------------------
// <copyright file="PluginsProvider.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Caching;

namespace MyNet.Utilities.Plugins;

public class PluginsProvider(string root)
{
    private readonly CacheStorage<Type, List<Type>> _cache = new();

    public string Root { get; } = root;

    public IList<Type> GetTypes<T>()
    {
        var types = _cache.Get(typeof(T));

        if (types is not null) return types;
        types = [.. PluginService.GetTypes<T>(Root)];
        _cache.Add(typeof(T), types);

        return types;
    }

    public Type? Get<T>(string? assemblyName = null)
    {
        var types = GetTypes<T>();

        return !string.IsNullOrEmpty(assemblyName) ? types.FirstOrDefault(x => x.Assembly.GetName().Name == assemblyName) : types.FirstOrDefault();
    }

    public T? Create<T>(string? assemblyName = null, params object[] constructorParameters)
    {
        var type = Get<T>(assemblyName);

        return type is null ? default : (T?)Activator.CreateInstance(type, constructorParameters);
    }
}
