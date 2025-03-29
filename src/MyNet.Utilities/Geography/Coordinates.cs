// -----------------------------------------------------------------------
// <copyright file="Coordinates.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Geography;

public class Coordinates(double latitude, double longitude)
{
    public double Latitude { get; set; } = latitude;

    public double Longitude { get; set; } = longitude;
}
