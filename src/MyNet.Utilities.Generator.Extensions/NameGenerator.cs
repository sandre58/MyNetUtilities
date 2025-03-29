// -----------------------------------------------------------------------
// <copyright file="NameGenerator.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using MyNet.Utilities.Generator.Extensions.Resources;

namespace MyNet.Utilities.Generator.Extensions;

public static class NameGenerator
{
    private static readonly Dictionary<(GenderType, NameFormats), Func<CultureInfo?, string[]>> FormatMap = new()
    {
        { (GenderType.Male, NameFormats.Standard), x => [FirstName(culture: x), LastName(x)] },
        { (GenderType.Male, NameFormats.Inverse), x => [LastName(x), FirstName(culture: x)] },
        { (GenderType.Male, NameFormats.WithPrefix), x => [Prefix(x), FirstName(culture: x), LastName(x)] },
        { (GenderType.Male, NameFormats.InverseWithPrefix), x => [Prefix(x), LastName(x), FirstName(culture: x)] },
        { (GenderType.Male, NameFormats.WithSuffix), x => [FirstName(culture: x), LastName(x), Suffix(x)] },
        { (GenderType.Male, NameFormats.InverseWithSuffix), x => [LastName(x), FirstName(culture: x), Suffix(x)] },
        { (GenderType.Female, NameFormats.Standard), x => [FirstName(GenderType.Female, culture: x), LastName(x)] },
        { (GenderType.Female, NameFormats.Inverse), x => [LastName(x), FirstName(GenderType.Female, culture: x)] },
        { (GenderType.Female, NameFormats.WithPrefix), x => [Prefix(x), FirstName(GenderType.Female, culture: x), LastName(x)] },
        { (GenderType.Female, NameFormats.InverseWithPrefix), x => [Prefix(x), LastName(x), FirstName(GenderType.Female, culture: x)] },
        { (GenderType.Female, NameFormats.WithSuffix), x => [FirstName(GenderType.Female, culture: x), LastName(x), Suffix(x)] },
        { (GenderType.Female, NameFormats.InverseWithSuffix), x => [LastName(x), FirstName(GenderType.Female, culture: x), Suffix(x)] }
    };

    static NameGenerator() => ResourceLocator.Initialize();

    public static string FullName(GenderType genderType = GenderType.Male, NameFormats format = NameFormats.Standard, CultureInfo? culture = null)
        => string.Join(" ", FormatMap[(genderType, format)].Invoke(culture));

    public static string FirstName(GenderType genderType = GenderType.Male, CultureInfo? culture = null)
        => (genderType == GenderType.Male ? NamesResources.MaleFirstNames : NamesResources.FemaleFirstNames).Translate(culture).Random();

    public static string LastName(CultureInfo? culture = null)
        => nameof(NamesResources.LastNames).Translate(culture).Random();

    public static string Prefix(CultureInfo? culture = null) => nameof(NamesResources.Suffixes).Translate(culture).Random();

    public static string Suffix(CultureInfo? culture = null) => nameof(NamesResources.Prefixes).Translate(culture).Random();
}
