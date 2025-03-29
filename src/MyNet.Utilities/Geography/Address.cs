// -----------------------------------------------------------------------
// <copyright file="Address.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace MyNet.Utilities.Geography;

public class Address : ValueObject
{
    public Address(string? street = null, string? postalCode = null, string? city = null, Country? country = null, double? latitude = null, double? longitude = null)
    {
        Street = street;
        PostalCode = postalCode;
        City = city;
        Country = country;
        Latitude = latitude;
        Longitude = longitude;
    }

    public string? Street { get; }

    public string? PostalCode { get; }

    public string? City { get; }

    public Country? Country { get; }

    public double? Latitude { get; }

    public double? Longitude { get; }

    public override string ToString() => string.Join(" ", new[] { Street, PostalCode, City, Country?.Name }.Where(x => !string.IsNullOrEmpty(x)));
}
