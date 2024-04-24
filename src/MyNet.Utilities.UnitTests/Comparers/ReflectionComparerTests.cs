// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using MyNet.Utilities.Comparers;
using Xunit;

namespace MyNet.Utilities.UnitTests.Comparers
{
    public class ReflectionComparerTests
    {
        [Fact]
        public void Compare_ObjectInterfaceImplementation_NullValues_ReturnsZero()
        {
            // Arrange
            var sortDescriptions = new List<ReflectionSortDescription>();
            var comparer = new ReflectionComparer<object>(sortDescriptions);

            // Act
            int result = comparer.Compare(null, null);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Compare_ObjectInterfaceImplementation_OneValueIsNull_ReturnsOne()
        {
            // Arrange
            var sortDescriptions = new List<ReflectionSortDescription>()
            {
                new("Length") // Assuming "Length" property exists in string class
            };
            var comparer = new ReflectionComparer<object>(sortDescriptions);

            // Act
            int result = comparer.Compare("test", null);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Compare_ObjectInterfaceImplementation_BothValuesAreNotNull_ReturnsComparisonResult()
        {
            // Arrange
            var sortDescriptions = new List<ReflectionSortDescription>
            {
                new("Length") // Assuming "Length" property exists in string class
            };
            var comparer = new ReflectionComparer<string>(sortDescriptions);

            // Act
            int result = comparer.Compare("test", "abc");

            // Assert
            Assert.True(result > 0); // "test" is longer than "abcd"
        }

        [Fact]
        public void Compare_GenericInterfaceImplementation_NullValues_ReturnsZero()
        {
            // Arrange
            var sortDescriptions = new List<ReflectionSortDescription>()
            {
                new("Length") // Assuming "Length" property exists in string class
            };
            var comparer = new ReflectionComparer<string>(sortDescriptions);

            // Act
            int result = comparer.Compare(null, null);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Compare_GenericInterfaceImplementation_OneValueIsNull_ReturnsOne()
        {
            // Arrange
            var sortDescriptions = new List<ReflectionSortDescription>()
            {
                new("Length") // Assuming "Length" property exists in string class
            };
            var comparer = new ReflectionComparer<string>(sortDescriptions);

            // Act
            int result = comparer.Compare("test", null);

            // Assert
            Assert.Equal(1, result);
        }
    }
}
