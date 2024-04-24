// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.IO.FileExtensions
{
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
}
