// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;

#if NET9_0_OR_GREATER
using System.Threading;
#endif

namespace MyNet.Utilities.Localization
{
    public class TranslationService
    {
        private static readonly Dictionary<CultureInfo, TranslationService> RegisteredServices = [];
        private static readonly Dictionary<string, ResourceManager> Resources = [];

#if NET9_0_OR_GREATER
        private static readonly Lock LockCulture = new();
        private static readonly Lock LockResources = new();
#else
        private static readonly object LockCulture = new();
        private static readonly object LockResources = new();
#endif

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
            var result = Resources.Select(r => r.Value.GetString(key, Culture)).NotNull().LastOrDefault();
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
