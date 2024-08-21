// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using MyNet.Utilities.Globalization;
using MyNet.Utilities.Localization;

namespace MyNet.Utilities.Extensions
{
    public static class LocalizationExtensions
    {
        public static string Translate(this string key, CultureInfo? cultureInfo = null) => (cultureInfo is not null ? TranslationService.Get(cultureInfo) : TranslationService.Current)[key];

        public static string Translate(this string key, string filename, CultureInfo? cultureInfo = null) => (cultureInfo is not null ? TranslationService.Get(cultureInfo) : TranslationService.Current)[key, filename];

        public static string? Translate(this CultureInfo culture, string key) => TranslationService.Get(culture).Translate(key);

        public static string? Translate(this CultureInfo culture, string key, string filename) => TranslationService.Get(culture).Translate(key, filename);

        public static T? GetProvider<T>(this CultureInfo culture) => LocalizationService.GetProvider<T>(culture);
    }
}
