// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Linq;
using MyNet.Utilities.IO.Registry;
using MyNet.Utilities.IO.Registry.FileManagement;

namespace MyNet.Utilities.IO.FileHistory.Registry
{
    internal class RegistryRecentFilesService(IRegistryService registryService, RegistryFileServiceParameter parameter) : RegistryFileService<RegistryRecentFile, RegistryFileServiceParameter>(registryService, parameter)
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
            var recentFileNumber = filesWithDate.Count(x => !x.Item.IsPinned && !x.Item.IsRecoveredFile);
            var itemToRemoveNumber = recentFileNumber - Parameters.SavedMaxCount;

            if (itemToRemoveNumber <= 0)
                return;

            var oldItems = filesWithDate.Where(x => !x.Item.IsPinned && !x.Item.IsRecoveredFile).OrderBy(x => DateTime.FromBinary(x.Item.LastAccessDate)).Take(itemToRemoveNumber);

            foreach (var item in oldItems)
                RegistryService.Remove(item.Parent, item.Key);
        }
    }
}
