// -----------------------------------------------------------------------
// <copyright file="RegistryFileServiceParameter.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.IO.Registry.FileManagement;

public class RegistryFileServiceParameter(string baseRegistry) : IRegistryFileServiceParameters
{
    public int SavedMaxCount { get; set; }

    public string BaseRegistry { get; set; } = baseRegistry;

    public string[] SupportedTypes { get; set; } = [];
}
