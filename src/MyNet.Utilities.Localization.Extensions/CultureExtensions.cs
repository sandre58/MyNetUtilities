// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace MyNet.Utilities.Localization.Extensions
{
    public static class CultureExtensions
    {
        public static byte[]? GetImage(this CultureInfo culture)
        {
            if (string.IsNullOrEmpty(culture.Name)) return [];

            var usedCulture = culture.IsNeutralCulture ? CultureInfo.CreateSpecificCulture(culture.Name) : culture;
            var name = usedCulture.Name.Replace("-", "_");
            CultureResources.ResourceManager.IgnoreCase = true;
            var obj = (byte[]?)CultureResources.ResourceManager.GetObject(name, CultureInfo.InvariantCulture);
            if (obj != null || usedCulture.Parent == null || culture.IsNeutralCulture) return obj;

            obj = usedCulture.Parent.GetImage();

            return obj ?? [];
        }
    }
}
