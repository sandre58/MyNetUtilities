// -----------------------------------------------------------------------
// <copyright file="NumberExtensionsTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Units;
using Xunit;

namespace MyNet.Utilities.UnitTests.Extensions;

public class NumberExtensionsTests
{
    [Theory]
    [InlineData(1024, FileSizeUnit.Byte, 1, FileSizeUnit.Kilobyte)]
    [InlineData(1048576, FileSizeUnit.Byte, 1, FileSizeUnit.Megabyte)]
    [InlineData(1073741824, FileSizeUnit.Byte, 1, FileSizeUnit.Gigabyte)]
    [InlineData(1099511627776, FileSizeUnit.Byte, 1, FileSizeUnit.Terabyte)]
    [InlineData(244587587, FileSizeUnit.Byte, 238855.0654296875, FileSizeUnit.Kilobyte)]
    [InlineData(244587587, FileSizeUnit.Byte, 233.2568998336792, FileSizeUnit.Megabyte)]
    [InlineData(244587587, FileSizeUnit.Byte, 0.22778994124382734, FileSizeUnit.Gigabyte)]
    [InlineData(244587587, FileSizeUnit.Byte, 0.00022245111449592514, FileSizeUnit.Terabyte)]
    [InlineData(21454545, FileSizeUnit.Kilobyte, 21969454080, FileSizeUnit.Byte)]
    [InlineData(21454545, FileSizeUnit.Kilobyte, 20951.7041015625, FileSizeUnit.Megabyte)]
    [InlineData(21454545, FileSizeUnit.Kilobyte, 20.46064853668213, FileSizeUnit.Gigabyte)]
    [InlineData(21454545, FileSizeUnit.Kilobyte, 0.01998110208660364, FileSizeUnit.Terabyte)]
    [InlineData(124124, FileSizeUnit.Megabyte, 130153447424, FileSizeUnit.Byte)]
    [InlineData(124124, FileSizeUnit.Megabyte, 127102976, FileSizeUnit.Kilobyte)]
    [InlineData(124124, FileSizeUnit.Megabyte, 121.21484375, FileSizeUnit.Gigabyte)]
    [InlineData(124124, FileSizeUnit.Megabyte, 0.11837387084960938, FileSizeUnit.Terabyte)]
    [InlineData(14212, FileSizeUnit.Gigabyte, 15260018802688, FileSizeUnit.Byte)]
    [InlineData(14212, FileSizeUnit.Gigabyte, 14902362112, FileSizeUnit.Kilobyte)]
    [InlineData(14212, FileSizeUnit.Gigabyte, 14553088, FileSizeUnit.Megabyte)]
    [InlineData(14212, FileSizeUnit.Gigabyte, 13.87890625, FileSizeUnit.Terabyte)]
    public void DoubleToFileSize(double from, FileSizeUnit fromUnit, double result, FileSizeUnit toUnit) => Assert.Equal(result, from.To(fromUnit, toUnit));

    [Fact]
    public void IntToTens()
    {
        const int number = 1;
        Assert.Equal(10, number.Tens());
    }

    [Fact]
    public void UintToTens()
    {
        const uint number = 1;
        Assert.Equal(10U, number.Tens());
    }

    [Fact]
    public void LongToTens()
    {
        const long number = 1;
        Assert.Equal(10L, number.Tens());
    }

    [Fact]
    public void UlongToTens()
    {
        const ulong number = 1;
        Assert.Equal(10UL, number.Tens());
    }

    [Fact]
    public void DoubleToTens()
    {
        const double number = 1;
        Assert.Equal(10d, number.Tens());
    }

    [Fact]
    public void IntToHundreds()
    {
        const int number = 2;
        Assert.Equal(200, number.Hundreds());
    }

    [Fact]
    public void UintToHundreds()
    {
        const uint number = 2;
        Assert.Equal(200U, number.Hundreds());
    }

    [Fact]
    public void LongToHundreds()
    {
        const long number = 2;
        Assert.Equal(200L, number.Hundreds());
    }

    [Fact]
    public void UlongToHundreds()
    {
        const ulong number = 2;
        Assert.Equal(200UL, number.Hundreds());
    }

    [Fact]
    public void DoubleToHundreds()
    {
        const double number = 2;
        Assert.Equal(200d, number.Hundreds());
    }

    [Fact]
    public void IntToThousands()
    {
        const int number = 3;
        Assert.Equal(3000, number.Thousands());
    }

    [Fact]
    public void UintToThousands()
    {
        const uint number = 3;
        Assert.Equal(3000U, number.Thousands());
    }

    [Fact]
    public void LongToThousands()
    {
        const long number = 3;
        Assert.Equal(3000L, number.Thousands());
    }

    [Fact]
    public void UlongToThousands()
    {
        const ulong number = 3;
        Assert.Equal(3000UL, number.Thousands());
    }

    [Fact]
    public void DoubleToThousands()
    {
        const double number = 3;
        Assert.Equal(3000d, number.Thousands());
    }

    [Fact]
    public void IntToMillions()
    {
        const int number = 4;
        Assert.Equal(4000000, number.Millions());
    }

    [Fact]
    public void UintToMillions()
    {
        const uint number = 4;
        Assert.Equal(4000000U, number.Millions());
    }

    [Fact]
    public void LongToMillions()
    {
        const long number = 4;
        Assert.Equal(4000000L, number.Millions());
    }

    [Fact]
    public void UlongToMillions()
    {
        const ulong number = 4;
        Assert.Equal(4000000UL, number.Millions());
    }

    [Fact]
    public void DoubleToMillions()
    {
        const double number = 4;
        Assert.Equal(4000000d, number.Millions());
    }

    [Fact]
    public void IntToBillions()
    {
        const int number = 1;
        Assert.Equal(1000000000, number.Billions());
    }

    [Fact]
    public void UintToBillions()
    {
        const uint number = 1;
        Assert.Equal(1000000000U, number.Billions());
    }

    [Fact]
    public void LongToBillions()
    {
        const long number = 1;
        Assert.Equal(1000000000L, number.Billions());
    }

    [Fact]
    public void UlongToBillions()
    {
        const ulong number = 1;
        Assert.Equal(1000000000UL, number.Billions());
    }

    [Fact]
    public void DoubleToBillions()
    {
        const double number = 1;
        Assert.Equal(1000000000d, number.Billions());
    }
}
