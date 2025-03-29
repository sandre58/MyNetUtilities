// -----------------------------------------------------------------------
// <copyright file="ReadOnlyObservableKeyedCollection.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.ObjectModel;

namespace MyNet.Utilities.Collections;

public class ReadOnlyObservableKeyedCollection<TKey, T>(ObservableKeyedCollection<TKey, T> list) : ReadOnlyObservableCollection<T>(list)
    where TKey : notnull
{
    public T? this[TKey key] => ((ObservableKeyedCollection<TKey, T>)Items)[key];
}
