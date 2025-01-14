// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using MyNet.Utilities.Generator;
using MyNet.Utilities.Helpers;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public static class StringExtensions
    {
        private const string RelativeUriSeparator = "/";
        private const string WebUriSeparator = "&";
        private const char Separator = ';';

        public static bool ContainsAny(this string? value, params string[] strings) => value is not null && strings.Any(x => value.Contains(x, StringComparison.OrdinalIgnoreCase));

        public static bool NotContainsAny(this string? value, params string[] strings) => value is null || strings.All(x => !value.Contains(x, StringComparison.OrdinalIgnoreCase));

        public static string Random(this string source, char separator = Separator) => RandomGenerator.ListItem(source.Split(separator)).Trim();

        /// <summary>
        /// Extension method to format string with passed arguments. Current thread's current culture is used
        /// </summary>
        /// <param name="format">string format</param>
        /// <param name="args">arguments</param>
        /// <returns></returns>
        public static string FormatWith(this string format, params object?[] args) => string.Format(CultureInfo.CurrentCulture, format, args);

        /// <summary>
        /// Extension method to format string with passed arguments using specified format provider (i.e. CultureInfo)
        /// </summary>
        /// <param name="format">string format</param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <param name="args">arguments</param>
        /// <returns></returns>
        public static string FormatWith(this string format, IFormatProvider provider, params object?[] args) => string.Format(provider, format, args);

        /// <summary>
        /// Extension method to format string with passed arguments. Current thread's current culture is used
        /// </summary>
        /// <param name="format">string format</param>
        /// <param name="args">arguments</param>
        /// <returns></returns>
        public static string InvariantFormatWith(this string format, params object?[] args) => string.Format(CultureInfo.InvariantCulture, format, args);

        public static string Increment(this string defaultName, IEnumerable<string> existStrings, int minIncrement = 1, int step = 1, string? format = null)
        {
            var inc = minIncrement;
            while (true)
            {
                var newName = defaultName + inc.ToString(format);
                if (!existStrings.Contains(newName))
                    return newName;

                inc += step;
            }
        }

        public static string GetInitials(this string value)
              => string.Concat(value
                 .Split([' '], StringSplitOptions.RemoveEmptyEntries)
                 .Where(x => x.Length >= 1)
                 .Select(x => double.TryParse(x, out var value) ? value.ToString() : char.ToUpper(x[0]).ToString()));

        public static string IncrementAlpha(this string defaultName, IEnumerable<string> existStrings, int minIncrement = 1, int step = 1)
        {
            var inc = minIncrement;
            while (inc < 26)
            {
                var newName = $"{defaultName}{CharHelper.GetAlphabet()[(inc - 1) % 25].ToString().ToUpper(CultureInfo.CurrentCulture)}";
                if (!existStrings.Contains(newName))
                    return newName;

                inc += step;
            }

            return defaultName;
        }

        public static Version ToVersion(this string value) => new(value);

        public static Uri ToRelativeUri(this string baseStr, params string?[] parameters)
        {
            var paramsStr = parameters != null ? string.Join(RelativeUriSeparator, parameters) : string.Empty;
            return new Uri(baseStr + (string.IsNullOrEmpty(paramsStr) ? string.Empty : RelativeUriSeparator + paramsStr), UriKind.Relative);
        }

        public static Uri ToWebUri(this string baseStr, params (string key, string value)[] parameters)
        {
            var paramsStr = parameters != null ? "?" + string.Join(WebUriSeparator, parameters.Select(x => $"{x.key}={x.value}")) : string.Empty;
            return new Uri(baseStr + (string.IsNullOrEmpty(paramsStr) ? string.Empty : RelativeUriSeparator + paramsStr), UriKind.Absolute);
        }

        public static string? ToFilename(this string filename, string replacement = "_")
        {
            if (string.IsNullOrEmpty(filename)) return filename;

            var invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(filename, invalidRegStr, replacement);
        }

        public static SecureString ToSecureString(this string input)
        {
            var secure = new SecureString();
            foreach (var c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(this SecureString input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            var returnValue = string.Empty;
            var ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }
    }
}
