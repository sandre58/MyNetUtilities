// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using MyNet.Utilities.Localization;

namespace MyNet.Utilities.Extensions
{
    public static class CultureExtensions
    {
        public static string? TranslateDatePattern(this CultureInfo culture, string? key)
        {
            var format = culture.DateTimeFormat;
            var prop = format.GetType().GetProperty(key ?? string.Empty);
            return prop != null ? prop.GetValue(format)?.ToString() ?? string.Empty : TranslationService.Get(culture).Translate(key);
        }

        public static string? TranslateDatePattern(this CultureInfo culture, string? key, string filename)
        {
            var format = culture.DateTimeFormat;
            var prop = format.GetType().GetProperty(key ?? string.Empty);
            return prop != null ? prop.GetValue(format)?.ToString() ?? string.Empty : TranslationService.Get(culture).Translate(key, filename);
        }

        public static string? Translate(this CultureInfo culture, string? key) => TranslationService.Get(culture).Translate(key);

        public static string? Translate(this CultureInfo culture, string? key, string filename) => TranslationService.Get(culture).Translate(key, filename);

        public static T? GetProvider<T>(this CultureInfo culture) => TranslationService.Get(culture).GetProvider<T>();
    }
}
