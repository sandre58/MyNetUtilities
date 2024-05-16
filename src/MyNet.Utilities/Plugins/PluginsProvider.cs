// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Caching;

namespace MyNet.Utilities.Plugins
{
    public class PluginsProvider(string root)
    {
        private readonly CacheStorage<Type, List<Type>> _cache = new();

        public string Root { get; private set; } = root;

        public List<Type> GetTypes<T>()
        {
            var types = _cache.Get(typeof(T));

            if (types is null)
            {
                types = PluginService.GetTypes<T>(Root).ToList();
                _cache.Add(typeof(T), types);
            }

            return types;
        }

        public Type? Get<T>(string? assemblyName = null)
        {
            var types = GetTypes<T>();

            return !string.IsNullOrEmpty(assemblyName) ? types.Find(x => x.Assembly.GetName().Name == assemblyName) : types.FirstOrDefault();
        }

        public T? Create<T>(string? assemblyName = null, params object[] constructorParameters)
        {
            var type = Get<T>(assemblyName);

            return type is null ? default : (T?)Activator.CreateInstance(type, constructorParameters);
        }
    }
}
