// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Geography;

namespace MyNet.Utilities.Google.Maps
{
    public interface ILocationService
    {
        /// <summary>
        /// Translates a Latitude / Longitude into a Region (state) using Google Maps api
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        Region? GetRegionFromCoordinates(double latitude, double longitude);

        /// <summary>
        /// Gets the latitude and longitude that belongs to an address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        Coordinates? GetCoordinatesFromAddress(string address);

        /// <summary>
        /// Gets the latitude and longitude that belongs to an address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        Coordinates? GetCoordinatesFromAddress(Address address);

        /// <summary>
        /// Gets the directions.
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <returns>The directions</returns>
        Directions? GetDirections(Address fromAddress, Address toAddress);

    }
}
