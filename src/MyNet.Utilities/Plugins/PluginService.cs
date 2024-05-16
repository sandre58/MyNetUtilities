// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MyNet.Utilities.Plugins
{
    public static class PluginService
    {
        public static IEnumerable<Type> GetTypes<T>(string pluginsDirectory)
        {
            if (!Directory.Exists(pluginsDirectory)) return [];

            var result = new List<Type>();

            foreach (var item in new DirectoryInfo(pluginsDirectory).GetDirectories())
            {
                var dllPath = Path.Combine(item.FullName, $"{item.Name}.dll");

                if (File.Exists(dllPath))
                {
                    var plugin = LoadAssemblyFromDll(dllPath);

                    if (plugin is not null)
                        result.AddRange(plugin.GetTypes().Where(x => x.IsAssignableTo(typeof(T))));
                }
            }

            return result;
        }

        public static Type? GetType<T>(string pluginPath)
        {
            if (string.IsNullOrEmpty(pluginPath)) return default;

            var plugin = LoadAssemblyFromDll(pluginPath);

            return plugin is null ? null : Array.Find(plugin.GetTypes(), x => x.IsAssignableTo(typeof(T)));
        }

        public static T? CreateInstance<T>(string pluginPath, params object[] constructorParameters)
        {
            if (string.IsNullOrEmpty(pluginPath)) return default;

            var plugin = GetType<T>(pluginPath);

            return plugin is null ? default : (T?)Activator.CreateInstance(plugin, constructorParameters);
        }

        private static Assembly? LoadAssemblyFromDll(string dllPath, string? assemblyName = null)
        {
            if (!File.Exists(dllPath)) return null;

            var finalAssemblyName = assemblyName ?? Path.GetFileNameWithoutExtension(dllPath);

            var loadContext = new PluginLoadContext(dllPath);
            return loadContext.LoadFromAssemblyName(new AssemblyName(finalAssemblyName));
        }
    }
}
