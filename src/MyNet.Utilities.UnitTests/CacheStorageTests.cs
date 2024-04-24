// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Caching;
using MyNet.Utilities.Caching.Policies;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyNet.Utilities.UnitTests
{
    public class CacheStorageTests
    {
        [Fact]
        public void GetFromCacheOrFetch_WithExistingKey_ReturnsCachedValue()
        {
            // Arrange
            var cache = new CacheStorage<string, int>();
            var key = "test";
            var valueToAdd = 42;
            cache.Add(key, valueToAdd);

            // Act
            var retrievedValue = cache.GetFromCacheOrFetch(key, () => throw new InvalidOperationException("Should not be called"));

            // Assert
            Assert.Equal(valueToAdd, retrievedValue);
        }

        [Fact]
        public void GetFromCacheOrFetch_WithNonExistingKey_CallsCodeAndCachesValue()
        {
            // Arrange
            var cache = new CacheStorage<string, int>();
            var key = "test";
            var valueToAdd = 42;

            // Act
            var retrievedValue = cache.GetFromCacheOrFetch(key, () => valueToAdd);

            // Assert
            Assert.Equal(valueToAdd, retrievedValue);
            Assert.Equal(valueToAdd, cache[key]);
        }

        [Fact]
        public async Task GetFromCacheOrFetch_WithExpirationPolicy_CachesValueWithExpirationAsync()
        {
            // Arrange
            var cache = new CacheStorage<string, int>();
            var key = "test";
            var valueToAdd = 42;
            var expirationPolicy = ExpirationPolicy.Duration(TimeSpan.FromSeconds(1));

            // Act
            var retrievedValue = cache.GetFromCacheOrFetch(key, () => valueToAdd, expirationPolicy);

            // Assert
            Assert.Equal(valueToAdd, retrievedValue);
            Assert.Equal(valueToAdd, cache[key]);

            // Wait for expiration
            await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(true);
            Assert.False(cache.Contains(key), "Cache should not contain expired item");
        }
    }
}
