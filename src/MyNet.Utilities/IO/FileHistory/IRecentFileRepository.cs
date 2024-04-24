// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace MyNet.Utilities.IO.FileHistory
{
    public interface IRecentFileRepository
    {
        IEnumerable<RecentFile> GetAll();

        RecentFile? Add(RecentFile file);

        bool Remove(string filePath);

        RecentFile? Update(RecentFile recentFile);
    }
}
