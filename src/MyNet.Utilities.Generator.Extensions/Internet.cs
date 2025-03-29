// -----------------------------------------------------------------------
// <copyright file="Internet.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using MyNet.Utilities.Generator.Extensions.Resources;

namespace MyNet.Utilities.Generator.Extensions;

public static partial class InternetGenerator
{
    private static readonly IEnumerable<Func<CultureInfo?, string>> UserNameFormats =
    [
        x => UserName(NameGenerator.LastName(culture: x)).ToLower(CultureInfo.CurrentCulture),
        x => $"{UserName(NameGenerator.FirstName(culture: x))}.{UserName(NameGenerator.LastName(x))}".ToLower(
            CultureInfo.CurrentCulture)
    ];

    static InternetGenerator() => ResourceLocator.Initialize();

    public static string Email(CultureInfo? culture = null) => $"{UserName(culture)}@{DomainName(culture)}";

    public static string Email(string name, CultureInfo? culture = null) => $"{UserName(name)}@{DomainName(culture)}";

    public static string FreeEmail(CultureInfo? culture = null) =>
        $"{UserName(culture)}@{nameof(InternetResources.FreeMails).Translate(culture).Random()}";

    public static string UserName(CultureInfo? culture = null) =>
        RandomGenerator.ListItem(UserNameFormats.ToList()).Invoke(culture);

    public static string UserName(string name) => UsernameRegex()
        .Replace(name, match => match.Groups[1].Value.ToUpper(CultureInfo.CurrentCulture))
        .ToLower(CultureInfo.CurrentCulture);

    public static string DomainName(CultureInfo? culture = null) => $"{UserName(culture)}.{DomainSuffix(culture)}";

    public static string DomainSuffix(CultureInfo? culture = null) =>
        nameof(InternetResources.DomainSuffixes).Translate(culture).Random();

    public static string IPv4Address()
    {
        const int min = 2;
        const int max = 255;
        var parts = new[]
        {
            RandomGenerator.Int(min, max).ToString(CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString(CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString(CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString(CultureInfo.InvariantCulture)
        };
        return string.Join(".", parts);
    }

    public static string IPv6Address()
    {
        const int min = 0;
        const int max = 65536;
        var parts = new[]
        {
            RandomGenerator.Int(min, max).ToString("x", CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString("x", CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString("x", CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString("x", CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString("x", CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString("x", CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString("x", CultureInfo.InvariantCulture),
            RandomGenerator.Int(min, max).ToString("x", CultureInfo.InvariantCulture)
        };
        return string.Join(":", parts);
    }

    [SuppressMessage("Design", "CA1055:URI-like return values should not be strings", Justification = "Not used as URI.")]
    public static string Url() => $"http://{DomainName()}/{UserName()}";

    [GeneratedRegex(@"[^\w]+")]
    private static partial Regex UsernameRegex();
}
