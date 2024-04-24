// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;

namespace MyNet.Utilities.Geography
{
    public class Address : ValueObject
    {
        public string? Street { get; }

        public string? PostalCode { get; }

        public string? City { get; }

        public Country? Country { get; }

        public double? Latitude { get; }

        public double? Longitude { get; }

        public Address(string? street = null, string? postalCode = null, string? city = null, Country? country = null, double? latitude = null, double? longitude = null)
        {
            Street = street;
            PostalCode = postalCode;
            City = city;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString() => string.Join(" ", new[] { Street, PostalCode, City, Country?.Name }.Where(x => !string.IsNullOrEmpty(x)));
    }
}
