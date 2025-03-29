// -----------------------------------------------------------------------
// <copyright file="WeightedRandom.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities.Generator;

public class WeightedRandom<T> : Dictionary<T, double>
    where T : notnull
{
    public T Random()
    {
        var random = RandomGenerator.Double() * Values.Sum();

        var accumulatedWeight = 0.0;
        foreach (var entry in this)
        {
            accumulatedWeight += entry.Value;
            if (accumulatedWeight >= random)
                return entry.Key;
        }

        throw new InvalidOperationException("There are not entries");
    }

    public WeightedRandom<T> Filter(Func<T, bool> predicate)
    {
        var result = new WeightedRandom<T>();
        result.AddRange(this.Where(x => predicate(x.Key)));

        return result;
    }
}
