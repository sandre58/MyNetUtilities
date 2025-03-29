// -----------------------------------------------------------------------
// <copyright file="IWrapper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities;

public interface IWrapper<out T>
{
    T Item { get; }
}
