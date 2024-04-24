// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MyNet.Utilities.Units;

namespace MyNet.Utilities
{
    /// <summary>
    /// Number to Number extensions
    /// </summary>
    public static class NumberExtensions
    {
        private static readonly Dictionary<Type, Func<object, Enum, Enum, double>> ConvertMethods = new()
        {
            {
                typeof(FileSizeUnit),
                (value, fromUnit, toUnit) =>
                {
                    var pow = (int)(object)toUnit - (int)(object)fromUnit;

                    return pow switch
                    {
                        < 0 => (double)value * Math.Pow(1024, Math.Abs(pow)),
                        > 0 => (double)value / Math.Pow(1024, pow),
                        _ => (double)value,
                    };
                }
            },
            { typeof(MetricUnit), (value, fromUnit, toUnit) => (double)value * Math.Pow(10, (int)(object)toUnit - (int)(object)fromUnit) },
            { typeof(LengthUnit), (value, fromUnit, toUnit) => (double)value * Math.Pow(10, (int)(object)toUnit - (int)(object)fromUnit) },
            { typeof(MassUnit), (value, fromUnit, toUnit) => (double)value * Math.Pow(10, (int)(object)toUnit - (int)(object)fromUnit) }
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double To<T, TUnit>(this T value, TUnit fromUnit, TUnit toUnit)
            where T : struct, IComparable<T>
            where TUnit : Enum
            => To(value, fromUnit.GetType(), fromUnit, toUnit);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double To<T, TUnit>(this T value, TUnit toUnit)
            where T : struct, IComparable<T>
            where TUnit : Enum
            => To(value, default!, toUnit);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double To<T>(this T value, Type type, Enum fromUnit, Enum toUnit)
            where T : struct, IComparable<T>
            => ConvertMethods.TryGetValue(type, out var convertMethod)
            ? convertMethod.Invoke(value, fromUnit, toUnit)
            : throw new NotImplementedException();

        public static (double newValue, TUnit newUnit) Simplify<T, TUnit>(this T value, TUnit unit, TUnit? minUnit = default, TUnit? maxUnit = default)
            where T : struct, IComparable<T>
            where TUnit : Enum
            => Simplify(value, unit, minUnit, maxUnit);

        public static (double newValue, Enum newUnit) Simplify<T>(this T value, Type type, Enum unit, Enum? minUnit = null, Enum? maxUnit = null)
            where T : struct, IComparable<T>
        {
            var newUnit = unit;
            double? newValue = null;
            var results = new Dictionary<Enum, double>();

            foreach (var enumValue in Enum.GetValues(type))
            {
                if (enumValue is not null && (minUnit is null || (int)enumValue >= (int)(object)minUnit) && (maxUnit is null || (int)enumValue <= (int)(object)maxUnit))
                    results.Add((Enum)enumValue, value.To(type, unit, (Enum)enumValue));
            }

            if (results.Any(x => !x.Value.NearlyEqual(0)))
            {
                var orderedResults = results.OrderBy(x => x.Value);
                var item = orderedResults.Any(x => x.Value >= 1) ? orderedResults.FirstOrDefault(x => x.Value >= 1) : orderedResults.LastOrDefault();
                newValue = item.Value;
                newUnit = item.Key;
            }

            return (newValue ?? (double)(object)value, newUnit);
        }

        /// <summary>
        /// 5.Tens == 50
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Tens(this int input) => input * 10;

        /// <summary>
        /// 5.Tens == 50
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Tens(this uint input) => input * 10;

        /// <summary>
        /// 5.Tens == 50
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Tens(this long input) => input * 10;

        /// <summary>
        /// 5.Tens == 50
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Tens(this ulong input) => input * 10;

        /// <summary>
        /// 5.Tens == 50
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Tens(this double input) => input * 10;

        /// <summary>
        /// 4.Hundreds() == 400
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Hundreds(this int input) => input * 100;

        /// <summary>
        /// 4.Hundreds() == 400
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Hundreds(this uint input) => input * 100;

        /// <summary>
        /// 4.Hundreds() == 400
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Hundreds(this long input) => input * 100;

        /// <summary>
        /// 4.Hundreds() == 400
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Hundreds(this ulong input) => input * 100;

        /// <summary>
        /// 4.Hundreds() == 400
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Hundreds(this double input) => input * 100;

        /// <summary>
        /// 3.Thousands() == 3000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Thousands(this int input) => input * 1000;

        /// <summary>
        /// 3.Thousands() == 3000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Thousands(this uint input) => input * 1000;

        /// <summary>
        /// 3.Thousands() == 3000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Thousands(this long input) => input * 1000;

        /// <summary>
        /// 3.Thousands() == 3000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Thousands(this ulong input) => input * 1000;

        /// <summary>
        /// 3.Thousands() == 3000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Thousands(this double input) => input * 1000;

        /// <summary>
        /// 2.Millions() == 2000000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Millions(this int input) => input * 1000000;

        /// <summary>
        /// 2.Millions() == 2000000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Millions(this uint input) => input * 1000000;

        /// <summary>
        /// 2.Millions() == 2000000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Millions(this long input) => input * 1000000;

        /// <summary>
        /// 2.Millions() == 2000000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Millions(this ulong input) => input * 1000000;

        /// <summary>
        /// 2.Millions() == 2000000
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Millions(this double input) => input * 1000000;

        /// <summary>
        /// 1.Billions() == 1000000000 (short scale)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Billions(this int input) => input * 1000000000;

        /// <summary>
        /// 1.Billions() == 1000000000 (short scale)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Billions(this uint input) => input * 1000000000;

        /// <summary>
        /// 1.Billions() == 1000000000 (short scale)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Billions(this long input) => input * 1000000000;

        /// <summary>
        /// 1.Billions() == 1000000000 (short scale)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Billions(this ulong input) => input * 1000000000;

        /// <summary>
        /// 1.Billions() == 1000000000 (short scale)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Billions(this double input) => input * 1000000000;
    }
}
