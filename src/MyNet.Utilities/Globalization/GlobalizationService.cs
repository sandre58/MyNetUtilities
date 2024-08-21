// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using MyNet.Utilities.Localization;
using MyNet.Utilities.Logging;

namespace MyNet.Utilities.Globalization
{
    public class GlobalizationService
    {
        public event EventHandler? CultureChanged;
        public event EventHandler? CalendarChanged;

        public List<CultureInfo> SupportedCultures { get; } =
        [
            Cultures.English,
            Cultures.French
        ];

        public ReadOnlyCollection<TimeZoneInfo> SupportedTimeZones { get; } = TimeZoneInfo.GetSystemTimeZones();

        public CalendarService Calendar { get; private set; }

        public LocalizationService Localization { get; private set; } = new(CultureInfo.CurrentCulture);

        public static GlobalizationService Current { get; } = new GlobalizationService();

        private GlobalizationService() => Calendar = new CalendarService(TimeZoneInfo.Local);

        public void SetCulture(string cultureCode) => SetCulture(CultureInfo.GetCultureInfo(cultureCode));

        public void SetCulture(CultureInfo culture)
        {
            if (culture == CultureInfo.CurrentCulture) return;

            LogManager.Debug($"Culture Changed : {CultureInfo.CurrentCulture} => {culture} for thread {Environment.CurrentManagedThreadId}");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            Localization = new LocalizationService(culture);

            CultureChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetTimeZoneInfo(TimeZoneInfo timeZoneinfo)
        {
            if (Calendar.TimeZone == timeZoneinfo) return;

            LogManager.Debug($"Time zone Changed : {Calendar.TimeZone} => {timeZoneinfo}");
            Calendar = new CalendarService(timeZoneinfo);

            CalendarChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
