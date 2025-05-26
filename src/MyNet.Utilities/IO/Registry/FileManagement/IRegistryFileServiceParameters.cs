// -----------------------------------------------------------------------
// <copyright file="IRegistryFileServiceParameters.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.IO.Registry.FileManagement;

public interface IRegistryFileServiceParameters
{
    int SavedMaxCount { get; set; }

    string BaseRegistry { get; set; }

    string[] SupportedTypes { get; set; }
}
