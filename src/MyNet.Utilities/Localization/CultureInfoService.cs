// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using MyNet.Utilities.Logging;

namespace MyNet.Utilities.Localization
{
    public class CultureInfoService
    {
        public event EventHandler? CultureChanged;

        public List<CultureInfo> SupportedCultures { get; } =
        [
            Cultures.English,
            Cultures.French
        ];

        public static CultureInfoService Current { get; } = new CultureInfoService();

        protected CultureInfoService() { }

        public void SetCulture(string cultureCode) => SetCulture(CultureInfo.GetCultureInfo(cultureCode));

        public void SetCulture(CultureInfo culture)
        {
            if (culture == CultureInfo.CurrentCulture) return;

            LogManager.Debug($"Culture Changed : {CultureInfo.CurrentCulture} => {culture} for thread {Environment.CurrentManagedThreadId}");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
