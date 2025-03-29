// -----------------------------------------------------------------------
// <copyright file="GoogleMapsSettings.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Geography;

namespace MyNet.Utilities.Google.Maps;

public class GoogleMapsSettings
{
    public Coordinates? Coordinates { get; init; }

    public string Address { get; init; } = string.Empty;

    public bool HideLeftPanel { get; set; }
}
