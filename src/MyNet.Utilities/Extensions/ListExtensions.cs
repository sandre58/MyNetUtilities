// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public static class ListExtensions
    {
        public static T? GetByIndex<T>(this IList<T> source, int index, T? defaultValue = default) => index >= 0 && index < source.Count ? source[index] : defaultValue;

        public static void Swap(this IList list, int firstIndex, int secondIndex)
        {

            if (firstIndex == secondIndex)
            {
                return;
            }

            (list[secondIndex], list[firstIndex]) = (list[firstIndex], list[secondIndex]);
        }

        public static void UpdateFrom<TSource, TDestination>(this IList<TDestination> destination,
            IEnumerable<TSource> source,
            Action<TSource> add,
            Action<TDestination> remove,
            Action<TDestination, TSource> update,
            Func<TSource, TDestination, bool> predicate)
        {
            var sourceList = source.ToList();

            // Delete
            var toDelete = destination.Where(x => !sourceList.Exists(y => predicate(y, x))).ToList();
            toDelete.ForEach(remove);

            // Update
            var toUpdate = destination.Where(x => sourceList.Exists(y => predicate(y, x))).ToList();
            toUpdate.ForEach(x => update(x, sourceList.First(y => predicate(y, x))));

            // Add
            var toAdd = sourceList.Where(x => !destination.Any(y => predicate(x, y))).ToList();
            toAdd.ForEach(add);
        }
    }
}
