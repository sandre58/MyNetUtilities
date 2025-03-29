// -----------------------------------------------------------------------
// <copyright file="ICloneable.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities;

public interface ICloneable<out T>
{
    T Clone();
}
