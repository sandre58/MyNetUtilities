// -----------------------------------------------------------------------
// <copyright file="AddressExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Geography;
using MyNet.Utilities.Google.Maps;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class AddressExtensions
{
    public static void OpenInGoogleMaps(this Address address)
    {
        var coordinates = address is { Latitude: not null, Longitude: not null } ? new Coordinates(address.Latitude.Value, address.Longitude.Value) : null;
        var fullAddress = address.ToString();
        GoogleMapsHelper.OpenGoogleMaps(new GoogleMapsSettings { Coordinates = coordinates, Address = fullAddress });
    }
}
