// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Geography;

namespace MyNet.Utilities.Google.Maps
{
    public class GoogleMapsSettings
    {
        public Coordinates? Coordinates { get; set; }

        public string Address { get; set; } = string.Empty;

        public bool HideLeftPanel { get; set; }

    }
}
