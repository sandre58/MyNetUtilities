// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyNet.Utilities
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
            => source is ObservableCollection<T> obsSource ? obsSource : (new(source));

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static void ForEach<TObject>(this IEnumerable<TObject> source, Action<TObject, int> action)
        {
            var i = 0;
            foreach (var item in source)
            {
                action(item, i);
                i++;
            }
        }

        public static bool Contains(this IEnumerable collection, object value) => collection.OfType<object>().Any(x => Equals(x, value));

        public static TimeSpan Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector) => source.Select(selector).Aggregate(TimeSpan.Zero, (t1, t2) => t1 + t2);

        public static IEnumerable<T> Rotate<T>(this IEnumerable<T> list, int offset) => list.Skip(offset).Concat(list.Take(offset));

        public static double AverageOrDefault(this IEnumerable<int> values) =>
            values == null || !values.Any() ? default : values.Average();

        public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, int defaultValue = default) =>
            source == null || !source.Any() ? defaultValue : source.Average(selector);

        public static double AverageOrDefault(this IEnumerable<double> values) =>
            values == null || !values.Any() ? default : values.Average();

        public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector, double defaultValue = default) =>
            source == null || !source.Any() ? defaultValue : source.Average(selector);

        public static decimal AverageOrDefault(this IEnumerable<decimal> values) =>
            values == null || !values.Any() ? default : values.Average();

        public static decimal AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector, decimal defaultValue = default) =>
            source == null || !source.Any() ? defaultValue : source.Average(selector);

        public static T MaxOrDefault<T>(this IEnumerable<T> values, T defaultValue = default) where T : struct =>
            values == null || !values.Any() ? defaultValue : values.Max();

        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue = default) where TResult : struct =>
            source == null || !source.Any() ? defaultValue : source.Max(selector);

        public static T MinOrDefault<T>(this IEnumerable<T> values, T defaultValue = default) where T : struct =>
            values == null || !values.Any() ? defaultValue : values.Min();

        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue = default) where TResult : struct =>
            source == null || !source.Any() ? defaultValue : source.Min(selector);

        public static T? GetByIdOrDefault<T, TId>(this IEnumerable<T> source, TId? id) where T : IIdentifiable<TId> => source.FirstOrDefault(x => Equals(x.Id, id));

        public static T GetById<T, TId>(this IEnumerable<T> source, TId id) where T : IIdentifiable<TId> => source.First(x => Equals(x.Id, id));

        public static bool HasId<T, TId>(this IEnumerable<T> source, TId id) where T : IIdentifiable<TId> => source.Any(x => Equals(x.Id, id));

        public static IEnumerable<IEnumerable<(T item1, T item2)>> RoundRobin<T>(this IEnumerable<T> items)
        {
            var result = new List<List<(T item1, T item2)>>();
            var list = items.OfType<T?>().ToList();

            if (list.Count % 2 != 0)
                list.Add(default);

            var countRounds = list.Count - 1;
            var countItemsByRound = list.Count / 2;

            for (var roundIndex = 0; roundIndex < countRounds; roundIndex++)
            {
                var round = new List<(T item1, T item2)>();

                for (var i = 0; i < countItemsByRound; i++)
                {
                    var item1 = list[i];
                    var item2 = list[list.Count - i - 1];

                    if (item1 is not null && item2 is not null)
                        round.Add((item1, item2));
                }

                result.Add(round);

                // Rotate the list.
                list = [list[0], list[list.Count - 1], .. list.Skip(1).Take(list.Count - 2)];
            }

            return result;
        }
    }
}
