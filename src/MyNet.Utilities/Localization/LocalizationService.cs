// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using MyNet.Utilities.Localization;

namespace MyNet.Utilities.Globalization
{
    public class LocalizationService
    {
        private readonly object _lockProviders = new();
        private readonly Dictionary<Type, object> _providers = [];
        private readonly Func<CultureInfo> _getCulture;

        public CultureInfo Culture => _getCulture();

        public LocalizationService() : this(CultureInfo.CurrentCulture) { }

        public LocalizationService(CultureInfo culture) : this(() => culture) { }

        private LocalizationService(Func<CultureInfo> getCulture) => _getCulture = getCulture;

        public LocalizationService RegisterProvider<TInterface, TProvider>() where TProvider : TInterface => RegisterProvider<TInterface, TProvider>(Activator.CreateInstance<TProvider>());

        public LocalizationService RegisterProvider<TInterface, TProvider>(TProvider localizer) where TProvider : TInterface
        {
            lock (_lockProviders)
            {
                if (localizer is not null)
                {
                    if (!_providers.ContainsKey(typeof(TInterface)))
                    {
                        _providers.Add(typeof(TInterface), localizer);
                    }
                    else
                    {
                        _providers[typeof(TInterface)] = localizer;
                    }
                }
            }

            return this;
        }

        public TProvider? GetProvider<TProvider>() => _providers.TryGetValue(typeof(TProvider), out var value) ? (TProvider?)value : GetProvider<TProvider>(Culture);

        public string Translate(string key) => TranslationService.Get(Culture).Translate(key);

        public string Translate(string key, string filename) => TranslationService.Get(Culture).Translate(key, filename);

        #region Static providers management

        private static readonly Dictionary<CultureInfo, IDictionary<Type, object>> CultureProviders = [];
        private static readonly Dictionary<Type, object> DefaultProviders = [];
        private static readonly object LockCultureProviders = new();
        private static readonly object LockDefaultProviders = new();

        public static void RegisterProvider<TInterface, TProvider>(CultureInfo culture) where TProvider : TInterface => RegisterProvider<TInterface, TProvider>(culture, Activator.CreateInstance<TProvider>());

        public static void RegisterProvider<TInterface, TProvider>(CultureInfo culture, TProvider provider) where TProvider : TInterface
        {
            lock (LockCultureProviders)
            {
                if (provider is not null)
                {
                    if (!CultureProviders.ContainsKey(culture))
                        CultureProviders.Add(culture, new Dictionary<Type, object>());

                    if (!CultureProviders[culture].ContainsKey(typeof(TInterface)))
                    {
                        CultureProviders[culture].Add(typeof(TInterface), provider);
                    }
                    else
                    {
                        CultureProviders[culture][typeof(TInterface)] = provider;
                    }
                }
            }
        }

        public static void RegisterDefaultProvider<TInterface, TProvider>() where TProvider : TInterface => RegisterDefaultProvider<TInterface, TProvider>(Activator.CreateInstance<TProvider>());

        public static void RegisterDefaultProvider<TInterface, TProvider>(TProvider provider) where TProvider : TInterface
        {
            lock (LockDefaultProviders)
            {
                if (provider is not null)
                {
                    if (!DefaultProviders.ContainsKey(typeof(TInterface)))
                    {
                        DefaultProviders.Add(typeof(TInterface), provider);
                    }
                    else
                    {
                        DefaultProviders[typeof(TInterface)] = provider;
                    }
                }
            }
        }

        public static TProvider? GetProvider<TProvider>(CultureInfo culture)
        {
            var provider = GetValueOrDefault<TProvider>(CultureProviders.GetOrDefault(culture, new Dictionary<Type, object>())!, typeof(TProvider));

            if (provider is not null || culture.IsNeutralCulture) return provider;

            provider = GetProvider<TProvider>(culture.Parent);
            return provider is not null ? provider : GetValueOrDefault<TProvider>(DefaultProviders, typeof(TProvider));
        }

        private static TProvider? GetValueOrDefault<TProvider>(IDictionary<Type, object> dictionary, Type type)
            => dictionary.TryGetValue(type, out var value) ? (TProvider?)value : default;

        #endregion
    }
}
