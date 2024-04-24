// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace MyNet.Utilities.Google.Maps
{
    public class Directions
    {
        public enum Status
        {
            Ok,
            Failed
        }

        public Directions() => Steps = [];

        public List<Step> Steps { get; set; }

        public string? Duration { get; set; }

        public string? Distance { get; set; }

        public Status StatusCode { get; set; }
    }
}
