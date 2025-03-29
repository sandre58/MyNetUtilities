// -----------------------------------------------------------------------
// <copyright file="PredicateItemsProvider.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities.Providers;

public class PredicateItemsProvider<T>(IItemsProvider<T> provider, Func<T, bool> predicate) : IItemsProvider<T>
{
    public PredicateItemsProvider(IEnumerable<T> items, Func<T, bool> predicate)
        : this(new ItemsProvider<T>(items), predicate) { }

    public virtual IEnumerable<T> ProvideItems() => provider.ProvideItems().Where(predicate);
}
