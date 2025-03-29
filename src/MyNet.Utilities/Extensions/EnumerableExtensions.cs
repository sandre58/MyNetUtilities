// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class EnumerableExtensions
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        => source as ObservableCollection<T> ?? [.. source];

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

    public static IEnumerable<T> Rotate<T>(this IEnumerable<T> values, int offset)
    {
        var list = values.ToList();
        return list.Skip(offset).Concat(list.Take(offset));
    }

    public static double AverageOrDefault(this IEnumerable<int> values)
    {
        var list = values.ToList();
        return list.Count == 0 ? 0 : list.Average();
    }

    public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, int defaultValue = 0)
    {
        var list = source.ToList();
        return list.Count == 0 ? defaultValue : list.Average(selector);
    }

    public static double AverageOrDefault(this IEnumerable<double> values)
    {
        var list = values.ToList();
        return list.Count == 0 ? 0 : list.Average();
    }

    public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector, double defaultValue = 0)
    {
        var list = source.ToList();
        return list.Count == 0 ? defaultValue : list.Average(selector);
    }

    public static decimal AverageOrDefault(this IEnumerable<decimal> values)
    {
        var list = values.ToList();
        return list.Count == 0 ? 0 : list.Average();
    }

    public static decimal AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector, decimal defaultValue = 0)
    {
        var list = source.ToList();
        return list.Count == 0 ? defaultValue : list.Average(selector);
    }

    public static T MaxOrDefault<T>(this IEnumerable<T> values, T defaultValue = default)
        where T : struct
    {
        var list = values.ToList();
        return list.Count == 0 ? defaultValue : list.Max();
    }

    public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue = default)
        where TResult : struct
    {
        var list = source.ToList();
        return list.Count == 0 ? defaultValue : list.Max(selector);
    }

    public static T MinOrDefault<T>(this IEnumerable<T> values, T defaultValue = default)
        where T : struct
    {
        var list = values.ToList();
        return list.Count == 0 ? defaultValue : list.Min();
    }

    public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue = default)
        where TResult : struct
    {
        var list = source.ToList();
        return list.Count == 0 ? defaultValue : list.Min(selector);
    }

    public static T? GetByIdOrDefault<T, TId>(this IEnumerable<T> source, TId? id)
        where T : IIdentifiable<TId>
        => source.FirstOrDefault(x => Equals(x.Id, id));

    public static T GetById<T, TId>(this IEnumerable<T> source, TId id)
        where T : IIdentifiable<TId>
        => source.First(x => Equals(x.Id, id));

    public static bool HasId<T, TId>(this IEnumerable<T> source, TId id)
        where T : IIdentifiable<TId>
        => source.Any(x => Equals(x.Id, id));

    public static IEnumerable<IEnumerable<(T Item1, T Item2)>> RoundRobin<T>(this IEnumerable<T> items)
    {
        var result = new List<List<(T Item1, T Item2)>>();
        var list = items.ToList();

        if (list.Count % 2 != 0)
            list.Add(default!);

        var countRounds = list.Count - 1;
        var countItemsByRound = list.Count / 2;

        for (var roundIndex = 0; roundIndex < countRounds; roundIndex++)
        {
            var round = new List<(T Item1, T Item2)>();

            for (var i = 0; i < countItemsByRound; i++)
            {
                var item1 = list[i];
                var item2 = list[list.Count - i - 1];

                if (item1 is not null && item2 is not null)
                    round.Add((item1, item2));
            }

            result.Add(round);

            // Rotate the list.
            list = [list[0], list[^1], .. list.Skip(1).Take(list.Count - 2)];
        }

        return result;
    }
}
