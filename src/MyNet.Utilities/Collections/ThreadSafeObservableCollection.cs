// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace MyNet.Utilities.Collections
{
    public class ThreadSafeObservableCollection<T> : ExtendedObservableCollection<T>
    {
        private readonly object _localLock = new();
        private readonly Action<Action>? _notifyOnUi;

        public ThreadSafeObservableCollection(Action<Action>? notifyOnUi = null) => _notifyOnUi = notifyOnUi;

        public ThreadSafeObservableCollection(List<T> list, Action<Action>? notifyOnUi = null) : base(list) => _notifyOnUi = notifyOnUi;

        public ThreadSafeObservableCollection(IEnumerable<T> collection, Action<Action>? notifyOnUi = null) : base(collection) => _notifyOnUi = notifyOnUi;

        public override event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected override void InsertItem(int index, T item) => ExecuteThreadSafe(() => base.InsertItem(index, item));

        protected override void MoveItem(int oldIndex, int newIndex) => ExecuteThreadSafe(() => base.MoveItem(oldIndex, newIndex));

        protected override void RemoveItem(int index) => ExecuteThreadSafe(() => base.RemoveItem(index));

        protected override void SetItem(int index, T item) => ExecuteThreadSafe(() => base.SetItem(index, item));

        protected override void ClearItems() => ExecuteThreadSafe(base.ClearItems);

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            using (BlockReentrancy())
            {
                var collectionChanged = CollectionChanged;
                if (collectionChanged != null)
                    NotifyCollectionChanged(e, collectionChanged);
            }
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
                    // Opeation has canceled by the system
                }
            }
        }

        protected virtual void InvokeNotifyCollectionChanged(NotifyCollectionChangedEventHandler notifyEventHandler, NotifyCollectionChangedEventArgs e) => notifyEventHandler.Invoke(this, e);

        protected void ExecuteThreadSafe(Action action)
        {
            lock (_localLock)
            {
                action();
            }
        }
    }

}
