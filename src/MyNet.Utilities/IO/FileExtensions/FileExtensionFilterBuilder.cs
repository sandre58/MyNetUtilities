// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities.IO.FileExtensions
{
    public class FileExtensionFilterBuilder
    {
        private readonly List<FileExtensionInfo> _extensions = [];

        public FileExtensionFilterBuilder() { }

        public FileExtensionFilterBuilder(IEnumerable<FileExtensionInfo> fileExtensionInfos) => AddRange(fileExtensionInfos);

        public FileExtensionFilterBuilder AddRange(IEnumerable<FileExtensionInfo> extensions)
        {
            foreach (var extension in extensions)
                _ = Add(extension);

            return this;
        }

        public FileExtensionFilterBuilder Add(FileExtensionInfo extension)
        {
            _extensions.Add(extension);
            return this;
        }

        public FileExtensionFilterBuilder AddMerge(string key, bool firstPosition, params FileExtensionInfo[] newExtensions)
        {
            var item = new FileExtensionInfo(key, newExtensions.SelectMany(x => x.Extensions).ToArray());

            if (!firstPosition)
                return Add(item);

            _extensions.Insert(0, item);

            return this;
        }

        public FileExtensionFilterBuilder AddContentMerge(string key, bool fistPosition = false)
        {
            var extensions = _extensions.ToArray();
            return AddMerge(key, fistPosition, extensions);
        }

        public IDictionary<string, string> GenerateFilters(Func<string, string?>? translateKey = null) => _extensions.GetFileFilters(translateKey);

        public int IndexOfExtension(string extensionName) =>
            _extensions.Select((x, y) => (index: y, item: x))
                             .FirstOrDefault(x => x.item.GetExtensionNames().Contains(extensionName.ToLowerInvariant()))
                             .index;
    }
}
