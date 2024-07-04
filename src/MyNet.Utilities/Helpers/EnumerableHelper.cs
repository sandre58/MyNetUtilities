// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace MyNet.Utilities.Helpers
{
    public static class EnumerableHelper
    {
        public static void Iteration(int count, Action<int> action)
        {
            for (var i = 0; i < count; i++)
                action.Invoke(i);
        }

        public static IEnumerable<double> Range(double min, double max, double step = 1)
        {
            for (var i = min; i <= max; i += step)
                yield return i;
        }

        public static IEnumerable<int> Range(int min, int max, int step = 1)
        {
            if (step > 0)
                for (var i = min; i <= max; i += step)
                    yield return i;
            else
                for (var i = min; i >= max; i += step)
                    yield return i;
        }
    }
}
