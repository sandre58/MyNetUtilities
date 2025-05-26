// -----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class DictionaryExtensions
{
    public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        where TKey : notnull
    {
        dictionary[key] = value;

        return dictionary[key];
    }

    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue newValue)
        where TKey : notnull
    {
        _ = dictionary.TryAdd(key, newValue);

        return dictionary[key];
    }

    public static void TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        where TKey : notnull
    {
        if (!dictionary.ContainsKey(key)) _ = dictionary.Remove(key);
    }

    public static TValue? GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue = default)
        where TKey : notnull => dictionary.TryGetValue(key, out var value) ? value : defaultValue;

    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this IEnumerable<IDictionary<TKey, TValue>> enumerable)
        where TKey : notnull
        => enumerable.SelectMany(x => x).ToDictionary(x => x.Key, y => y.Value);

    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dictionary1, IDictionary<TKey, TValue> dictionary2)
        where TKey : notnull
        => new[] { dictionary1, dictionary2 }.Merge();
}
