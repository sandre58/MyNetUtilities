// -----------------------------------------------------------------------
// <copyright file="FileExtensionInfo.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace MyNet.Utilities.IO.FileExtensions;

public class FileExtensionInfo
{
    public FileExtensionInfo(string titleKey, string[] extensions) => (Key, Extensions) = (titleKey, extensions);

    public FileExtensionInfo(string titleKey, FileExtensionInfo[] extensions) => (Key, Extensions) = (titleKey, extensions.SelectMany(x => x.Extensions).ToArray());

    public string Key { get; set; }

    public string[] Extensions { get; set; }

    public string GetDefaultExtension() => Extensions.FirstOrDefault() ?? string.Empty;
}
