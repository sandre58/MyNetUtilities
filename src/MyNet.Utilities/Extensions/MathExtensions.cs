// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Helpers;

namespace MyNet.Utilities
{
    public static class MathExtensions
    {
        public const double DBL_EPSILON = 2.2204460492503131e-016;
        public const float FLOAT_EPSILON = 1.1920929E-07f;

        public static bool NearlyEqual(this double value1, double value2, double epsilon = double.Epsilon) => Math.Abs(value1 - value2) < epsilon;

        public static bool NearlyEqual(this double? value1, double? value2, double epsilon = double.Epsilon) => !value1.HasValue && !value2.HasValue
            || value1.HasValue && value2.HasValue && value1.Value.NearlyEqual(value2.Value, epsilon);

        public static bool NearlyEqual(this float value1, float value2, float epsilon = float.Epsilon) => Math.Abs(value1 - value2) < epsilon;

        public static bool NearlyEqual(this float? value1, float? value2, float epsilon = float.Epsilon) => !value1.HasValue && !value2.HasValue
            || value1.HasValue && value2.HasValue && value1.Value.NearlyEqual(value2.Value, epsilon);

        public static bool IsEven(this int value) => value % 2 == 0;

        public static bool IsOdd(this int value) => value % 2 == 1;

        public static void Iteration(this int value, Action<int> action) => EnumerableHelper.Iteration(value, action);

        public static IEnumerable<int> Range(this int value, int min = 1, int step = 1) => EnumerableHelper.Range(min, value, step);

        public static double ExtractDouble(this object val)
        {
            var d = val as double? ?? double.NaN;
            return double.IsInfinity(d) ? double.NaN : d;
        }

        public static bool AnyNan(this IEnumerable<double> vals) => vals.Any(double.IsNaN);

        /// <summary>
        /// Returns whether or not two float are "close". 
        /// </summary>
        /// <param name="value1"> The first float to compare. </param>
        /// <param name="value2"> The second float to compare. </param>
        /// <returns>
        /// bool - the result of the AreClose comparision.
        /// </returns>
        public static bool IsCloseTo(this float value1, float value2)
        {
            //in case they are Infinities (then epsilon check does not work)
            if (value1.NearlyEqual(value2))
            {
                return true;
            }

            var eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * FLOAT_EPSILON;
            var delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

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

        /// <summary>
        ///     Clamps a value between a minimum and maximum value.
        /// </summary>
        /// <param name="value"> The value to clamp. </param>
        /// <param name="min"> The minimum value. </param>
        /// <param name="max"> The maximum value. </param>
        /// <returns></returns>
        public static double SafeClamp(this double value, double min, double max)
        {
            (min, max) = MathHelper.GetMinMax(min, max);
            return value < min ? min : value > max ? max : value;
        }

        /// <summary>
        ///     Clamps a value between a minimum and maximum value.
        /// </summary>
        /// <param name="value"> The value to clamp. </param>
        /// <param name="min"> The minimum value. </param>
        /// <param name="max"> The maximum value. </param>
        /// <returns></returns>
        public static decimal SafeClamp(this decimal value, decimal min, decimal max)
        {
            (min, max) = MathHelper.GetMinMax(min, max);
            return value < min ? min : value > max ? max : value;
        }

        /// <summary>
        ///     Clamps a value between a minimum and maximum value.
        /// </summary>
        /// <param name="value"> The value to clamp. </param>
        /// <param name="min"> The minimum value. </param>
        /// <param name="max"> The maximum value. </param>
        /// <returns></returns>
        public static int SafeClamp(this int value, int min, int max)
        {
            (min, max) = MathHelper.GetMinMax(min, max);
            return value < min ? min : value > max ? max : value;
        }

        /// <summary>
        ///     Clamps a value between a minimum and maximum value.
        /// </summary>
        /// <param name="value"> The value to clamp. </param>
        /// <param name="min"> The minimum value. </param>
        /// <param name="max"> The maximum value. </param>
        /// <returns></returns>
        public static float SafeClamp(this float value, float min, float max)
        {
            (min, max) = MathHelper.GetMinMax(min, max);
            return value < min ? min : value > max ? max : value;
        }

        /// <summary>
        /// LessThan - Returns whether or not the first double is less than the second double.
        /// That is, whether or not the first is strictly less than *and* not within epsilon of
        /// the other number.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        public static bool LessThan(this double value1, double value2) => value1 < value2 && !value1.IsCloseTo(value2);

        /// <summary>
        /// LessThan - Returns whether or not the first float is less than the second float.
        /// That is, whether or not the first is strictly less than *and* not within epsilon of
        /// the other number.
        /// </summary>
        /// <param name="value1"> The first single float to compare. </param>
        /// <param name="value2"> The second single float to compare. </param>
        public static bool LessThan(this float value1, float value2) => value1 < value2 && !value1.IsCloseTo(value2);

        /// <summary>
        /// GreaterThan - Returns whether or not the first double is greater than the second double.
        /// That is, whether or not the first is strictly greater than *and* not within epsilon of
        /// the other number.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        public static bool GreaterThan(this double value1, double value2) => value1 > value2 && !value1.IsCloseTo(value2);

        /// <summary>
        /// GreaterThan - Returns whether or not the first float is greater than the second float.
        /// That is, whether or not the first is strictly greater than *and* not within epsilon of
        /// the other number.
        /// </summary>
        /// <param name="value1"> The first float to compare. </param>
        /// <param name="value2"> The second float to compare. </param>
        public static bool GreaterThan(this float value1, float value2) => value1 > value2 && !value1.IsCloseTo(value2);

        /// <summary>
        /// LessThanOrClose - Returns whether or not the first double is less than or close to
        /// the second double.  That is, whether or not the first is strictly less than or within
        /// epsilon of the other number.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        public static bool LessThanOrClose(this double value1, double value2) => value1 < value2 || value1.IsCloseTo(value2);

        /// <summary>
        /// LessThanOrClose - Returns whether or not the first float is less than or close to
        /// the second float.  That is, whether or not the first is strictly less than or within
        /// epsilon of the other number.
        /// </summary>
        /// <param name="value1"> The first float to compare. </param>
        /// <param name="value2"> The second float to compare. </param>
        public static bool LessThanOrClose(this float value1, float value2) => value1 < value2 || value1.IsCloseTo(value2);

        /// <summary>
        /// GreaterThanOrClose - Returns whether or not the first double is greater than or close to
        /// the second double.  That is, whether or not the first is strictly greater than or within
        /// epsilon of the other number.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        public static bool GreaterThanOrClose(this double value1, double value2) => value1 > value2 || value1.IsCloseTo(value2);

        /// <summary>
        /// GreaterThanOrClose - Returns whether or not the first float is greater than or close to
        /// the second float.  That is, whether or not the first is strictly greater than or within
        /// epsilon of the other number.
        /// </summary>
        /// <param name="value1"> The first float to compare. </param>
        /// <param name="value2"> The second float to compare. </param>
        public static bool GreaterThanOrClose(this float value1, float value2) => value1 > value2 || value1.IsCloseTo(value2);

        /// <summary>
        /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The double to compare to 1. </param>
        public static bool IsOne(this double value) => Math.Abs(value - 1.0) < 10.0 * DBL_EPSILON;

        /// <summary>
        /// IsOne - Returns whether or not the float is "close" to 1.  Same as AreClose(float, 1),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The float to compare to 1. </param>
        public static bool IsOne(this float value) => Math.Abs(value - 1.0f) < 10.0f * FLOAT_EPSILON;

        /// <summary>
        /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The double to compare to 0. </param>
        public static bool IsZero(this double value) => Math.Abs(value) < 10.0 * DBL_EPSILON;

        /// <summary>
        /// IsZero - Returns whether or not the float is "close" to 0.  Same as AreClose(float, 0),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The float to compare to 0. </param>
        public static bool IsZero(this float value) => Math.Abs(value) < 10.0f * FLOAT_EPSILON;
    }
}
