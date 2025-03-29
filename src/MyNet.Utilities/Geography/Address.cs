// -----------------------------------------------------------------------
// <copyright file="Address.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace MyNet.Utilities.Geography;

public class Address(string? street = null, string? postalCode = null, string? city = null, Country? country = null, double? latitude = null, double? longitude = null) : ValueObject
{
    public string? Street { get; } = street;

    public string? PostalCode { get; } = postalCode;

    public string? City { get; } = city;

    public Country? Country { get; } = country;

    public double? Latitude { get; } = latitude;

    public double? Longitude { get; } = longitude;

    public override string ToString() => string.Join(" ", new[] { Street, PostalCode, City, Country?.Name }.Where(x => !string.IsNullOrEmpty(x)));
}
