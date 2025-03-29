// -----------------------------------------------------------------------
// <copyright file="IDirectoryService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.IO;

public interface IDirectoryService
{
    string RootDirectory { get; }

    string CreateSubDirectory(string name);

    string CreateFile(string? fileExtension = null, string? preferredFileName = null);

    string GetFileName(string? fileExtension = null, string? preferredFileName = null);

    void Clean();

    void Delete();
}
