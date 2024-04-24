// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace MyNet.Utilities.Localization
{
    public class TranslationService
    {
        private static readonly Dictionary<CultureInfo, TranslationService> RegisteredServices = [];
        private static readonly Dictionary<string, ResourceManager> Resources = [];
        private static readonly Dictionary<Type, object> DefaultProviders = [];
        private static readonly object LockCulture = new();
        private static readonly object LockResources = new();
        private static readonly object LockProviders = new();
        private static readonly object LockDefaultProviders = new();

        private readonly Dictionary<Type, object> _providers = [];

        public CultureInfo Culture { get; }

        public static TranslationService Current => Get(CultureInfo.CurrentUICulture);

        public static TranslationService Get(CultureInfo culture)
        {
            lock (LockCulture)
            {
                if (!RegisteredServices.ContainsKey(culture))
                {
                    RegisteredServices.Add(culture, new TranslationService(culture));
                }
            }

            return RegisteredServices[culture];
        }

        private TranslationService(CultureInfo culture) => Culture = culture;

        public string? Translate(string? key)
        {
            var result = Resources.Select(r => r.Value.GetString(key ?? string.Empty, Culture)).NotNull().LastOrDefault();
            return !string.IsNullOrEmpty(result) ? result : key;
        }

        public string? Translate(string? key, string? filename) => filename != null && Resources.TryGetValue(filename, out var value) ? value.GetString(key ?? string.Empty, Culture) : key;

        public string? this[string key] => Translate(key);

        public string? this[string key, string? filename] => Translate(key, filename);

        public static void RegisterResources(string? resourceKey, ResourceManager resourceManager)
        {
            lock (LockResources)
            {
                if (resourceKey != null && !Resources.ContainsKey(resourceKey))
                    Resources.Add(resourceKey, resourceManager);
            }
        }

        public TranslationService AddProvider<TInterface, TProvider>() where TProvider : TInterface => AddProvider<TInterface, TProvider>(Activator.CreateInstance<TProvider>());

        public TranslationService AddProvider<TInterface, TProvider>(TProvider localizer) where TProvider : TInterface
        {
            lock (LockProviders)
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

        public static void AddDefaultProvider<TInterface, TProvider>() where TProvider : TInterface => AddDefaultProvider<TInterface, TProvider>(Activator.CreateInstance<TProvider>());

        public static void AddDefaultProvider<TInterface, TProvider>(TProvider provider) where TProvider : TInterface
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

        public TLocalizer? GetProvider<TLocalizer>()
        {
            var localizer = GetValueOrDefault<TLocalizer>(_providers, typeof(TLocalizer));

            if (localizer is not null || Culture.IsNeutralCulture) return localizer;

            localizer = Get(Culture.Parent).GetProvider<TLocalizer>();
            return localizer is not null ? localizer : GetValueOrDefault<TLocalizer>(DefaultProviders, typeof(TLocalizer));
        }

        private static TLocalizer? GetValueOrDefault<TLocalizer>(Dictionary<Type, object> dictionary, Type type)
            => dictionary.TryGetValue(type, out var value) ? (TLocalizer?)value : default;
    }
}
