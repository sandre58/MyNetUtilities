// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using Xunit;

namespace MyNet.Utilities.UnitTests
{
    [UseCulture("en")]
    public class UseCultureTests
    {
        [Fact]
        public void CurrentCultureIsEn() => Assert.Equal("en", CultureInfo.CurrentCulture.Name);
    }
}
