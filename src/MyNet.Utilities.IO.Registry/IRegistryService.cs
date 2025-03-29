// -----------------------------------------------------------------------
// <copyright file="IRegistryService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace MyNet.Utilities.IO.Registry;

public interface IRegistryService
{
    void AddOrUpdate<T>(RegistryEntry<T> value);

    int Count(string path);

    RegistryEntry<T>? Get<T>(string path)
        where T : new();

    RegistryEntry<T>? Get<T>(string parentKey, string key)
        where T : new();

    IEnumerable<RegistryEntry<T>> GetAll<T>(string parentKey)
        where T : new();

    bool KeyExist(string key);

    void Remove(string parentKey, string key);

    void Remove(string path);

    string? SearchKeyByValue<T>(string parentKey, string valueKey, T value);

    void Set<T>(string parentKey, string valueKey, T value)
        where T : notnull;
}
