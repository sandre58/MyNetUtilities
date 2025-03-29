// -----------------------------------------------------------------------
// <copyright file="ExpiringEventArgs.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyNet.Utilities.Caching.Policies;

namespace MyNet.Utilities.Caching;

/// <summary>
/// The expiring event args.
/// </summary>
/// <typeparam name="TKey">The key type.</typeparam>
/// <typeparam name="TValue">The value type.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="ExpiringEventArgs{TKey, TValue}" /> class.
/// </remarks>
/// <param name="key">The key.</param>
/// <param name="value">The value.</param>
/// <param name="expirationPolicy">The expiration policy.</param>
public class ExpiringEventArgs<TKey, TValue>(TKey key, TValue value, ExpirationPolicy? expirationPolicy) : EventArgs
{
    /// <summary>
    /// Gets or sets a value indicating whether the expiration of value should be canceled and the value should stay in cache.
    /// </summary>
    /// <value><c>true</c> if cancelled; otherwise, <c>false</c>.</value>
    public bool Cancel { get; set; }

    /// <summary>
    /// Gets or sets the expiration policy.
    /// </summary>
    public ExpirationPolicy? ExpirationPolicy { get; set; } = expirationPolicy;

    /// <summary>
    /// Gets the key.
    /// </summary>
    /// <value>The key.</value>
    public TKey Key { get; private set; } = key;

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public TValue Value { get; private set; } = value;
}
