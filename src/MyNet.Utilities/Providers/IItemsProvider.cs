// -----------------------------------------------------------------------
// <copyright file="IItemsProvider.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace MyNet.Utilities.Providers;

public interface IItemsProvider<out T>
{
    IEnumerable<T> ProvideItems();
}
