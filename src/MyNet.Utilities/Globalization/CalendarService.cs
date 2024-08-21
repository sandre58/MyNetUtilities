// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Globalization
{
    public class CalendarService
    {
        public TimeZoneInfo TimeZone { get; }

        public CalendarService(TimeZoneInfo timeZone) => TimeZone = timeZone;

        public DateTime ConvertFromUtc(DateTime utcDateTime) => TimeZoneInfo.ConvertTimeFromUtc(utcDateTime.ToUniversalTime(), TimeZone);

        public DateTime ConvertToUtc(DateTime utcDateTime) => TimeZoneInfo.ConvertTimeToUtc(utcDateTime, TimeZone);
    }
}
