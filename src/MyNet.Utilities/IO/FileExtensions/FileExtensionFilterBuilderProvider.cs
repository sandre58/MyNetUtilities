// -----------------------------------------------------------------------
// <copyright file="FileExtensionFilterBuilderProvider.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.IO.FileExtensions;

public static class FileExtensionFilterBuilderProvider
{
    public static FileExtensionFilterBuilder AllImages { get; } = new(
    [
        FileExtensionInfoProvider.Jpg,
        FileExtensionInfoProvider.Jpeg,
        FileExtensionInfoProvider.Png,
        FileExtensionInfoProvider.Gif,
        FileExtensionInfoProvider.Tif,
        FileExtensionInfoProvider.Bmp
    ]);
}
