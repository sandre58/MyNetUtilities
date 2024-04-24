// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities.Generator
{
    public class WeightedRandom<T> : Dictionary<T, double> where T : notnull
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
}
