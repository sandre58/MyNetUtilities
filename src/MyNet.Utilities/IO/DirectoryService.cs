// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using MyNet.Utilities.Helpers;
using MyNet.Utilities.Logging;

namespace MyNet.Utilities.IO
{
    public class DirectoryService : IDirectoryService
    {
        private string _rootDirectory = null!;

        private readonly List<string> _listOfDirectories = [];

        public string RootDirectory
        {
            get => _rootDirectory;
            set
            {
                var newRoot = GetOrMakeRootDirectory(value);

                if (newRoot != _rootDirectory)
                    _rootDirectory = newRoot;
            }
        }

        public DirectoryService(string root) => RootDirectory = root;

        public string CreateSubDirectory(string name)
        {
            var fullpath = Path.Combine(RootDirectory, name);
            _ = Directory.CreateDirectory(fullpath);
            return fullpath;
        }

        public string CreateFile(string? fileExtension = null, string? preferredFileName = null)
        {
            string fileName;
            var attempt = 0;
            const int maxAttempts = 10;
            var defaultFileName = preferredFileName;
            while (true)
            {
                fileName = GetFileName(fileExtension, defaultFileName);

                try
                {
                    using (new FileStream(fileName, FileMode.CreateNew))
                    {
                        // Empty block is intended: we only want to create an empty file
                    }
                    break;
                }
                catch (IOException ex)
                {
                    defaultFileName = string.Empty;
                    if (++attempt == maxAttempts)
                        throw new IOException("No unique temporary file name is available.", ex);
                }
            }

            return fileName;
        }

        public string GetFileName(string? fileExtension = null, string? preferredFileName = null)
        {
            var extension = fileExtension;
            if (string.IsNullOrEmpty(extension))
            {
                extension = string.IsNullOrEmpty(preferredFileName) ?
                    ".tmp" : Path.GetExtension(preferredFileName);
            }

            if (extension?.StartsWith("*", StringComparison.OrdinalIgnoreCase) ?? false)
                extension = extension.Substring(1);

            var fileName = string.IsNullOrEmpty(preferredFileName) ?
                Path.GetRandomFileName() : preferredFileName;

            fileName = Path.ChangeExtension(fileName, extension);
            fileName = Path.Combine(RootDirectory, fileName);

            return fileName;
        }

        private string GetOrMakeRootDirectory(string? root)
        {
            var tempPath = string.IsNullOrEmpty(root) ?
                    Path.GetTempPath()
                    : Environment.ExpandEnvironmentVariables(root);

            FileHelper.EnsureDirectoryExists(tempPath);

            if (!_listOfDirectories.Contains(tempPath))
                _listOfDirectories.Add(tempPath);

            return tempPath;
        }

        public void Delete() => TryDelete(new DirectoryInfo(RootDirectory));

        public void Clean()
        {
            try
            {
                var baseInfo = new DirectoryInfo(RootDirectory);
                foreach (var finfo in baseInfo.GetFiles())
                    TryDelete(finfo);
                foreach (var dinfo in baseInfo.GetDirectories())
                {
                    TryDelete(dinfo);
                }
            }
            catch (Exception ex)
            {
                LogManager.Warning($"an error occurred while attempting to clean temporary data directories: {ex.Message}");
            }
        }

        protected static void TryDelete(DirectoryInfo info)
        {
            try
            {
                info.Delete(true);
            }
            catch (Exception ex)
            {
                LogManager.Warning($"An error occurred while attempting to delete a directory: {ex.Message}");
            }
        }

        private static void TryDelete(FileInfo info)
        {
            try
            {
                info.Delete();
            }
            catch (Exception ex)
            {
                LogManager.Warning($"An error occurred while attempting to delete a file: {ex.Message}");
            }
        }

    }
}
