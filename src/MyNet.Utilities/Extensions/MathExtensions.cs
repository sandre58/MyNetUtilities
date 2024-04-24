// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities
{
    public static class MathExtensions
    {
        // Smallest double value such that 1.0+DBL_EPSILON != 1.0
        public const double DBL_EPSILON = 2.2204460492503131e-016;

        public static bool NearlyEqual(this double value1, double value2, double epsilon = double.Epsilon) => Math.Abs(value1 - value2) < epsilon;

        public static bool NearlyEqual(this double? value1, double? value2, double epsilon = double.Epsilon) => !value1.HasValue && !value2.HasValue
|| value1.HasValue && value2.HasValue && value1.Value.NearlyEqual(value2.Value, epsilon);

        public static bool NearlyEqual(this float value1, float value2, float epsilon = float.Epsilon) => Math.Abs(value1 - value2) < epsilon;

        public static bool NearlyEqual(this float? value1, float? value2, float epsilon = float.Epsilon) => !value1.HasValue && !value2.HasValue
|| value1.HasValue && value2.HasValue && value1.Value.NearlyEqual(value2.Value, epsilon);

        public static double ExtractDouble(this object val)
        {
            var d = val as double? ?? double.NaN;
            return double.IsInfinity(d) ? double.NaN : d;
        }

        public static bool AnyNan(this IEnumerable<double> vals) => vals.Any(double.IsNaN);

        /// <summary>
        /// Returns whether or not two doubles are "close". 
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <returns>
        /// bool - the result of the AreClose comparision.
        /// </returns>
        public static bool IsCloseTo(this double value1, double value2)
        {
            //in case they are Infinities (then epsilon check does not work)
            if (value1.NearlyEqual(value2))
            {
                return true;
            }

            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            var eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * DBL_EPSILON;
            var delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }
    }
}
