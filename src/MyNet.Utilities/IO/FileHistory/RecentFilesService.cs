// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Caching;

namespace MyNet.Utilities.IO.FileHistory
{
    public class RecentFilesService(IRecentFileRepository recentFileRepository)
    {
        private readonly IRecentFileRepository _recentFileRepository = recentFileRepository;
        private static readonly CacheStorage<string, RecentFile> Cache = new();

        private RecentFile? GetByPath(string path) => GetAll().FirstOrDefault(x => x.Path == path);

        public RecentFile? GetLastRecentFile() => GetAll().Where(x => x.LastAccessDate.HasValue && !x.IsRecoveredFile).OrderBy(x => x.LastAccessDate).LastOrDefault();

        public IList<RecentFile> GetAll()
        {
            if (!Cache.Keys.Any())
            {
                var recentFiles = _recentFileRepository.GetAll().ToList();
                recentFiles.ForEach(x => Cache.Add(x.Path, x));
            }

            return Cache.Keys.Select(x => Cache.Get(x)!).ToList();
        }

        public RecentFile? Add(string name, string path)
        {
            var recentFile = new RecentFile(name, path, null, null, false);

            var newRecentFile = _recentFileRepository.Add(recentFile);

            if (newRecentFile is not null)
                Cache.Add(path, newRecentFile, true);

            return newRecentFile;
        }

        public void Remove(string path)
        {
            _recentFileRepository.Remove(path);
            Cache.Clear();
        }

        public RecentFile? Update(string path, bool isPinned)
        {
            var recentFile = GetByPath(path) ?? throw new ArgumentNullException(nameof(path));
            recentFile.IsPinned = isPinned;
            var newRecentFile = _recentFileRepository.Update(recentFile);

            if (newRecentFile is not null)
                Cache.Add(path, newRecentFile, true);

            return newRecentFile;
        }
    }
}
