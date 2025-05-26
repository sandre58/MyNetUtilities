// -----------------------------------------------------------------------
// <copyright file="ThreadSafeObservableCollection.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

#if NET9_0_OR_GREATER
using System.Threading;
#endif

namespace MyNet.Utilities.Collections;

public class ThreadSafeObservableCollection<T> : OptimizedObservableCollection<T>
{
#if NET9_0_OR_GREATER
    private readonly Lock _localLock = new();
#else
    private readonly object _localLock = new();
#endif

    private readonly Action<Action>? _notifyOnUi;

    public ThreadSafeObservableCollection(Action<Action>? notifyOnUi = null) => _notifyOnUi = notifyOnUi;

    public ThreadSafeObservableCollection(Collection<T> list, Action<Action>? notifyOnUi = null)
        : base(list) => _notifyOnUi = notifyOnUi;

    public ThreadSafeObservableCollection(IEnumerable<T> collection, Action<Action>? notifyOnUi = null)
        : base(collection) => _notifyOnUi = notifyOnUi;

    public override event NotifyCollectionChangedEventHandler? CollectionChanged;

    protected override void InsertItem(int index, T item) => ExecuteThreadSafe(() => base.InsertItem(index, item));

    protected override void MoveItem(int oldIndex, int newIndex) => ExecuteThreadSafe(() => base.MoveItem(oldIndex, newIndex));

    protected override void RemoveItem(int index) => ExecuteThreadSafe(() => base.RemoveItem(index));

    protected override void SetItem(int index, T item) => ExecuteThreadSafe(() => base.SetItem(index, item));

    protected override void ClearItems() => ExecuteThreadSafe(base.ClearItems);

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        var collectionChanged = CollectionChanged;
        if (collectionChanged == null) return;

        using (BlockReentrancy())
        {
            NotifyCollectionChanged(e, collectionChanged);
        }
    }

    protected virtual void InvokeNotifyCollectionChanged(NotifyCollectionChangedEventHandler notifyEventHandler, NotifyCollectionChangedEventArgs e) => notifyEventHandler.Invoke(this, e);

    protected void ExecuteThreadSafe(Action action)
    {
#if NET9_0_OR_GREATER
        _localLock.Enter();
        try
        {
            action();
        }
        finally
        {
            _localLock.Exit();
        }
#else
        lock (_localLock)
        {
            action();
        }
#endif
    }

    private void NotifyCollectionChanged(NotifyCollectionChangedEventArgs e, NotifyCollectionChangedEventHandler collectionChanged)
    {
        foreach (var notifyEventHandler in collectionChanged.GetInvocationList().OfType<NotifyCollectionChangedEventHandler>())
        {
            try
            {
                if (_notifyOnUi is not null)
                    _notifyOnUi(() => InvokeNotifyCollectionChanged(notifyEventHandler, e));
                else
                    InvokeNotifyCollectionChanged(notifyEventHandler, e);
            }
            catch (TaskCanceledException)
            {
                // Operation has canceled by the system
            }
        }
    }
}
