// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyNet.Utilities.Extensions;

namespace MyNet.Utilities.IO.FileExtensions
{
    public static class FileExtensionInfoExtensions
    {
        public static (string Title, string Extensions) GetFileFilter(this FileExtensionInfo fileTypeExtension, Func<string, string?>? translateKey = null)
        {
            var extensionFilters = fileTypeExtension.Extensions.Select(x => $"*{x}").Aggregate((x, y) => $"{x};{y}");
            var title = $"{translateKey?.Invoke(fileTypeExtension.Key) ?? fileTypeExtension.Key.Translate()}";

            if (!string.IsNullOrEmpty(extensionFilters) && extensionFilters != "*.*")
                title += $" ({extensionFilters})";

            return (title, extensionFilters);
        }

        public static string? GetFileFilters(this FileExtensionInfo fileTypeExtension, Func<string, string?>? translateKey = null)
            => GetFileFilters([fileTypeExtension], translateKey);

        public static string? GetFileFilters(this IEnumerable<FileExtensionInfo> fileTypeExtensions, Func<string, string?>? translateKey = null)
            => string.Join("|", fileTypeExtensions.SelectMany(x =>
            {
                var fileFilter = x.GetFileFilter(translateKey);
                return new[] { fileFilter.Title, fileFilter.Extensions };
            }).ToList());

        public static IEnumerable<string> GetExtensionNames(this FileExtensionInfo fileTypeExtension) => fileTypeExtension.Extensions.Select(x => x.ToLowerInvariant());

        public static IEnumerable<string> FilterFiles(this FileExtensionInfo fileTypeExtension, IEnumerable<string> fileNames)
        {
            var extensions = fileTypeExtension.GetExtensionNames().Select(s => (name: s, isPartial: s.EndsWith(".*")));

            return fileNames.Where(file => extensions.Any(ext => ext.isPartial
                        ? Path.GetFileNameWithoutExtension(ext.name).Equals(Path.GetExtension(Path.GetFileNameWithoutExtension(file)), StringComparison.InvariantCultureIgnoreCase)
                        : ext.name.Equals(Path.GetExtension(file), StringComparison.InvariantCultureIgnoreCase)));
        }

        public static bool IsValid(this FileExtensionInfo fileExtensionInfo, string filename) => fileExtensionInfo.GetExtensionNames().Contains(Path.GetExtension(filename).ToLowerInvariant());

        public static FileExtensionInfo Concat(this FileExtensionInfo fileExtensionInfo, FileExtensionInfo other, string? title = null)
            => new(title ?? fileExtensionInfo.Key, fileExtensionInfo.Extensions.Concat(other.Extensions).ToArray());
    }
}
