// -----------------------------------------------------------------------
// <copyright file="RegistryFile.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.IO.Registry.FileManagement;

public class RegistryFile
{
    public string Path { get; set; } = string.Empty;

    public long LastAccessDate { get; set; }
}
