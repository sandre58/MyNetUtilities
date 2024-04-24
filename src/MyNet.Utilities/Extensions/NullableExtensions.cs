// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities
{
    public static class NullableExtensions
    {
        public static bool IsTrue(this bool? boolean) => boolean.HasValue && boolean.Value;

        public static bool IsFalse(this bool? boolean) => !boolean.HasValue || !boolean.Value;

        public static T OrThrow<T>(this T? value) => value is null ? throw new ArgumentNullException(nameof(value)) : value;

        public static string OrEmpty(this string? value) => value is null ? string.Empty : value;

        public static string Or(this string? value, string placeholder) => string.IsNullOrWhiteSpace(value) ? placeholder : value!;

        public static void IfNull<T>(this T? argument, Action method, Action<T>? elseMethod = null)
        {
            if (argument is null)
            {
                method.Invoke();
            }
            else
                elseMethod?.Invoke(argument);

        }
        public static void IfNotNull<T>(this T? argument, Action<T> method, Action? elseMethod = null)
        {
            if (argument is not null)
            {
                method.Invoke(argument);
            }
            else
                elseMethod?.Invoke();
        }

        public static void IfIs<T>(this object argument, Action<T> method)
        {
            if (argument is T argTyped)
            {
                method.Invoke(argTyped);
            }
        }

        public static void IfTrue(this bool argument, Action method, Action? elseMethod = null)
        {
            if (argument)
            {
                method.Invoke();
            }
            else
                elseMethod?.Invoke();
        }

        public static void IfFalse(this bool argument, Action method, Action? elseMethod = null)
        {
            if (!argument)
            {
                method.Invoke();
            }
            else
                elseMethod?.Invoke();
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> enumerable) => enumerable.Where(e => e is not null).Select(e => e!);

        public static IEnumerable<string> NotNullOrEmpty(this IEnumerable<string?> enumerable) => enumerable.Where(e => !string.IsNullOrWhiteSpace(e)).Select(e => e!);
    }
}
