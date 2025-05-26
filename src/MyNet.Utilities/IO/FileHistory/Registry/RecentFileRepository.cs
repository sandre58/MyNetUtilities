// -----------------------------------------------------------------------
// <copyright file="RecentFileRepository.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyNet.Utilities.IO.Registry;
using MyNet.Utilities.IO.Registry.FileManagement;

namespace MyNet.Utilities.IO.FileHistory.Registry;

public class RecentFileRepository : IRecentFileRepository
{
    private readonly RegistryRecentFilesService _fileService;
    private readonly ICollection<string> _extensions;

    public RecentFileRepository(IRegistryService registryService, string baseRegistryPath, ICollection<string> extensions, int maxRecentFiles = 0)
    {
        _extensions = extensions;
        _fileService = new RegistryRecentFilesService(registryService, new RegistryFileServiceParameter(baseRegistryPath)
        {
            SavedMaxCount = maxRecentFiles,
            SupportedTypes = [.. _extensions]
        });
    }

    public IEnumerable<RecentFile> GetAll() => from extension in _extensions from registryRecentFile in _fileService.GetFiles(extension) select CreateRecentFile(registryRecentFile);

    public RecentFile? Add(RecentFile file)
    {
        var registryRecentFile = CreateRegistryRecentFile(file);
        var result = _fileService.AddFile(registryRecentFile, Guid.NewGuid().ToString());

        return result is not null ? CreateRecentFile(result) : null;
    }

    public bool Remove(string filePath) => _fileService.RemoveFile(filePath);

    public RecentFile? Update(RecentFile recentFile)
    {
        var result = _fileService.UpdateFile(CreateRegistryRecentFile(recentFile));

        return result is not null ? CreateRecentFile(result) : null;
    }

    private static RegistryRecentFile CreateRegistryRecentFile(RecentFile source)
        => new()
        {
            Name = source.Name,
            IsPinned = source.IsPinned,
            IsRecoveredFile = source.IsRecoveredFile,
            LastAccessDate = source.LastAccessDate?.ToFileTimeUtc() ?? 0,
            Path = source.Path
        };

    private static RecentFile CreateRecentFile(RegistryRecentFile source)
    {
        var lastAccessDate = DateTime.FromBinary(source.LastAccessDate);
        var modificationDate = File.GetLastWriteTimeUtc(source.Path);
        return new RecentFile(source.Name.OrEmpty(),
                              source.Path.OrEmpty(),
                              lastAccessDate == DateTime.MinValue ? null : lastAccessDate,
                              modificationDate == DateTime.MinValue ? null : modificationDate,
                              source.IsPinned,
                              source.IsRecoveredFile);
    }
}
