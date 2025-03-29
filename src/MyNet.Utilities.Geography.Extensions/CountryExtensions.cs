// -----------------------------------------------------------------------
// <copyright file="CountryExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace MyNet.Utilities.Geography.Extensions;

public static class CountryExtensions
{
    public static byte[]? GetFlag(this Country country, FlagSize size = FlagSize.Pixel32)
        => (byte[]?)CountryResources.ResourceManager.GetObject($"{country.Alpha2}{(int)size}", CultureInfo.InvariantCulture);

    public static string GetDisplayName(this Country country)
        => CountryResources.ResourceManager.GetString(country.Name, CultureInfo.CurrentCulture).OrEmpty();
}
