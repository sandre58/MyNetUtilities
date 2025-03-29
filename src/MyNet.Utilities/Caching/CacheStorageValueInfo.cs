// -----------------------------------------------------------------------
// <copyright file="CacheStorageValueInfo.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyNet.Utilities.Caching.Policies;

namespace MyNet.Utilities.Caching;

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
internal sealed class CacheStorageValueInfo<TValue>(TValue value, ExpirationPolicy? expirationPolicy = null)
{
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
            if (CanExpire && (ExpirationPolicy?.CanReset ?? false))
            {
                ExpirationPolicy.Reset();
            }

            return value;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this value can expire.
    /// </summary>
    /// <value><c>true</c> if this value can expire; otherwise, <c>false</c>.</value>
    public bool CanExpire => ExpirationPolicy is not null;

    /// <summary>
    /// Gets a value indicating whether this value is expired.
    /// </summary>
    /// <value><c>true</c> if this value is expired; otherwise, <c>false</c>.</value>
    public bool IsExpired => CanExpire && (ExpirationPolicy?.IsExpired ?? false);

    /// <summary>
    /// Gets the expiration policy.
    /// </summary>
    internal ExpirationPolicy? ExpirationPolicy { get; } = expirationPolicy;

    #endregion

    #region Methods

    /// <summary>
    /// Dispose value.
    /// </summary>
    public void DisposeValue()
    {
        var disposable = value as IDisposable;
        disposable?.Dispose();
    }

    #endregion
}
