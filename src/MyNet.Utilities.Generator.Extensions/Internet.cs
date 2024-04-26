// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using MyNet.Utilities.Generator.Extensions.Resources;
using MyNet.Utilities.Localization;

namespace MyNet.Utilities.Generator.Extensions
{
    public static partial class InternetGenerator
    {
        static InternetGenerator() => ResourceLocator.Initialize();

        private static readonly IEnumerable<Func<CultureInfo?, string>> UserNameFormats =
        [
            x => UserName(NameGenerator.LastName(culture: x)).ToLowerInvariant(),
            x => string.Format("{0}.{1}", UserName(NameGenerator.FirstName(culture: x)), UserName(NameGenerator.LastName(x))).ToLowerInvariant()
        ];

        public static string Email(CultureInfo? culture = null) => string.Format("{0}@{1}", UserName(culture), DomainName(culture));

        public static string Email(string name, CultureInfo? culture = null) => string.Format("{0}@{1}", UserName(name), DomainName(culture));

        public static string FreeEmail(CultureInfo? culture = null) => string.Format("{0}@{1}", UserName(culture), GetTranslationService(culture).Translate(nameof(InternetResources.FreeMails))?.Random());

        public static string UserName(CultureInfo? culture = null) => RandomGenerator.ListItem(UserNameFormats.ToList()).Invoke(culture);

        public static string UserName(string name) => UsernameRegex().Replace(name, match => match.Groups[1].Value.ToUpper()).ToLowerInvariant();

        public static string DomainName(CultureInfo? culture = null) => string.Format("{0}.{1}", UserName(culture), DomainSuffix(culture));

        public static string DomainSuffix(CultureInfo? culture = null) => GetTranslationService(culture).Translate(nameof(InternetResources.DomainSuffixes))?.Random() ?? string.Empty;

        public static string IPv4Address()
        {
            var random = new Random();
            var min = 2;
            var max = 255;
            var parts = new string[] {
                random.Next(min, max).ToString(),
                random.Next(min, max).ToString(),
                random.Next(min, max).ToString(),
                random.Next(min, max).ToString(),
            };
            return string.Join(".", parts);
        }

        public static string IPv6Address()
        {
            var random = new Random();
            var min = 0;
            var max = 65536;
            var parts = new string[] {
                random.Next(min, max).ToString("x"),
                random.Next(min, max).ToString("x"),
                random.Next(min, max).ToString("x"),
                random.Next(min, max).ToString("x"),
                random.Next(min, max).ToString("x"),
                random.Next(min, max).ToString("x"),
                random.Next(min, max).ToString("x"),
                random.Next(min, max).ToString("x"),
            };
            return string.Join(":", parts);
        }

        public static string Url() => string.Format("http://{0}/{1}", DomainName(), UserName());

        private static TranslationService GetTranslationService(CultureInfo? culture = null) => TranslationService.Get(culture ?? CultureInfo.CurrentCulture);

        [GeneratedRegex(@"[^\w]+")]
        private static partial Regex UsernameRegex();
    }
}
