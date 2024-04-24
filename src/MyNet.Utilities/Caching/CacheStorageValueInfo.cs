// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyNet.Utilities.Caching.Policies;

namespace MyNet.Utilities.Caching
{
    /// <summary>
    /// Value info for the cache storage.
    /// </summary>
    /// <typeparam name="TValue">
    /// The value type.
    /// </typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CacheStorageValueInfo{TValue}" /> class.
    /// </remarks>
    /// <param name="value">The value.</param>
    /// <param name="expirationPolicy">The expiration policy.</param>
    internal class CacheStorageValueInfo<TValue>(TValue value, ExpirationPolicy? expirationPolicy = null)
    {
        #region Fields
        private readonly ExpirationPolicy? _expirationPolicy = expirationPolicy;

        private readonly TValue _value = value;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheStorageValueInfo{TValue}" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expiration">The expiration.</param>
        public CacheStorageValueInfo(TValue value, TimeSpan expiration)
            : this(value, ExpirationPolicy.Duration(expiration))
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public TValue Value
        {
            get
            {
                if (CanExpire && (_expirationPolicy?.CanReset ?? false))
                {
                    _expirationPolicy.Reset();
                }

                return _value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this value can expire.
        /// </summary>
        /// <value><c>true</c> if this value can expire; otherwise, <c>false</c>.</value>
        public bool CanExpire => _expirationPolicy is not null;

        /// <summary>
        /// Gets a value indicating whether this value is expired.
        /// </summary>
        /// <value><c>true</c> if this value is expired; otherwise, <c>false</c>.</value>
        public bool IsExpired => CanExpire && (_expirationPolicy?.IsExpired ?? false);

        /// <summary>
        /// Gets or sets the expiration policy.
        /// </summary>
        internal ExpirationPolicy? ExpirationPolicy => _expirationPolicy;

        #endregion

        #region Methods
        /// <summary>
        /// Dispose value.
        /// </summary>
        public void DisposeValue()
        {
            var disposable = _value as IDisposable;
            disposable?.Dispose();
        }
        #endregion
    }
}
