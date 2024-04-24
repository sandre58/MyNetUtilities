// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using MyNet.Utilities.Units;

namespace MyNet.Utilities.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime Max(DateTime date1, DateTime date2) =>
            date1 > date2
            ? date1
            : date2;

        public static DateTime Min(DateTime date1, DateTime date2) =>
            date1 > date2
            ? date2
            : date1;

        public static IEnumerable<DateTime> Range(DateTime min, DateTime max, int step, TimeUnit unit)
        {
            Func<DateTime, DateTime> increment = null!;

            switch (unit)
            {
                case TimeUnit.Millisecond:
                    increment = x => x.AddMilliseconds(step);
                    break;
                case TimeUnit.Second:
                    increment = x => x.AddSeconds(step);
                    break;
                case TimeUnit.Minute:
                    increment = x => x.AddMinutes(step);
                    break;
                case TimeUnit.Hour:
                    increment = x => x.AddHours(step);
                    break;
                case TimeUnit.Day:
                    increment = x => x.AddDays(step);
                    break;
                case TimeUnit.Week:
                    increment = x => x.AddDays(step * 7);
                    break;
                case TimeUnit.Month:
                    increment = x => x.AddMonths(step);
                    break;
                case TimeUnit.Year:
                    increment = x => x.AddYears(step);
                    break;
                default:
                    break;
            }

            for (var i = min; i <= max; i = increment.Invoke(i))
                yield return i;
        }

        public static int NumberOfDaysInWeek() => Enum.GetValues(typeof(DayOfWeek)).Length;
    }
}
