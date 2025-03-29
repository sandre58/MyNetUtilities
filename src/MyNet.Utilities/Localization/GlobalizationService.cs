// -----------------------------------------------------------------------
// <copyright file="GlobalizationService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using MyNet.Utilities.Logging;

namespace MyNet.Utilities.Localization;

public class GlobalizationService
{
    private readonly Action<CultureInfo>? _onCultureChanged;

    public GlobalizationService()
        : this(CultureInfo.CurrentCulture, TimeZoneInfo.Local) { }

    public GlobalizationService(CultureInfo culture, TimeZoneInfo timeZoneInfo)
        : this(culture, timeZoneInfo, null) { }

    private GlobalizationService(CultureInfo culture, TimeZoneInfo timeZoneInfo, Action<CultureInfo>? onCultureChanged)
    {
        Culture = culture;
        TimeZone = timeZoneInfo;
        _onCultureChanged = onCultureChanged;
    }

    public event EventHandler? CultureChanged;

    public event EventHandler? TimeZoneChanged;

    public static GlobalizationService Current { get; } = new(CultureInfo.CurrentCulture, TimeZoneInfo.Local, x =>
    {
        CultureInfo.CurrentCulture = x;
        CultureInfo.CurrentUICulture = x;
    });

    public IList<CultureInfo> SupportedCultures { get; } =
    [
        Cultures.English,
        Cultures.French
    ];

    public ReadOnlyCollection<TimeZoneInfo> SupportedTimeZones { get; } = TimeZoneInfo.GetSystemTimeZones();

    public TimeZoneInfo TimeZone { get; private set; }

    public CultureInfo Culture { get; private set; }

    public DateTime Date => DateTime.UtcNow.ToTimeZone(TimeZone);

    public virtual DateTime UtcDate => DateTime.UtcNow;

    public void SetCulture(string cultureCode) => SetCulture(CultureInfo.GetCultureInfo(cultureCode));

    public void SetCulture(CultureInfo culture)
    {
        if (Equals(culture, Culture)) return;

        LogManager.Debug($"Culture Changed : {Culture} => {culture} for thread {Environment.CurrentManagedThreadId}");
        Culture = culture;
        _onCultureChanged?.Invoke(Culture);

        CultureChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetTimeZone(TimeZoneInfo timeZone)
    {
        if (TimeZone.Equals(timeZone)) return;

        LogManager.Debug($"Time zone Changed : {TimeZone} => {timeZone}");
        TimeZone = timeZone;

        TimeZoneChanged?.Invoke(this, EventArgs.Empty);
    }

    public DateTime Convert(DateTime dateTime) => dateTime.Kind switch
    {
        DateTimeKind.Local => TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, TimeZone),
        _ => TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), TimeZone)
    };

    public DateTime ConvertToUtc(DateTime dateTime) => dateTime.Kind switch
    {
        DateTimeKind.Unspecified => TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZone),
        DateTimeKind.Local => dateTime.ToUniversalTime(),
        _ => dateTime
    };

    public DateTime ConvertFromTimeZone(DateTime dateTime, TimeZoneInfo sourceTimeZone) => dateTime.Kind switch
    {
        DateTimeKind.Utc => TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc, TimeZone),
        DateTimeKind.Local => TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, TimeZone),
        _ => TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, TimeZone)
    };

    public DateTime ConvertToTimeZone(DateTime dateTime, TimeZoneInfo destinationTimeZone) => dateTime.Kind switch
    {
        DateTimeKind.Utc => TimeZoneInfo.ConvertTime(Convert(dateTime), TimeZone, TimeZoneInfo.Utc),
        DateTimeKind.Local => TimeZoneInfo.ConvertTime(Convert(dateTime), TimeZone, TimeZoneInfo.Local),
        _ => TimeZoneInfo.ConvertTime(dateTime, TimeZone, destinationTimeZone)
    };

    public TProvider? GetProvider<TProvider>() => LocalizationService.Get<TProvider>(Culture);

    public string Translate(string key) => TranslationService.Get(Culture)[key];

    public string Translate(string key, string filename) => TranslationService.Get(Culture)[key, filename];

    public string TranslateAbbreviated(string key) => Translate(key.ToAbbreviationKey());

    public string TranslateAbbreviated(string key, string filename) => Translate(key.ToAbbreviationKey(), filename);
}
