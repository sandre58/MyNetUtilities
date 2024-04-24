// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities
{
    public static class DictionaryExtensions
    {
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
            where TKey : notnull
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
            else
                dictionary[key] = value;

            return dictionary[key];
        }

        public static TValue TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue newValue)
            where TKey : notnull
        {
            if (!dictionary.ContainsKey(key)) dictionary.Add(key, newValue);

            return dictionary[key];
        }

        public static void TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TKey : notnull
        {
            if (!dictionary.ContainsKey(key)) dictionary.Remove(key);
        }

        public static TValue? GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue = default)
            where TKey : notnull => dictionary.TryGetValue(key, out var value) ? value : defaultValue;

        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this IEnumerable<IDictionary<TKey, TValue>> enumerable) where TKey : notnull
            => enumerable.SelectMany(x => x).ToDictionary(x => x.Key, y => y.Value);

        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dictionary1, IDictionary<TKey, TValue> dictionary2) where TKey : notnull
            => new IDictionary<TKey, TValue>[] { dictionary1, dictionary2 }.Merge();
    }
}
