// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities.Providers
{
    public class PredicateItemsProvider<T> : IItemsProvider<T>
    {
        private readonly Func<T, bool> _predicate;
        private readonly IItemsProvider<T> _itemsProvider;

        public PredicateItemsProvider(IEnumerable<T> items, Func<T, bool> predicate)
            : this(new ItemsProvider<T>(items), predicate) { }

        public PredicateItemsProvider(IItemsProvider<T> provider, Func<T, bool> predicate)
        {
            _itemsProvider = provider;
            _predicate = predicate;
        }

        public IEnumerable<T> ProvideItems() => _itemsProvider.ProvideItems().Where(_predicate);
    }
}
