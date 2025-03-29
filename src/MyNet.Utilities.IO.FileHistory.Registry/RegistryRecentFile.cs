// -----------------------------------------------------------------------
// <copyright file="RegistryRecentFile.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.IO.Registry.FileManagement;

namespace MyNet.Utilities.IO.FileHistory.Registry;

internal sealed class RegistryRecentFile : RegistryFile
{
    public string? Name { get; set; }

    public bool IsPinned { get; set; }

    public bool IsRecoveredFile { get; set; }
}
