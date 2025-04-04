﻿// -----------------------------------------------------------------------
// <copyright file="CultureExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;

namespace MyNet.Utilities.Localization.Extensions;

public static class CultureExtensions
{
    public static byte[]? GetImage(this CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(culture);

        if (string.IsNullOrEmpty(culture.Name)) return [];

        var usedCulture = culture.IsNeutralCulture ? CultureInfo.CreateSpecificCulture(culture.Name) : culture;
        var name = usedCulture.Name.Replace("-", "_", StringComparison.Ordinal);
        CultureResources.ResourceManager.IgnoreCase = true;
        var obj = (byte[]?)CultureResources.ResourceManager.GetObject(name, CultureInfo.InvariantCulture);
        if (obj != null || culture.IsNeutralCulture) return obj;

        obj = usedCulture.Parent.GetImage();

        return obj ?? [];
    }
}
