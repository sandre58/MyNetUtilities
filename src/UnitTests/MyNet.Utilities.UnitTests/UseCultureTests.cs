// -----------------------------------------------------------------------
// <copyright file="UseCultureTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using Xunit;

namespace MyNet.Utilities.UnitTests;

[UseCulture("en")]
public class UseCultureTests
{
    [Fact]
    public void CurrentCultureIsEn() => Assert.Equal("en", CultureInfo.CurrentCulture.Name);
}
