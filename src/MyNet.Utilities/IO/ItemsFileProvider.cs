// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using MyNet.Utilities.Providers;

namespace MyNet.Utilities.IO
{
    public abstract class ItemsFileProvider<T> : IItemsProvider<T>
    {
        public string? Filename { get; private set; }

        public IEnumerable<Exception> Exceptions { get; private set; } = [];

        protected ItemsFileProvider(string? filename = null) => SetFilename(filename.OrEmpty());

        public void SetFilename(string filename) => Filename = filename;

        public void Clear() => Exceptions = [];

        public IEnumerable<T> ProvideItems()
        {
            if (string.IsNullOrEmpty(Filename)) return [];

            if (!File.Exists(Filename))
                throw new FileNotFoundException(string.Empty, Filename);

            var (items, exceptions) = LoadItems(Filename);
            Exceptions = exceptions;

            return items;
        }

        protected abstract (IEnumerable<T> items, IEnumerable<Exception> exceptions) LoadItems(string filename);
    }
}
