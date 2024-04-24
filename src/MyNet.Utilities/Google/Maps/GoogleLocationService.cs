﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using MyNet.Utilities.Geography;

namespace MyNet.Utilities.Google.Maps
{
    public class GoogleLocationService(string apikey, bool useHttps) : ILocationService
    {
        private readonly string _apiKey = apikey;
        private readonly string _urlProtocolPrefix = useHttps ? "https://" : "http://";

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleLocationService"/> class.
        /// </summary>
        /// <param name="useHttps">Indicates whether to call the Google API over HTTPS or not.</param>
        public GoogleLocationService(bool useHttps) : this(string.Empty, useHttps) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleLocationService"/> class. Default calling the API over regular
        /// HTTP (not HTTPS).
        /// </summary>
        public GoogleLocationService() : this(string.Empty, false) { }

        private string ApiUrlRegionFromLatLong => _urlProtocolPrefix + Constants.ApiUriTemplates.ApiRegionFromLatLong;

        private string ApiUrlLatLongFromAddress => _urlProtocolPrefix + Constants.ApiUriTemplates.ApiLatLongFromAddress;

        private string ApiUrlDirections => _urlProtocolPrefix + Constants.ApiUriTemplates.ApiDirections;

        /// <summary>
        /// Translates a Latitude / Longitude into a Region (state) using Google Maps api
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public Region? GetRegionFromCoordinates(double latitude, double longitude)
        {
            var doc = XDocument.Load(string.Format(CultureInfo.InvariantCulture, ApiUrlRegionFromLatLong, latitude, longitude) + "&key=" + _apiKey);

            var els = doc.Descendants("result").First().Descendants("address_component").FirstOrDefault(s => s.Descendants("type").First().Value == "administrative_area_level_1");

            return null != els
                ? new Region() { Name = els.Descendants("long_name").First().Value, ShortCode = els.Descendants("short_name").First().Value }
                : null;
        }

        /// <summary>
        /// Translates a Latitude / Longitude into an address using Google Maps api
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public Address? GetAddressFromCoordinates(double latitude, double longitude)
        {
            var addressCountry = string.Empty;
            var addressLocality = string.Empty;
            var addressSublocality = string.Empty;
            var addressRoute = string.Empty;
            var addressStreetNumber = string.Empty;
            var addressPostalCode = string.Empty;

            var doc = new XmlDocument();

            doc.Load(string.Format(CultureInfo.InvariantCulture, ApiUrlRegionFromLatLong, latitude, longitude) + "&key=" + _apiKey);
            var element = doc.SelectSingleNode("//GeocodeResponse/status");
            if (element == null || element.InnerText == Constants.ApiResponses.ZeroResults)
            {
                return null;
            }

            var xnList = doc.SelectNodes("//GeocodeResponse/result/address_component");

            if (xnList == null) return null;

            foreach (XmlNode? xn in xnList)
            {
                var longname = xn?["long_name"]?.InnerText;
                var shortname = xn?["short_name"]?.InnerText;
                var typename = xn?["type"]?.InnerText;

                switch (typename)
                {
                    case "country":
                        addressCountry = longname;
                        break;
                    case "locality":
                        addressLocality = longname;
                        break;
                    case "sublocality":
                        addressSublocality = longname;
                        break;
                    case "neighborhood":
                    case "colloquial_area":
                    case "administrative_area_level_1":
                    case "administrative_area_level_2":
                    case "administrative_area_level_3":
                        break;
                    case "route":
                        addressRoute = shortname;
                        break;
                    case "street_number":
                        addressStreetNumber = shortname;
                        break;
                    case "postal_code":
                        addressPostalCode = longname;
                        break;
                }
            }

            _ = Country.TryFromName(addressCountry.OrEmpty(), out var country);
            return new Address(string.Join(" ", new[] { addressStreetNumber, addressRoute, addressSublocality }.NotNull()), addressPostalCode, addressLocality, country, latitude, longitude);
        }



        /// <summary>
        /// Gets the latitude and longitude that belongs to an address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        /// <exception cref="System.Net.WebException"></exception>
        public Coordinates? GetCoordinatesFromAddress(string address)
        {
            var doc = XDocument.Load(string.Format(CultureInfo.InvariantCulture, ApiUrlLatLongFromAddress, Uri.EscapeDataString(address)) + "&key=" + _apiKey);

            var status = doc?.Descendants("status").FirstOrDefault()?.Value;
            if (status == Constants.ApiResponses.OverQueryLimit)
            {
                throw new QueryLimitExceededException("QueryLimit exceeded, check your dashboard");
            }

            if (status == Constants.ApiResponses.RequestDenied)
            {
                throw new RequestDeniedException("Request denied, it's likely you need to enable the necessary Google maps APIs");
            }

            var els = doc?.Descendants("result").Descendants("geometry").Descendants("location").FirstOrDefault();
            if (null != els)
            {
                var latitude = ParseUS((els.Nodes().First() as XElement)?.Value);
                var longitude = ParseUS((els.Nodes().ElementAt(1) as XElement)?.Value);
                return latitude.HasValue && longitude.HasValue ? new Coordinates(latitude.Value, longitude.Value) : null;
            }
            return null;
        }

        /// <summary>
        /// Gets the latitude and longitude that belongs to an address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public Coordinates? GetCoordinatesFromAddress(Address address) => GetCoordinatesFromAddress(address.ToString());

        /// <summary>
        /// Gets an array of string addresses that matched a possibly ambiguous address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        /// <exception cref="System.Net.WebException"></exception>
        public string[] GetAddressesListFromAddress(string address)
        {
            var doc = XDocument.Load(string.Format(CultureInfo.InvariantCulture, ApiUrlLatLongFromAddress, Uri.EscapeDataString(address)) + "&key=" + _apiKey);
            var status = doc?.Descendants("status").FirstOrDefault()?.Value;

            if (status == Constants.ApiResponses.OverQueryLimit)
            {
                throw new QueryLimitExceededException("QueryLimit exceeded, check your dashboard");
            }

            if (status == Constants.ApiResponses.RequestDenied)
            {
                throw new RequestDeniedException("Request denied, it's likely you need to enable the necessary Google maps APIs");
            }

            var results = doc?.Descendants("result").Descendants("formatted_address").ToArray();
            var addresses = (from elem in results select elem.Value).ToArray();
            return addresses.Length > 0 ? addresses : [];
        }

        /// <summary>
        /// Gets an array of string addresses that matched a possibly ambiguous address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        /// <exception cref="System.Net.WebException"></exception>
        public string[] GetAddressesListFromAddress(Address address) => GetAddressesListFromAddress(address.ToString());

        /// <summary>
        /// Gets the directions.
        /// </summary>
        /// <param name="fromAddress">From address.</param>
        /// <param name="toAddress">To address.</param>
        /// <returns>The directions</returns>
        public Directions GetDirections(Address fromAddress, Address toAddress)
        {
            var direction = new Directions();

            var xdoc = XDocument.Load(string.Format(CultureInfo.InvariantCulture,
                                                ApiUrlDirections,
                                                Uri.EscapeDataString(fromAddress.ToString()),
                                                Uri.EscapeDataString(toAddress.ToString())) + "&key=" + _apiKey);

            var status = (from s in xdoc.Descendants("DirectionsResponse").Descendants("status")
                          select s).FirstOrDefault();

            if (status != null && status.Value == "OK")
            {
                direction.StatusCode = Directions.Status.Ok;
                var distance = (from d in xdoc.Descendants("DirectionsResponse").Descendants("route").Descendants("leg")
                               .Descendants("distance").Descendants("text")
                                select d).LastOrDefault();

                if (distance != null)
                {
                    direction.Distance = distance.Value;
                }

                var duration = (from d in xdoc.Descendants("DirectionsResponse").Descendants("route").Descendants("leg")
                               .Descendants("duration").Descendants("text")
                                select d).LastOrDefault();

                if (duration != null)
                {
                    direction.Duration = duration.Value;
                }

                var steps = from s in xdoc.Descendants("DirectionsResponse").Descendants("route").Descendants("leg").Descendants("step")
                            select s;

                foreach (var step in steps)
                {
                    var directionStep = new Step
                    {
                        Instruction = step?.Element("html_instructions")?.Value,
                        Distance = step?.Descendants("distance").First()?.Element("text")?.Value
                    };
                    direction.Steps.Add(directionStep);

                }
                return direction;
            }
            else if (status != null && status.Value != "OK")
            {
                direction.StatusCode = Directions.Status.Failed;
                return direction;
            }
            else
            {
                throw new InvalidOperationException("Unable to get Directions from Google");
            }

        }

        private static double? ParseUS(string? value) => value is null ? null : double.Parse(value, new CultureInfo("en-US"));
    }
}
