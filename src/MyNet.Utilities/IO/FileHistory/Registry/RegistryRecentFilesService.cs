// -----------------------------------------------------------------------
// <copyright file="RegistryRecentFilesService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using MyNet.Utilities.IO.Registry;
using MyNet.Utilities.IO.Registry.FileManagement;

namespace MyNet.Utilities.IO.FileHistory.Registry;

internal sealed class RegistryRecentFilesService(IRegistryService registryService, RegistryFileServiceParameter parameter) : RegistryFileService<RegistryRecentFile, RegistryFileServiceParameter>(registryService, parameter)
{
    public override RegistryRecentFile? AddFile(RegistryRecentFile fileInfo, string key)
    {
        var result = base.AddFile(fileInfo, key);

        if (result != null)
            RemoveOldFiles(Path.GetExtension(result.Path).Split('.')[1]);

        return result;
    }

    private void RemoveOldFiles(string type)
    {
        if (Parameters.SavedMaxCount == 0) return;

        var filesWithDate = RegistryService.GetAll<RegistryRecentFile>(GetRegistryPath(type));
        var list = filesWithDate.ToList();
        var recentFileNumber = list.Count(x => x.Item is { IsPinned: false, IsRecoveredFile: false });
        var itemToRemoveNumber = recentFileNumber - Parameters.SavedMaxCount;

        if (itemToRemoveNumber <= 0)
            return;

        var oldItems = list.Where(x => x.Item is { IsPinned: false, IsRecoveredFile: false }).OrderBy(x => DateTime.FromBinary(x.Item.LastAccessDate)).Take(itemToRemoveNumber);

        foreach (var item in oldItems)
            RegistryService.Remove(item.Parent, item.Key);
    }
}
