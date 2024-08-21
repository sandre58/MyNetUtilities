// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using MyNet.Utilities.Globalization;
using Xunit.Sdk;

namespace MyNet.Utilities.UnitTests
{

    /// <summary>
    /// Apply this attribute to your test method to replace the
    /// <see cref="Thread.CurrentThread" /> <see cref="CultureInfo.CurrentCulture" /> and
    /// <see cref="CultureInfo.CurrentUICulture" /> with another culture.
    /// </summary>
    /// <remarks>
    /// Replaces the culture and UI culture of the current thread with
    /// <paramref name="culture" />
    /// </remarks>
    /// <param name="culture">The name of the culture.</param>
    /// <remarks>
    /// <para>
    /// This constructor overload uses <paramref name="culture" /> for both
    /// <see cref="Culture" /> and <see cref="UICulture" />.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class UseCultureAttribute(string culture) : BeforeAfterTestAttribute
    {
        private readonly Lazy<CultureInfo> _culture = new(() => new CultureInfo(culture));
        private CultureInfo? _originalCulture;

        /// <summary>
        /// Gets the culture.
        /// </summary>
        public CultureInfo Culture => _culture.Value;

        /// <summary>
        /// Stores the current <see cref="CultureInfo.CurrentCulture" />
        /// <see cref="CultureInfo.CurrentCulture" /> and <see cref="CultureInfo.CurrentUICulture" />
        /// and replaces them with the new cultures defined in the constructor.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public override void Before(MethodInfo methodUnderTest)
        {
            _originalCulture = CultureInfo.CurrentCulture;

            GlobalizationService.Current.SetCulture(Culture);
        }

        /// <summary>
        /// Restores the original <see cref="CultureInfo.CurrentCulture" /> and
        /// <see cref="CultureInfo.CurrentUICulture" /> to <see cref="CultureInfo.CurrentCulture" />
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public override void After(MethodInfo methodUnderTest)
        {
            if (_originalCulture == null) return;

            GlobalizationService.Current.SetCulture(_originalCulture);
        }
    }
}
