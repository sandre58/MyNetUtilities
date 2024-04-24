// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MyNet.Utilities.Exceptions;

namespace MyNet.Utilities.Extensions
{
    public static partial class ValidationExtensions
    {
        public static T IsRequiredOrThrow<T>(this T value, [CallerMemberName] string propertyName = null!)
            => string.IsNullOrEmpty(value?.ToString()) ? throw new NullOrEmptyException(propertyName) : value;

        public static bool IsLowerThan<T>(this T value, T target) where T : IComparable => value.CompareTo(target) < 0;

        public static T IsLowerThanOrThrow<T>(this T value, T target, [CallerMemberName] string propertyName = null!)
            where T : IComparable
            => value.IsLowerThan(target) ? value : throw new IsNotLowerOrEqualsThanException(propertyName, target);

        public static bool IsUpperThan<T>(this T value, T target) where T : IComparable => value.CompareTo(target) > 0;

        public static T IsUpperThanOrThrow<T>(this T value, T target, [CallerMemberName] string propertyName = null!)
            where T : IComparable
            => value.IsUpperThan(target) ? value : throw new IsNotUpperOrEqualsThanException(propertyName, target);

        public static bool IsLowerOrEqualThan<T>(this T value, T target) where T : IComparable => value.CompareTo(target) <= 0;

        public static T IsLowerOrEqualThanOrThrow<T>(this T value, T target, [CallerMemberName] string propertyName = null!)
            where T : IComparable
            => value.IsLowerOrEqualThan(target) ? value : throw new IsNotLowerOrEqualsThanException(propertyName, target);

        public static bool IsUpperOrEqualThan<T>(this T value, T target) where T : IComparable => value.CompareTo(target) >= 0;

        public static T IsUpperOrEqualThanOrThrow<T>(this T value, T target, [CallerMemberName] string propertyName = null!)
            where T : IComparable
            => value.IsUpperOrEqualThan(target) ? value : throw new IsNotUpperOrEqualsThanException(propertyName, target);

        public static DateTime IsInPastOrThrow(this DateTime value, [CallerMemberName] string propertyName = null!)
            => value.IsInPast() ? value : throw new FutureDateException(propertyName);

        public static DateTime? IsInPastOrThrow(this DateTime? value, [CallerMemberName] string propertyName = null!)
            => value.HasValue ? value.Value.IsInPastOrThrow(propertyName) : value;

        public static bool IsEmailAddress(this string value)
        {
            var regex = EmailRegex();
            return regex.Match(value).Length > 0;
        }

        public static string IsEmailAddressOrThrow(this string value, [CallerMemberName] string propertyName = null!)
            => value.IsEmailAddress() ? value : throw new InvalidEmailAddressException(propertyName);

        public static bool IsPhoneNumber(this string value, [CallerMemberName] string propertyName = null!)
        {
            var regex = PhoneRegex();

            return regex.Match(value).Length > 0;
        }

        public static string IsPhoneNumberOrThrow(this string value, [CallerMemberName] string propertyName = null!)
            => value.IsPhoneNumber() ? value : throw new InvalidPhoneException(propertyName);

        [GeneratedRegex("^((([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+(\\.([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+)*)|((\\x22)((((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(([\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x7f]|\\x21|[\\x23-\\x5b]|[\\x5d-\\x7e]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(\\\\([\\x01-\\x09\\x0b\\x0c\\x0d-\\x7f]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF]))))*(((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(\\x22)))@((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
        private static partial Regex EmailRegex();

        [GeneratedRegex("^(\\+\\s?)?((?<!\\+.*)\\(\\+?\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+)([\\s\\-\\.]?(\\(\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+))*(\\s?(x|ext\\.?)\\s?\\d+)?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
        private static partial Regex PhoneRegex();
    }
}
