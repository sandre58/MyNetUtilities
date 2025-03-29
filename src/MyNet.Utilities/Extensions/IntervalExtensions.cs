// -----------------------------------------------------------------------
// <copyright file="IntervalExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Sequences;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class IntervalExtensions
{
    public static IEnumerable<TClass> Merge<T, TClass>(this IEnumerable<TClass> intervals)
        where T : struct, IComparable
        where TClass : Interval<T, TClass>
    {
        var list = intervals.ToList();
        if (list.Count <= 1) return list;

        var result = new List<TClass>();

        TClass? previousInterval = null;
        foreach (var item in list.OrderBy(x => x.Start).ToList())
        {
            if (previousInterval is not null)
            {
                if (previousInterval.Union(item) is { } interval)
                {
                    result.Add(interval);
                    previousInterval = interval;
                }
                else
                {
                    result.Add(previousInterval);
                    previousInterval = item;
                }
            }
            else
            {
                previousInterval = item;
            }
        }

        return result;
    }
}
