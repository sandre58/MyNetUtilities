// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;

namespace MyNet.Utilities.IO.FileExtensions
{
    public class FileExtensionInfo
    {
        public string Key { get; set; }

        public string[] Extensions { get; set; }

        public FileExtensionInfo(string titleKey, string[] extensions) => (Key, Extensions) = (titleKey, extensions);

        public FileExtensionInfo(string titleKey, FileExtensionInfo[] extensions) => (Key, Extensions) = (titleKey, extensions.SelectMany(x => x.Extensions).ToArray());

        public string GetDefaultExtension() => Extensions.FirstOrDefault() ?? string.Empty;
    }
}
