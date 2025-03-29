// -----------------------------------------------------------------------
// <copyright file="SortableObservableCollection.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace MyNet.Utilities.Collections;

public class SortableObservableCollection<T> : ThreadSafeObservableCollection<T>
{
    public SortableObservableCollection() { }

    public SortableObservableCollection(Collection<T> list)
        : base(list) { }

    public SortableObservableCollection(IEnumerable<T> collection)
        : base(collection) { }

    public SortableObservableCollection(Func<T, object> sortSelector, ListSortDirection direction = ListSortDirection.Ascending) => (SortSelector, SortDirection) = (sortSelector, direction);

    public Func<T, object>? SortSelector { get; set; }

    public ListSortDirection SortDirection { get; set; }

    public void Sort()
    {
        if (SortSelector is null || Count < 2) return;

        ExecuteThreadSafe(() =>
        {
            var query = this.Select((x, index) => (Item: x, Index: index));

            query = SortDirection == ListSortDirection.Ascending ? query.OrderBy(x => SortSelector.Invoke(x.Item)) : query.OrderByDescending(x => SortSelector.Invoke(x.Item));

            var map = query.Select((x, index) => (OldIndex: x.Index, NewIndex: index)).Where(o => o.OldIndex != o.NewIndex);

            using var enumerator = map.GetEnumerator();
            if (enumerator.MoveNext())
                Move(enumerator.Current.OldIndex, enumerator.Current.NewIndex);
        });
    }

    protected override void InvokeNotifyCollectionChanged(NotifyCollectionChangedEventHandler notifyEventHandler, NotifyCollectionChangedEventArgs e)
    {
        base.InvokeNotifyCollectionChanged(notifyEventHandler, e);

        if (SortSelector is null || e.Action == NotifyCollectionChangedAction.Remove)
            return;

        Sort();
    }
}
