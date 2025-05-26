// -----------------------------------------------------------------------
// <copyright file="IRegistryFileService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNet.Utilities.IO.Registry.FileManagement;

public interface IRegistryFileService<T>
    where T : RegistryFile, new()
{
    Task<T?> AddFileAsync(T fileInfo, string key);

    T? AddFile(T fileInfo, string key);

    Task<T?> UpdateFileAsync(T fileInfo);

    Task<bool> RemoveFileAsync(string fileName);

    IEnumerable<T> GetFiles(string extension);

    Task<IEnumerable<T>> GetFilesAsync(string extension);
}
