// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace MyNet.Utilities.IO.Registry
{
    public interface IRegistryService
    {
        void AddOrUpdate<T>(Registry<T> value);

        int Count(string path);

        Registry<T>? Get<T>(string path) where T : new();

        Registry<T>? Get<T>(string parentKey, string key) where T : new();

        IEnumerable<Registry<T>> GetAll<T>(string parentKey) where T : new();

        bool KeyExist(string key);

        void Remove(string parentKey, string key);

        void Remove(string path);

        string? SearchKeyByValue<T>(string parentKey, string valueKey, T value);

        void Set<T>(string parentKey, string valueKey, T value) where T : notnull;
    }
}
