// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

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
        private static readonly object LockCulture = new();
        private static readonly object LockResources = new();

        public CultureInfo Culture { get; }

        public static TranslationService Current => Get(CultureInfo.CurrentCulture);

        public static TranslationService GetOrCurrent(CultureInfo? cultureInfo = null) => cultureInfo is not null ? Get(cultureInfo) : Current;

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

        public string Translate(string key)
        {
            var result = Resources.Select(r => r.Value.GetString(key, Culture)).ToList().NotNull().LastOrDefault();
            return !string.IsNullOrEmpty(result) ? result : key;
        }

        public string Translate(string key, string filename) => Resources.TryGetValue(filename, out var value) && value.GetString(key, Culture) is string result ? result : key;

        public string this[string key] => Translate(key);

        public string this[string key, string filename] => Translate(key, filename);

        public static void RegisterResources(string resourceKey, ResourceManager resourceManager)
        {
            lock (LockResources)
            {
                Resources.TryAdd(resourceKey, resourceManager);
            }
        }
    }
}
