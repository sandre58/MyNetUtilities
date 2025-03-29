// -----------------------------------------------------------------------
// <copyright file="EnumerableHelper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace MyNet.Utilities.Helpers;

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
        {
            for (var i = min; i <= max; i += step)
                yield return i;
        }
        else
        {
            for (var i = min; i >= max; i += step)
                yield return i;
        }
    }
}
