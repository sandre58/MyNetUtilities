// -----------------------------------------------------------------------
// <copyright file="RandomGeneratorTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Generator;
using Xunit;

namespace MyNet.Utilities.UnitTests.Generator;

public class RandomGeneratorTests
{
    [Fact]
    public void Number_DefaultMinMax_ReturnsInRange()
    {
        var number = RandomGenerator.Number();
        Assert.InRange(number, 0, int.MaxValue);
    }

    [Fact]
    public void Number_CustomMinMax_ReturnsInRange()
    {
        const int min = 10;
        const int max = 20;
        var number = RandomGenerator.Number(min, max);
        Assert.InRange(number, min, max);
    }

    [Fact]
    public void Number_MaxValueExclusive_ReturnsInExclusiveRange()
    {
        const int min = 0;
        const int max = int.MaxValue;
        var number = RandomGenerator.Number(min, max);
        Assert.InRange(number, min, max - 1);
    }

    [Fact]
    public void Number_MinGreaterThanMax_SwapsMinMax()
    {
        const int min = 20;
        const int max = 10;
        var number = RandomGenerator.Number(min, max);
        Assert.InRange(number, max, min);
    }

    [Fact]
    public void Date_ReturnsDateInRange()
    {
        // Arrange
        var startDate = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var endDate = new DateTime(2022, 12, 31, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var result = RandomGenerator.Date(startDate, endDate);

        // Assert
        Assert.InRange(result, startDate, endDate);
    }

    [Fact]
    public void Date_StartDateGreaterThanEndDate_ReturnsDateInRange()
    {
        // Arrange
        var startDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var endDate = new DateTime(2022, 12, 31, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var result = RandomGenerator.Date(startDate, endDate);

        // Act & Assert
        Assert.InRange(result, endDate, startDate);
    }

    [Fact]
    public void Digits_ReturnsArrayOfSpecifiedCount()
    {
        // Arrange
        const int count = 5;

        // Act
        var result = RandomGenerator.Digits(count);

        // Assert
        Assert.Equal(count, result.Length);
    }

    [Fact]
    public void Digits_ReturnsDigitsInRange()
    {
        // Arrange
        const int count = 5;
        const int minDigit = 0;
        const int maxDigit = 9;

        // Act
        var result = RandomGenerator.Digits(count);

        // Assert
        foreach (var digit in result)
        {
            Assert.InRange(digit, minDigit, maxDigit);
        }
    }

    [Fact]
    public void Even_ReturnsEvenNumberInRange()
    {
        // Arrange
        const int min = 0;
        const int max = 10;

        // Act
        var result = RandomGenerator.Even(min, max);

        // Assert
        Assert.Equal(0, result % 2);
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Odd_ReturnsOddNumberInRange()
    {
        // Arrange
        const int min = 0;
        const int max = 10;

        // Act
        var result = RandomGenerator.Odd(min, max);

        // Assert
        Assert.NotEqual(0, result % 2);
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Double_ReturnsValueInRange()
    {
        // Arrange
        const double min = 0.0d;
        const double max = 10.0d;

        // Act
        var result = RandomGenerator.Double(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Double_ReturnsDefaultRangeValue()
    {
        // Act
        var result = RandomGenerator.Double();

        // Assert
        Assert.InRange(result, 0.0d, 1.0d);
    }

    [Fact]
    public void Decimal_ReturnsValueInRange()
    {
        // Arrange
        const decimal min = 0.0m;
        const decimal max = 10.0m;

        // Act
        var result = RandomGenerator.Decimal(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Decimal_ReturnsDefaultRangeValue()
    {
        // Act
        var result = RandomGenerator.Decimal();

        // Assert
        Assert.InRange(result, 0.0m, 1.0m);
    }

    [Fact]
    public void Float_ReturnsValueInRange()
    {
        // Arrange
        const float min = 0.0f;
        const float max = 10.0f;

        // Act
        var result = RandomGenerator.Float(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Float_ReturnsDefaultRangeValue()
    {
        // Act
        var result = RandomGenerator.Float();

        // Assert
        Assert.InRange(result, 0.0f, 1.0f);
    }

    [Fact]
    public void Byte_ReturnsValueInRange()
    {
        // Arrange
        const int min = byte.MinValue;
        const int max = byte.MaxValue;

        // Act
        var result = RandomGenerator.Byte();

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Byte_ReturnsDefaultRangeValue()
    {
        // Act
        var result = RandomGenerator.Byte();

        // Assert
        Assert.InRange(result, byte.MinValue, byte.MaxValue);
    }

    [Fact]
    public void Bytes_ReturnsArrayWithSpecifiedLength()
    {
        // Arrange
        const int count = 10;

        // Act
        var result = RandomGenerator.Bytes(count);

        // Assert
        Assert.Equal(count, result.Length);
    }

    [Fact]
    public void Bytes_ReturnsRandomValues()
    {
        // Arrange
        const int count = 10;

        // Act
        var result1 = RandomGenerator.Bytes(count);
        var result2 = RandomGenerator.Bytes(count);

        // Assert
        Assert.NotEqual(result1, result2);
    }

    [Fact]
    public void SByte_ReturnsValueWithinRange()
    {
        // Arrange
        const sbyte min = -50;
        const sbyte max = 50;

        // Act
        var result = RandomGenerator.SByte(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void SByte_DefaultRange_ReturnsValueWithinDefaultRange()
    {
        // Arrange
        const sbyte min = sbyte.MinValue;
        const sbyte max = sbyte.MaxValue;

        // Act
        var result = RandomGenerator.SByte();

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Int_ReturnsValueWithinRange()
    {
        // Arrange
        const int min = -100;
        const int max = 100;

        // Act
        var result = RandomGenerator.Int(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Int_DefaultRange_ReturnsValueWithinDefaultRange()
    {
        // Arrange
        const int min = int.MinValue;
        const int max = int.MaxValue;

        // Act
        var result = RandomGenerator.Int();

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void UInt_ReturnsValueWithinRange()
    {
        // Arrange
        const uint min = 0;
        const uint max = 100;

        // Act
        var result = RandomGenerator.UInt(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void UInt_DefaultRange_ReturnsValueWithinDefaultRange()
    {
        // Arrange
        const uint min = uint.MinValue;
        const uint max = uint.MaxValue;

        // Act
        var result = RandomGenerator.UInt();

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void ULong_ReturnsValueWithinRange()
    {
        // Arrange
        const ulong min = 0;
        const ulong max = 100;

        // Act
        var result = RandomGenerator.ULong(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void ULong_DefaultRange_ReturnsValueWithinDefaultRange()
    {
        // Arrange
        const ulong min = ulong.MinValue;
        const ulong max = ulong.MaxValue;

        // Act
        var result = RandomGenerator.ULong();

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Long_ReturnsValueWithinRange()
    {
        // Arrange
        const long min = -1000;
        const long max = 1000;

        // Act
        var result = RandomGenerator.Long(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Long_DefaultRange_ReturnsValueWithinDefaultRange()
    {
        // Arrange
        const long min = long.MinValue;
        const long max = long.MaxValue;

        // Act
        var result = RandomGenerator.Long();

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Short_ReturnsValueWithinRange()
    {
        // Arrange
        const short min = -100;
        const short max = 100;

        // Act
        var result = RandomGenerator.Short(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Short_DefaultRange_ReturnsValueWithinDefaultRange()
    {
        // Arrange
        const short min = short.MinValue;
        const short max = short.MaxValue;

        // Act
        var result = RandomGenerator.Short();

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void UShort_ReturnsValueWithinRange()
    {
        // Arrange
        const ushort min = 0;
        const ushort max = 100;

        // Act
        var result = RandomGenerator.UShort(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void UShort_DefaultRange_ReturnsValueWithinDefaultRange()
    {
        // Arrange
        const ushort min = ushort.MinValue;
        const ushort max = ushort.MaxValue;

        // Act
        var result = RandomGenerator.UShort();

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Char_ReturnsValueWithinRange()
    {
        // Arrange
        const char min = 'A';
        const char max = 'Z';

        // Act
        var result = RandomGenerator.Char(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Char_DefaultRange_ReturnsValueWithinDefaultRange()
    {
        // Arrange
        const char min = char.MinValue;
        const char max = char.MaxValue;

        // Act
        var result = RandomGenerator.Char();

        // Assert
        Assert.InRange(result, min, max);
    }

    [Fact]
    public void Chars_ReturnsArrayOfSpecifiedLength()
    {
        // Arrange
        const int count = 10;

        // Act
        var result = RandomGenerator.Chars(count: count);

        // Assert
        Assert.Equal(count, result.Length);
    }

    [Fact]
    public void Chars_ReturnsArrayOfSpecifiedLengthWithValuesWithinDefaultRange()
    {
        // Arrange
        const int count = 10;
        const char min = char.MinValue;
        const char max = char.MaxValue;

        // Act
        var result = RandomGenerator.Chars(count: count);

        // Assert
        foreach (var c in result)
        {
            Assert.InRange(c, min, max);
        }
    }

    [Fact]
    public void Chars_ReturnsArrayOfSpecifiedLengthWithValuesWithinRange()
    {
        // Arrange
        const int count = 10;
        const char min = 'A';
        const char max = 'Z';

        // Act
        var result = RandomGenerator.Chars(min, max, count);

        // Assert
        foreach (var c in result)
        {
            Assert.InRange(c, min, max);
        }
    }

    [Fact]
    public void String_ReturnsStringWithSpecifiedLength()
    {
        // Arrange
        const int length = 50;

        // Act
        var result = RandomGenerator.String(length);

        // Assert
        Assert.Equal(length, result.Length);
    }

    [Fact]
    public void String_DefaultLength_ReturnsStringWithinDefaultRange()
    {
        // Arrange
        const int minLength = 40;
        const int maxLength = 80;

        // Act
        var result = RandomGenerator.String();

        // Assert
        Assert.InRange(result.Length, minLength, maxLength);
    }

    [Fact]
    public void String_ReturnsStringWithinSpecifiedRange()
    {
        // Arrange
        const int minLength = 5;
        const int maxLength = 10;

        // Act
        var result = RandomGenerator.String(minLength, maxLength);

        // Assert
        Assert.InRange(result.Length, minLength, maxLength);
    }

    [Fact]
    public void String2_ReturnsStringWithSpecifiedLengthAndChars()
    {
        // Arrange
        const int length = 10;
        const string chars = "ABC123";

        // Act
        var result = RandomGenerator.String2(length, chars);

        // Assert
        Assert.Equal(length, result.Length);
        foreach (var c in result)
        {
            Assert.Contains(c, chars);
        }
    }

    [Fact]
    public void String2_ReturnsStringWithinSpecifiedRangeAndChars()
    {
        // Arrange
        const int minLength = 5;
        const int maxLength = 10;
        const string chars = "ABC123";

        // Act
        var result = RandomGenerator.String2(minLength, maxLength, chars);

        // Assert
        Assert.InRange(result.Length, minLength, maxLength);
        foreach (var c in result)
        {
            Assert.Contains(c, chars);
        }
    }

    [Fact]
    public void Bool_ReturnsTrueOrFalse()
    {
        // Act
        var result = RandomGenerator.Bool();

        // Assert
        Assert.True(result || !result);
    }

    [Fact]
    public void PhoneNumber_ReturnsValidPhoneNumber()
    {
        // Act
        var result = RandomGenerator.PhoneNumber();

        // Assert
        Assert.Equal(10, result.Length); // Considering the leading '0' and 9 digits
        Assert.Matches(@"^0[0-9]{9}$", result); // Matches the format 0xxxxxxxxx
    }

    [Fact]
    public void ArrayElement_ReturnsRandomElementFromArray()
    {
        // Arrange
        var array = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = RandomGenerator.ArrayElement(array);

        // Assert
        Assert.Contains(result, array);
    }

    [Fact]
    public void ArrayElements_ReturnsRandomSubsetFromArray()
    {
        // Arrange
        var array = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = RandomGenerator.ArrayElements(array);

        // Assert
        Assert.InRange(result.Length, 0, array.Length);
        foreach (var item in result)
        {
            Assert.Contains(item, array);
        }
    }

    [Fact]
    public void ArrayElements_WithCountGreaterThanArrayLength_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var array = new[] { 1, 2, 3 };

        // Act & Assert
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => RandomGenerator.ArrayElements(array, 4));
    }

    [Fact]
    public void ListItem_ReturnsRandomItemFromList()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        var result = RandomGenerator.ListItem(list);

        // Assert
        Assert.Contains(result, list);
    }

    [Fact]
    public void ListItems_ReturnsRandomSubsetFromList()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        var result = RandomGenerator.ListItems(list);

        // Assert
        Assert.InRange(result.Count, 0, list.Count);
        foreach (var item in result)
        {
            Assert.Contains(item, list);
        }
    }

    [Fact]
    public void CollectionItem_ReturnsRandomItemFromCollection()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        var result = RandomGenerator.CollectionItem((IReadOnlyCollection<int>)collection);

        // Assert
        Assert.Contains(result, collection);
    }

    [Fact]
    public void CollectionItem_WithReadOnlyCollection_ReturnsRandomItemFromCollection()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3, 4, 5 }.AsReadOnly();

        // Act
        var result = RandomGenerator.CollectionItem((IReadOnlyCollection<int>)collection);

        // Assert
        Assert.Contains(result, collection);
    }

    [Fact]
    public void Enum_ReturnsRandomEnumValue()
    {
        // Act
        var result = RandomGenerator.Enum<GenderType>();

        // Assert
        Assert.True(Enum.IsDefined(result));
    }

    [Fact]
    public void Enum_ExcludeValues_ReturnsRandomEnumValueExcludingExcludedValues()
    {
        // Act
        var result = RandomGenerator.Enum(GenderType.Female);

        // Assert
        Assert.NotEqual(GenderType.Female, result);
    }

    [Fact]
    public void Shuffle_ShufflesIEnumerable()
    {
        // Arrange
        var inputList = Enumerable.Range(1, 10).ToList();

        // Act
        var shuffledList = RandomGenerator.Shuffle(inputList).ToList();

        // Assert
        Assert.NotEqual(inputList, shuffledList);
        Assert.Equal(inputList.OrderBy(x => x), shuffledList.OrderBy(x => x));
    }

    [Fact]
    public void Color_ReturnsValidHexColor()
    {
        // Act
        var result = RandomGenerator.Color();

        // Assert
        Assert.Matches(@"^#[0-9a-fA-F]{6}$", result);
    }

    [Fact]
    public void Country_ReturnsRandomCountry()
    {
        // Act
        var result = RandomGenerator.Country();

        // Assert
        Assert.True(result is not null);
    }
}
