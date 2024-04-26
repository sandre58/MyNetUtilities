// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace MyNet.Utilities.Geography.Extensions
{
    public static class CountryExtensions
    {
        public static byte[]? GetFlag(this Country country, FlagSize size = FlagSize._32)
            => (byte[]?)CountryResources.ResourceManager.GetObject($"{country.Alpha2}{(int)size}", CultureInfo.InvariantCulture);

        public static string GetDisplayName(this Country country)
            => CountryResources.ResourceManager.GetString(country.Name, CultureInfo.CurrentCulture).OrEmpty();
    }
}
