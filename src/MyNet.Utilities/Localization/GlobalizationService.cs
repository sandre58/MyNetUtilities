// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using MyNet.Utilities.Extensions;
using MyNet.Utilities.Logging;

namespace MyNet.Utilities.Localization
{
    public class GlobalizationService
    {
        private CultureInfo _culture;
        private TimeZoneInfo _timeZone;
        private readonly Action<CultureInfo>? _onCultureChanged;

        public event EventHandler? CultureChanged;
        public event EventHandler? TimeZoneChanged;

        public List<CultureInfo> SupportedCultures { get; } =
        [
            Cultures.English,
            Cultures.French
        ];

        public ReadOnlyCollection<TimeZoneInfo> SupportedTimeZones { get; } = TimeZoneInfo.GetSystemTimeZones();

        public static GlobalizationService Current { get; } = new GlobalizationService(CultureInfo.CurrentCulture, TimeZoneInfo.Local, x =>
        {
            CultureInfo.DefaultThreadCurrentCulture = x;
            CultureInfo.DefaultThreadCurrentUICulture = x;
            CultureInfo.CurrentCulture = x;
            CultureInfo.CurrentUICulture = x;
        });

        public GlobalizationService() : this(CultureInfo.CurrentCulture, TimeZoneInfo.Local) { }

        public GlobalizationService(CultureInfo culture, TimeZoneInfo timeZoneInfo) : this(culture, timeZoneInfo, null) { }

        private GlobalizationService(CultureInfo culture, TimeZoneInfo timeZoneInfo, Action<CultureInfo>? onCultureChanged)
        {
            _culture = culture;
            _timeZone = timeZoneInfo;
            _onCultureChanged = onCultureChanged;
        }

        public TimeZoneInfo TimeZone => _timeZone;

        public CultureInfo Culture => _culture;

        public DateTime Date => DateTime.UtcNow.ToTimeZone(_timeZone);

        public virtual DateTime UtcDate => DateTime.UtcNow;

        public void SetCulture(string cultureCode) => SetCulture(CultureInfo.GetCultureInfo(cultureCode));

        public void SetCulture(CultureInfo culture)
        {
            if (culture == _culture) return;

            LogManager.Debug($"Culture Changed : {_culture} => {culture} for thread {Environment.CurrentManagedThreadId}");
            _culture = culture;
            _onCultureChanged?.Invoke(_culture);

            CultureChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetTimeZone(TimeZoneInfo timeZone)
        {
            if (_timeZone == timeZone) return;

            LogManager.Debug($"Time zone Changed : {_timeZone} => {timeZone}");
            _timeZone = timeZone;

            TimeZoneChanged?.Invoke(this, EventArgs.Empty);
        }

        public DateTime Convert(DateTime dateTime) => TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), _timeZone);

        public DateTime ConvertToUtc(DateTime dateTime) => TimeZoneInfo.ConvertTimeToUtc(dateTime, _timeZone);

        public DateTime ConvertFromTimeZone(DateTime dateTime, TimeZoneInfo sourceTimeZone) => TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, _timeZone);

        public DateTime ConvertToTimeZone(DateTime dateTime, TimeZoneInfo destinationTimeZone) => TimeZoneInfo.ConvertTime(dateTime, _timeZone, destinationTimeZone);

        public TProvider? GetProvider<TProvider>() => LocalizationService.Get<TProvider>(_culture);

        public string Translate(string key) => TranslationService.Get(_culture)[key];

        public string Translate(string key, string filename) => TranslationService.Get(_culture)[key, filename];

        public string TranslateAbbreviated(string key) => Translate(key.ToAbbreviationKey());

        public string TranslateAbbreviated(string key, string filename) => Translate(key.ToAbbreviationKey(), filename);
    }
}
