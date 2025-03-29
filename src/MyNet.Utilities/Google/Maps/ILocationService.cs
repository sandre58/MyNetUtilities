// -----------------------------------------------------------------------
// <copyright file="ILocationService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Geography;

namespace MyNet.Utilities.Google.Maps;

public interface ILocationService
{
    /// <summary>
    /// Translates a Latitude / Longitude into a Region (state) using Google Maps api.
    /// </summary>
    Region? GetRegionFromCoordinates(double latitude, double longitude);

    /// <summary>
    /// Gets the latitude and longitude that belongs to an address.
    /// </summary>
    /// <param name="address">The address.</param>
    Coordinates? GetCoordinatesFromAddress(string address);

    /// <summary>
    /// Gets the latitude and longitude that belongs to an address.
    /// </summary>
    /// <param name="address">The address.</param>
    Coordinates? GetCoordinatesFromAddress(Address address);

    /// <summary>
    /// Gets the directions.
    /// </summary>
    Directions? GetDirections(Address fromAddress, Address toAddress);
}
