﻿// -----------------------------------------------------------------------
// <copyright file="ReflectionComparer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyNet.Utilities.Comparers;

public class ReflectionComparer<T>(IList<ReflectionSortDescription> sortDescriptions) : IComparer, IComparer<T>
{
    public int Compare(object? x, object? y) => Compare((T?)x, (T?)y);

    public int Compare(T? x, T? y)
    {
        var result = 0;

        foreach (var item in sortDescriptions)
        {
            var obj1 = x?.GetDeepPropertyValue(item.Path);
            var obj2 = y?.GetDeepPropertyValue(item.Path);

            result = obj1 switch
            {
                IComparable toCompare1 => toCompare1.CompareTo(obj2),
                null => obj2 != null ? -1 : 0,
                _ => obj2 != null ? 1 : -1
            };
            result *= item.Direction == ListSortDirection.Descending ? -1 : 1;

            if (result != 0)
            {
                break;
            }
        }

        return result;
    }
}

public class ReflectionSortDescription(string path, ListSortDirection direction = ListSortDirection.Ascending)
{
    public string Path { get; } = path;

    public ListSortDirection Direction { get; } = direction;
}
