// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.IO.FileExtensions
{
    public static class FileExtensionInfoProvider
    {
        public static FileExtensionInfo AllFiles { get; } = new(nameof(AllFiles), new[] { ".*" });

        public static FileExtensionInfo Csv { get; } = new(BuildTitleKey(nameof(Csv)), new[] { ".csv" });

        public static FileExtensionInfo Excel { get; } = new(BuildTitleKey(nameof(Excel)), new[] { ".xlsx", ".xls" });

        public static FileExtensionInfo Png { get; } = new(BuildTitleKey(nameof(Png)), new[] { ".png" });

        public static FileExtensionInfo Jpg { get; } = new(BuildTitleKey(nameof(Jpg)), new[] { ".jpg" });

        public static FileExtensionInfo Jpeg { get; } = new(BuildTitleKey(nameof(Jpeg)), new[] { ".jpeg" });

        public static FileExtensionInfo Gif { get; } = new(BuildTitleKey(nameof(Gif)), new[] { ".gif" });

        public static FileExtensionInfo Tif { get; } = new(BuildTitleKey(nameof(Tif)), new[] { ".tif" });

        public static FileExtensionInfo Bmp { get; } = new(BuildTitleKey(nameof(Bmp)), new[] { ".bmp" });

        public static FileExtensionInfo Ico { get; } = new(BuildTitleKey(nameof(Ico)), new[] { ".ico" });

        public static FileExtensionInfo AllImages { get; } = new(nameof(AllImages), new[] { Jpg, Jpeg, Png, Gif, Tif, Bmp });

        private static string BuildTitleKey(string extension) => $"File{extension}";
    }
}
