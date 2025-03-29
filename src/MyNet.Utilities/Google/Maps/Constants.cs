﻿// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Google.Maps;

public static class Constants
{
    internal static class ApiResponses
    {
        public const string ZeroResults = "ZERO_RESULTS";
        public const string OverQueryLimit = "OVER_QUERY_LIMIT";
        public const string RequestDenied = "REQUEST_DENIED";
    }

    internal static class ApiUriTemplates
    {
        public const string ApiRegionFromLatLong = "maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
        public const string ApiLatLongFromAddress = "maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";
        public const string ApiDirections = "maps.googleapis.com/maps/api/directions/xml?origin={0}&destination={1}&sensor=false";
    }
}
