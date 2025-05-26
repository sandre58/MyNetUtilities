// -----------------------------------------------------------------------
// <copyright file="IRegistryFilesProvider.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNet.Utilities.IO.Registry.FileManagement;

public interface IRegistryFilesProvider<T>
{
    string Type { get; }

    Task<IEnumerable<T>> GetFilesAsync();

    IEnumerable<T> GetFiles();
}
