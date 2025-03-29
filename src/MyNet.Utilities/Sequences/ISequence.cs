// -----------------------------------------------------------------------
// <copyright file="ISequence.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Sequences;

public interface ISequence<out T>
{
    T NextValue { get; }

    T CurrentValue { get; }
}
