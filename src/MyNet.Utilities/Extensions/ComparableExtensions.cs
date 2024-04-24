// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyNet.Utilities.Comparaison;
using MyNet.Utilities.Comparers;

namespace MyNet.Utilities
{
    public static class ComparableExtensions
    {
        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool Compare(this IComparable x, IComparable y, ComparableOperator sign)
        {
            if (x == null || y == null)
                return false;

            var compare = x.CompareTo(y);

            return sign switch
            {
                ComparableOperator.EqualsTo => compare == 0,
                ComparableOperator.NotEqualsTo => compare != 0,
                ComparableOperator.LessThan => compare < 0,
                ComparableOperator.GreaterThan => compare > 0,
                ComparableOperator.LessEqualThan => compare <= 0,
                ComparableOperator.GreaterEqualThan => compare >= 0,
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Compares three object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool Compare(this IComparable x, IComparable from, IComparable to, ComplexComparableOperator sign)
        {
            if (x == null || from == null || to == null)
                return false;

            var compareFrom = x.CompareTo(from);
            var compareTo = x.CompareTo(to);

            var result = compareFrom >= 0 && compareTo <= 0;

            return sign switch
            {
                ComplexComparableOperator.IsBetween => result,
                ComplexComparableOperator.IsNotBetween => !result,
                ComplexComparableOperator.EqualsTo => compareFrom == 0,
                ComplexComparableOperator.NotEqualsTo => compareFrom != 0,
                ComplexComparableOperator.LessThan => compareTo < 0,
                ComplexComparableOperator.GreaterThan => compareFrom > 0,
                ComplexComparableOperator.LessEqualThan => compareTo <= 0,
                ComplexComparableOperator.GreaterEqualThan => compareFrom >= 0,
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Compares three object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool Compare<T>(this IComparable<T> x, T? from, T? to, ComplexComparableOperator sign) where T : struct, IComparable<T>
        {
            var compareFrom = x.CompareTo(from);
            var compareTo = x.CompareTo(to);

            var result = compareFrom >= 0 && compareTo <= 0;

            return sign switch
            {
                ComplexComparableOperator.IsBetween => result,
                ComplexComparableOperator.IsNotBetween => !result,
                ComplexComparableOperator.EqualsTo => compareFrom == 0,
                ComplexComparableOperator.NotEqualsTo => compareFrom != 0,
                ComplexComparableOperator.LessThan => compareTo < 0,
                ComplexComparableOperator.GreaterThan => compareFrom > 0,
                ComplexComparableOperator.LessEqualThan => compareTo <= 0,
                ComplexComparableOperator.GreaterEqualThan => compareFrom >= 0,
                _ => throw new NotImplementedException(),
            };
        }

        public static int CompareTo<T>(this IComparable<T>? obj1, T obj2) where T : struct, IComparable<T> => new NullableComparer<T>().Compare(obj1, obj2);

        public static int CompareTo<T>(this IComparable<T>? obj1, T? obj2) where T : struct, IComparable<T> => new NullableComparer<T>().Compare(obj1, obj2);

    }
}
