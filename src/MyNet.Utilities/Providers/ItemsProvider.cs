// -----------------------------------------------------------------------
// <copyright file="ItemsProvider.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace MyNet.Utilities.Providers;

public class ItemsProvider<T>(IEnumerable<T> items) : IItemsProvider<T>
{
    public virtual IEnumerable<T> ProvideItems() => items;
}
