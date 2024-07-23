// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Sequences;

namespace MyNet.Utilities
{
    public static class IntervalExtensions
    {
        public static IEnumerable<TClass> Merge<T, TClass>(this IEnumerable<TClass> intervals)
            where T : struct, IComparable
            where TClass : Interval<T, TClass>
        {
            if (intervals.Count() <= 1) return intervals;

            var result = new List<TClass>();

            TClass? previousInterval = null;
            foreach (var item in intervals.OrderBy(x => x.Start).ToList())
            {
                if (previousInterval is not null)
                {
                    if (previousInterval.Union(item) is TClass interval)
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
                    previousInterval = item;
            }

            return result;
        }
    }
}
