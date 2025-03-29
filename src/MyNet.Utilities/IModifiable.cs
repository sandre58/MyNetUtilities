// -----------------------------------------------------------------------
// <copyright file="IModifiable.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities;

public interface IModifiable
{
    void ResetIsModified();

    bool IsModified();
}
