// -----------------------------------------------------------------------
// <copyright file="LocalizationExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using MyNet.Utilities.Localization;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class LocalizationExtensions
{
    public const string AbbreviationSuffix = "Abbr";

    public static string ToAbbreviationKey(this string key) => $"{key}{AbbreviationSuffix}";

    public static string Translate(this string key, CultureInfo? cultureInfo = null) => TranslationService.GetOrCurrent(cultureInfo)[key];

    public static string Translate(this string key, string filename, CultureInfo? cultureInfo = null) => TranslationService.GetOrCurrent(cultureInfo)[key, filename];

    public static string Translate(this CultureInfo culture, string key) => TranslationService.Get(culture).Translate(key);

    public static string Translate(this CultureInfo culture, string key, string filename) => TranslationService.Get(culture).Translate(key, filename);

    public static string TranslateAbbreviated(this string key, CultureInfo? cultureInfo = null) => key.ToAbbreviationKey().Translate(cultureInfo);

    public static string TranslateAbbreviated(this string key, string filename, CultureInfo? cultureInfo = null) => key.ToAbbreviationKey().Translate(filename, cultureInfo);

    public static string TranslateAbbreviated(this CultureInfo culture, string key) => culture.Translate(key.ToAbbreviationKey());

    public static string TranslateAbbreviated(this CultureInfo culture, string key, string filename) => culture.Translate(key.ToAbbreviationKey(), filename);

    public static T? GetProvider<T>(this CultureInfo culture) => LocalizationService.Get<T>(culture);
}
