﻿// -----------------------------------------------------------------------
// <copyright file="StringExtensionsTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using Xunit;

namespace MyNet.Utilities.UnitTests.Extensions;

public class StringExtensionsTests
{
    private const string Format = "This is a format with three numbers: {0}-{1}-{2}.";
    private const string Expected = "This is a format with three numbers: 1-2-3.";

    [Fact]
    public void CanFormatStringWithExactNumberOfArguments() => Assert.Equal(Expected, Format.FormatWith(1, 2, 3));

    [Fact]
    public void CanFormatStringWithMoreArguments() => Assert.Equal(Expected, Format.FormatWith(1, 2, 3, 4, 5));

    [Fact]
    public void CannotFormatStringWithLessArguments() => Assert.Throws<FormatException>(() => Format.FormatWith(1, 2));

    [Theory]
    [InlineData("en-US", "6,666.66")]
    [InlineData("ru-RU", "6 666,66")]
    public void CanSpecifyCultureExplicitly(string culture, string expected) => Assert.Equal(expected, "{0:N2}".FormatWith(new CultureInfo(culture), 6666.66));
}
