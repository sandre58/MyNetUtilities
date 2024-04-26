// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyNet.Utilities.IO.Registry.FileManagement
{
    public class RegistryFileService<T, TParameter>(IRegistryService registryService, TParameter parameters) : IRegistryFileService<T>
        where T : RegistryFile, new()
            where TParameter : IRegistryFileServiceParameters
    {
        protected TParameter Parameters { get; } = parameters;

        protected IRegistryService RegistryService { get; } = registryService;

        public string GetRegistryPath(string extension) => $@"{Parameters.BaseRegistry}\{extension}";

        public Task<IEnumerable<T>> GetFilesAsync(string extension) => Task.Run(() => GetFiles(extension));

        public IEnumerable<T> GetFiles(string extension)
        {
            var files = RegistryService.GetAll<T>(GetRegistryPath(extension));

            return files.Select(x => x.Item);
        }

        public Task<T?> AddFileAsync(T fileInfo, string key) => Task.Run(() => AddFile(fileInfo, key));

        public virtual T? AddFile(T fileInfo, string key)
        {
            var type = Path.GetExtension(fileInfo.Path)[1..];

            if (!Parameters.SupportedTypes.Contains(type))
                return default;

            // Check if the file alreay exists in the registry to update it instead of create a new one
            var existingRegistry = RegistryService.GetAll<T>(GetRegistryPath(type)).FirstOrDefault(x => x.Item.Path == fileInfo.Path);
            if (existingRegistry != null)
            {
                existingRegistry.Item.LastAccessDate = DateTime.UtcNow.ToBinary();
                RegistryService.AddOrUpdate(existingRegistry);
                return existingRegistry.Item;
            }

            fileInfo.LastAccessDate = DateTime.UtcNow.ToBinary();
            RegistryService.AddOrUpdate(new Registry<T>(key, GetRegistryPath(type), fileInfo));

            return fileInfo;
        }

        public Task<T?> UpdateFileAsync(T fileInfo) => Task.Run(() => UpdateFile(fileInfo));

        public T? UpdateFile(T fileInfo)
        {
            var type = Path.GetExtension(fileInfo.Path)[1..];

            var existingRegistry = RegistryService.GetAll<T>(GetRegistryPath(type)).FirstOrDefault(x => x.Item.Path == fileInfo.Path);

            if (existingRegistry != null)
            {
                fileInfo.LastAccessDate = existingRegistry.Item.LastAccessDate;
                RegistryService.AddOrUpdate(new Registry<T>(existingRegistry.Key, GetRegistryPath(type), fileInfo));

                return fileInfo;
            }

            return null;
        }

        public virtual Task<bool> RemoveFileAsync(string fileName) => Task.Run(() => RemoveFile(fileName));

        public virtual bool RemoveFile(string filename)
        {
            var fileInfo = new FileInfo(filename);
            var type = fileInfo.Extension[1..];

            var registryPath = GetRegistryPath(type);

            var registryEntry = RegistryService.SearchKeyByValue(registryPath, nameof(RegistryFile.Path), filename);

            if (registryEntry == null)
                return false;

            RegistryService.Remove(registryPath, registryEntry);

            return true;
        }
    }
}
