// -----------------------------------------------------------------------
// <copyright file="ItemsFileProvider.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using MyNet.Utilities.Providers;

namespace MyNet.Utilities.IO;

public abstract class ItemsFileProvider<T> : IItemsProvider<T>
{
    protected ItemsFileProvider(string? filename = null) => SetFilename(filename.OrEmpty());

    public string? Filename { get; private set; }

    public IEnumerable<Exception> Exceptions { get; private set; } = [];

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

    protected abstract (IEnumerable<T> Items, IEnumerable<Exception> Exceptions) LoadItems(string filename);
}
