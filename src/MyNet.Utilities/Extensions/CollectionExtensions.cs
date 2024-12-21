// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public static class CollectionExtensions
    {
        public static void Set<T>(this ICollection<T> collection, IEnumerable<T>? items)
        {
            collection.Clear();
            collection.AddRange(items);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T>? items)
        {
            if (collection is List<T> list)
                list.AddRange(items ?? []);
            else
                foreach (var item in items ?? [])
                    collection.Add(item);
        }

        public static void Sort<T, TKey>(this ICollection<T> collection, Func<T, TKey> keySelector)
        {
            var items = collection.OrderBy(keySelector).ToArray();
            collection.Set(items);
        }

        public static void Sort<T, TKey>(this ICollection<T> collection, Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            var items = collection.OrderBy(keySelector, comparer).ToArray();
            collection.Set(items);
        }

        public static void SortDescending<T, TKey>(this ICollection<T> collection, Func<T, TKey> keySelector)
        {
            var items = collection.OrderByDescending(keySelector).ToArray();
            collection.Set(items);
        }
    }
}
