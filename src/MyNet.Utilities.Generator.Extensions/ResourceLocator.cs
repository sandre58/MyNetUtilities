// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Generator.Extensions.Resources;
using MyNet.Utilities.Localization;

namespace MyNet.Utilities.Generator.Extensions
{
    public static class ResourceLocator
    {
        private static bool _isInitialized;

        public static void Initialize()
        {
            if (_isInitialized) return;

            TranslationService.RegisterResources(nameof(NamesResources), NamesResources.ResourceManager);
            TranslationService.RegisterResources(nameof(InternetResources), InternetResources.ResourceManager);
            TranslationService.RegisterResources(nameof(AddressResources), AddressResources.ResourceManager);

            _isInitialized = true;
        }

    }
}
