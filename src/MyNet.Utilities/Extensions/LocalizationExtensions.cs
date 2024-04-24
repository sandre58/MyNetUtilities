// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using MyNet.Utilities.Localization;

namespace MyNet.Utilities.Extensions
{
    public static class LocalizationExtensions
    {
        public static string? TranslateDatePattern(this string key)
        {
            var format = CultureInfo.CurrentCulture.DateTimeFormat;
            var prop = format.GetType().GetProperty(key);
            return prop != null ? prop.GetValue(format)?.ToString() ?? string.Empty : TranslationService.Current[key];
        }

        public static string? Translate(this string key, bool abbreviation = false) => TranslationService.Current[abbreviation ? key.GetAbbreviatedResourceKey() : key];

        public static string? Translate(this string key, string? filename, bool abbreviation = false) => TranslationService.Current[abbreviation ? key.GetAbbreviatedResourceKey() : key, filename];

        public static string GetAbbreviatedResourceKey(this string key) => key + "Abbr";
    }
}
