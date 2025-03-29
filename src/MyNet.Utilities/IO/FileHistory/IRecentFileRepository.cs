// -----------------------------------------------------------------------
// <copyright file="IRecentFileRepository.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace MyNet.Utilities.IO.FileHistory;

public interface IRecentFileRepository
{
    IEnumerable<RecentFile> GetAll();

    RecentFile? Add(RecentFile file);

    bool Remove(string filePath);

    RecentFile? Update(RecentFile recentFile);
}
