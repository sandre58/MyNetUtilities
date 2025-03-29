// -----------------------------------------------------------------------
// <copyright file="ResourceLocator.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Generator.Extensions.Resources;
using MyNet.Utilities.Localization;

namespace MyNet.Utilities.Generator.Extensions;

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
