// -----------------------------------------------------------------------
// <copyright file="RegistryService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Versioning;
using Microsoft.Win32;

namespace MyNet.Utilities.IO.Registry;

public class RegistryService : IRegistryService
{
    private readonly RegistryKey _registryKey;

    [SupportedOSPlatform("windows")]
    private readonly Dictionary<Type, Func<RegistryValueKind>> _typeMapping = new()
    {
        [typeof(ushort)] = () => RegistryValueKind.DWord,
        [typeof(short)] = () => RegistryValueKind.DWord,
        [typeof(int)] = () => RegistryValueKind.DWord,
        [typeof(uint)] = () => RegistryValueKind.DWord,
        [typeof(long)] = () => RegistryValueKind.QWord,
        [typeof(ulong)] = () => RegistryValueKind.QWord,
        [typeof(string)] = () => RegistryValueKind.String,
        [typeof(bool)] = () => RegistryValueKind.DWord
    };

    private readonly Dictionary<Type, Func<object, object>> _converters = new()
    {
        [typeof(bool)] = x => Convert.ToBoolean(x, CultureInfo.InvariantCulture)
    };

    [SupportedOSPlatform("windows")]
    public RegistryService() => _registryKey = Microsoft.Win32.Registry.CurrentUser;

    [SupportedOSPlatform("windows")]
    public int Count(string path) => _registryKey.OpenSubKey(path)?.SubKeyCount ?? 0;

    [SupportedOSPlatform("windows")]
    public void Remove(string parentKey, string key)
    {
        using var item = _registryKey.OpenSubKey(parentKey, true);
        item?.DeleteSubKeyTree(key);
    }

    [SupportedOSPlatform("windows")]
    public void Remove(string path) => _registryKey.DeleteSubKey(path);

    [SupportedOSPlatform("windows")]
    public IEnumerable<RegistryEntry<T>> GetAll<T>(string parentKey)
        where T : new()
    {
        var keys = _registryKey.OpenSubKey(parentKey)?.GetSubKeyNames();
        return keys?.Select(x => Get<T>(parentKey, x)!) ?? [];
    }

    [SupportedOSPlatform("windows")]
    public RegistryEntry<T>? Get<T>(string parentKey, string key)
        where T : new()
        => Get<T>($"{parentKey}\\{key}");

    [SupportedOSPlatform("windows")]
    public RegistryEntry<T>? Get<T>(string path)
        where T : new()
    {
        var type = typeof(T);
        var properties = type.GetPublicProperties().Where(x => x.CanWrite);

        var result = new RegistryEntry<T>(path, new T());

        var itemFullKey = result.GetItemFullKey();
        using var subKey = _registryKey.OpenSubKey(itemFullKey);
        if (subKey == null) return null;

        foreach (var item in properties)
        {
            var converter = GetConverters(item.PropertyType);
            var val = subKey.GetValue(item.Name, default(T));
            if (val == null) continue;
            var value = converter != null ? converter(val) : val;
            item.SetValue(result.Item, value);
        }

        return result;
    }

    [SupportedOSPlatform("windows")]
    public void Set<T>(string parentKey, string valueKey, T value)
        where T : notnull
    {
        using var subKey = _registryKey.CreateSubKey(parentKey);
        subKey.SetValue(valueKey, value);
    }

    [SupportedOSPlatform("windows")]
    public void AddOrUpdate<T>(RegistryEntry<T> value)
    {
        var type = typeof(T);
        var properties = type.GetPublicProperties().Where(x => x.CanWrite);

        using var subKey = _registryKey.CreateSubKey(value.GetItemFullKey());
        foreach (var item in properties)
        {
            var val = item.GetValue(value.Item, null);
            if (val != null)
                subKey.SetValue(item.Name, val, GetRegistryType(item.PropertyType));
            else if (subKey.OpenSubKey(item.Name) is not null)
                subKey.DeleteValue(item.Name);
        }
    }

    [SupportedOSPlatform("windows")]
    public string? SearchKeyByValue<T>(string parentKey, string? valueKey, T value)
    {
        using var subKey = _registryKey.OpenSubKey(parentKey);
        if (subKey == null)
            return null;

        var keys = subKey.GetSubKeyNames();

        foreach (var key in keys)
        {
            try
            {
                var currentValue = subKey.OpenSubKey(key)?.GetValue(valueKey);

                if (Comparer<T>.Default.Compare(value, (T?)currentValue) == 0)
                    return key;
            }
            catch
            {
                // Do nothing, try another key
            }
        }

        return null;
    }

    [SupportedOSPlatform("windows")]
    public bool KeyExist(string key)
    {
        using var subKey = _registryKey.OpenSubKey(key);
        return subKey != null;
    }

    [SupportedOSPlatform("windows")]
    private RegistryValueKind GetRegistryType(Type type) => !_typeMapping.TryGetValue(type, out var value) ? RegistryValueKind.Unknown : value();

    private Func<object, object>? GetConverters(Type type) => _converters.GetValueOrDefault(type);
}
