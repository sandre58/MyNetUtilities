// -----------------------------------------------------------------------
// <copyright file="RegistryFilesProvider.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNet.Utilities.IO.Registry.FileManagement;

public class RegistryFilesProvider<TFileService, T> : IRegistryFilesProvider<T>
    where TFileService : IRegistryFileService<T>
    where T : RegistryFile, new()
{
    private readonly TFileService _fileService;

    protected RegistryFilesProvider(TFileService service, string fileType)
    {
        _fileService = service;
        Type = fileType;
    }

    public string Type { get; }

    public virtual async Task<IEnumerable<T>> GetFilesAsync() => await _fileService.GetFilesAsync(Type).ConfigureAwait(false);

    public IEnumerable<T> GetFiles() => _fileService.GetFiles(Type);
}
