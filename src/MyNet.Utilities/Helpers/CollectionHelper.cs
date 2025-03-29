// -----------------------------------------------------------------------
// <copyright file="CollectionHelper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Linq;

namespace MyNet.Utilities.Helpers;

public static class CollectionHelper
{
    public static void OnCollectionChanged<T>(IEnumerable? oldItems, IEnumerable? newItems, Action<T> actionOnOldItems, Action<T> actionOnNewItems)
    {
        oldItems?.OfType<T>().ToList().ForEach(actionOnOldItems.Invoke);
        newItems?.OfType<T>().ToList().ForEach(actionOnNewItems.Invoke);
    }
}
