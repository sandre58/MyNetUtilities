// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using MyNet.Utilities.Generator.Extensions.Resources;
using MyNet.Utilities.Geography;
using MyNet.Utilities.Localization;

namespace MyNet.Utilities.Generator.Extensions
{
    public static class AddressGenerator
    {
        static AddressGenerator() => ResourceLocator.Initialize();

        public static Coordinates Coordinates()
        {
            var latitude = Math.Round(RandomGenerator.Double(-85, 85), 4);
            var longitude = Math.Round(RandomGenerator.Double(-180, 180), 4);
            return new Coordinates(latitude, longitude);
        }

        public static string City(CultureInfo? culture = null)
            => GetTranslationService(culture).Translate(nameof(AddressResources.Cities))?.Random() ?? string.Empty;

        public static string StreetName(CultureInfo? culture = null)
            => string.Join(" ", NameGenerator.FirstName(RandomGenerator.Enum<GenderType>(), culture: culture), NameGenerator.LastName(culture));

        public static string StreetPrefix(CultureInfo? culture = null)
            => GetTranslationService(culture).Translate(nameof(AddressResources.StreetPrefixes))?.Random() ?? string.Empty;

        public static string Street(CultureInfo? culture = null)
            => GetTranslationService(culture).Translate(nameof(AddressResources.StreetFormat))?.FormatWith(RandomGenerator.Int(1, 200), StreetPrefix(culture), StreetName(culture)) ?? string.Empty;

        public static string PostalCode() => string.Join(string.Empty, Enumerable.Range(0, 5).Select(_ => RandomGenerator.Int(0, 9)));

        private static TranslationService GetTranslationService(CultureInfo? culture = null) => TranslationService.Get(culture ?? CultureInfo.CurrentCulture);
    }
}
