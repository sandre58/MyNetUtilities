// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using MyNet.Utilities.Extensions;
using MyNet.Utilities.Generator.Extensions.Resources;

namespace MyNet.Utilities.Generator.Extensions
{
    public static class NameGenerator
    {
        static NameGenerator() => ResourceLocator.Initialize();

        private static readonly Dictionary<(GenderType, NameFormats), Func<CultureInfo?, string[]>> FormatMap = new()
        {
            { (GenderType.Male, NameFormats.Standard), x => new [] { FirstName(culture: x), LastName(x) } },
            { (GenderType.Male, NameFormats.Inverse), x => new[] { LastName(x), FirstName(culture: x) } },
            { (GenderType.Male, NameFormats.WithPrefix), x => new[] { Prefix(x), FirstName(culture: x), LastName(x) } },
            { (GenderType.Male, NameFormats.InverseWithPrefix), x => new[] { Prefix(x), LastName(x), FirstName(culture: x) } },
            { (GenderType.Male, NameFormats.WithSuffix), x => new[] { FirstName(culture: x), LastName(x), Suffix(x) } },
            { (GenderType.Male, NameFormats.InverseWithSuffix), x => new[] { LastName(x), FirstName(culture: x), Suffix(x) } },
            { (GenderType.Female, NameFormats.Standard), x => new[] { FirstName(GenderType.Female, culture: x), LastName(x) } },
            { (GenderType.Female, NameFormats.Inverse), x => new[] { LastName(x), FirstName(GenderType.Female, culture: x) } },
            { (GenderType.Female, NameFormats.WithPrefix), x => new[] { Prefix(x), FirstName(GenderType.Female, culture: x), LastName(x) } },
            { (GenderType.Female, NameFormats.InverseWithPrefix), x => new[] { Prefix(x), LastName(x), FirstName(GenderType.Female, culture: x) } },
            { (GenderType.Female, NameFormats.WithSuffix), x => new[] { FirstName(GenderType.Female, culture: x), LastName(x), Suffix(x) } },
            { (GenderType.Female, NameFormats.InverseWithSuffix), x => new[] { LastName(x), FirstName(GenderType.Female, culture: x), Suffix(x) } },
        };

        public static string FullName(GenderType genderType = GenderType.Male, NameFormats format = NameFormats.Standard, CultureInfo? culture = null)
            => string.Join(" ", FormatMap[(genderType, format)].Invoke(culture));

        public static string FirstName(GenderType genderType = GenderType.Male, CultureInfo? culture = null)
            => (genderType == GenderType.Male ? NamesResources.MaleFirstNames : NamesResources.FemaleFirstNames).Translate(culture).Random();

        public static string LastName(CultureInfo? culture = null)
            => nameof(NamesResources.LastNames).Translate(culture).Random();

        public static string Prefix(CultureInfo? culture = null) => nameof(NamesResources.Suffixes).Translate(culture).Random();

        public static string Suffix(CultureInfo? culture = null) => nameof(NamesResources.Prefixes).Translate(culture).Random();
    }
}
