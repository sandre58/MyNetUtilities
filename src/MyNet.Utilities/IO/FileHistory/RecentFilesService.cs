// -----------------------------------------------------------------------
// <copyright file="RecentFilesService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Caching;

namespace MyNet.Utilities.IO.FileHistory;

public class RecentFilesService(IRecentFileRepository recentFileRepository)
{
    private static readonly CacheStorage<string, RecentFile> Cache = new();

    public RecentFile? GetLastRecentFile() => GetAll().Where(x => x is { LastAccessDate: not null, IsRecoveredFile: false }).OrderBy(x => x.LastAccessDate).LastOrDefault();

    public IList<RecentFile> GetAll()
    {
        if (Cache.Keys.Any()) return [.. Cache.Keys.Select(x => Cache.Get(x)!)];

        var recentFiles = recentFileRepository.GetAll().ToList();
        recentFiles.ForEach(x => Cache.Add(x.Path, x));

        return [.. Cache.Keys.Select(x => Cache.Get(x)!)];
    }

    public RecentFile? Add(string name, string path)
    {
        var recentFile = new RecentFile(name, path, null, null, false);

        var newRecentFile = recentFileRepository.Add(recentFile);

        if (newRecentFile is not null)
            Cache.Add(path, newRecentFile, true);

        return newRecentFile;
    }

    public void Remove(string path)
    {
        _ = recentFileRepository.Remove(path);
        Cache.Clear();
    }

    public RecentFile? Update(string path, bool isPinned)
    {
        var recentFile = GetByPath(path) ?? throw new ArgumentNullException(nameof(path));
        recentFile.IsPinned = isPinned;
        var newRecentFile = recentFileRepository.Update(recentFile);

        if (newRecentFile is not null)
            Cache.Add(path, newRecentFile, true);

        return newRecentFile;
    }

    private RecentFile? GetByPath(string path) => GetAll().FirstOrDefault(x => x.Path == path);
}
