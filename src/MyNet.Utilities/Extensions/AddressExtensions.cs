// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Geography;
using MyNet.Utilities.Google.Maps;

namespace MyNet.Utilities.Extensions
{
    public static class AddressExtensions
    {
        public static void OpenInGoogleMaps(this Address address)
        {
            var coordinates = address.Latitude.HasValue && address.Longitude.HasValue ? new Coordinates(address.Latitude.Value, address.Longitude.Value) : null;
            var fullAddress = address.ToString();
            GoogleMapsHelper.OpenGoogleMaps(new GoogleMapsSettings { Coordinates = coordinates, Address = fullAddress });
        }
    }
}
