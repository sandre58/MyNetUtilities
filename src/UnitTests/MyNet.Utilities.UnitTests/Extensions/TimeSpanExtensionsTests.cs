// -----------------------------------------------------------------------
// <copyright file="TimeSpanExtensionsTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyNet.Utilities.Units;
using Xunit;

namespace MyNet.Utilities.UnitTests.Extensions;

public class TimeSpanExtensionsTests
{
    [Theory]
    [InlineData(0, 0, 30, 30, TimeUnit.Minute)]
    [InlineData(0, 5, 0, 5, TimeUnit.Hour)]
    [InlineData(2, 0, 0, 2, TimeUnit.Day)]
    [InlineData(30, 0, 0, 1, TimeUnit.Month)]
    [InlineData(365, 0, 0, 1, TimeUnit.Year)]
    [InlineData(0, 0, 0, 0, TimeUnit.Millisecond)]
    [InlineData(14, 0, 0, 2, TimeUnit.Week)]
    [InlineData(60, 0, 0, 2, TimeUnit.Month)]
    [InlineData(730, 0, 0, 2, TimeUnit.Year)]
    [InlineData(22, 0, 0, 22, TimeUnit.Day)]
    [InlineData(0, 1, 23, 83, TimeUnit.Minute)]
    [InlineData(1, 0, 42, 1482, TimeUnit.Minute)]
    public void Simplify_ShouldReturnExpectedResult(int days, int hours, int minutes, int expectedValue, TimeUnit expectedUnit)
    {
        var (value, unit) = new TimeSpan(days, hours, minutes, 0).Simplify();

        Assert.Equal(expectedValue, value);
        Assert.Equal(expectedUnit, unit);
    }
}
