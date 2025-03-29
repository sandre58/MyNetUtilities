// -----------------------------------------------------------------------
// <copyright file="DirectoryService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using MyNet.Utilities.Helpers;
using MyNet.Utilities.Logging;

namespace MyNet.Utilities.IO;

public class DirectoryService : IDirectoryService
{
    private readonly List<string> _listOfDirectories = [];

    private string _rootDirectory = null!;

    public DirectoryService(string root) => RootDirectory = root;

    public string RootDirectory
    {
        get => _rootDirectory;
        set
        {
            var newRoot = GetOrMakeRootDirectory(value);

            _rootDirectory = newRoot;
        }
    }

    public string CreateSubDirectory(string name)
    {
        var fullPath = Path.Combine(RootDirectory, name);
        _ = Directory.CreateDirectory(fullPath);
        return fullPath;
    }

    public string CreateFile(string? fileExtension = null, string? preferredFileName = null)
    {
        string fileName;
        var attempt = 0;
        const int maxAttempts = 10;
        var defaultFileName = preferredFileName;
        while (true)
        {
            fileName = GetFileName(fileExtension, defaultFileName);

            try
            {
                using (new FileStream(fileName, FileMode.CreateNew))
                {
                    // Empty block is intended: we only want to create an empty file
                }

                break;
            }
            catch (IOException ex)
            {
                defaultFileName = string.Empty;
                if (++attempt == maxAttempts)
                    throw new IOException("No unique temporary file name is available.", ex);
            }
        }

        return fileName;
    }

    public string GetFileName(string? fileExtension = null, string? preferredFileName = null)
    {
        var extension = fileExtension;
        if (string.IsNullOrEmpty(extension))
        {
            extension = string.IsNullOrEmpty(preferredFileName) ? ".tmp" : Path.GetExtension(preferredFileName);
        }

        if (extension.StartsWith('*'))
            extension = extension[1..];

        var fileName = string.IsNullOrEmpty(preferredFileName) ? Path.GetRandomFileName() : preferredFileName;

        fileName = Path.ChangeExtension(fileName, extension);
        fileName = Path.Combine(RootDirectory, fileName);

        return fileName;
    }

    public void Delete() => TryDelete(new DirectoryInfo(RootDirectory));

    public void Clean()
    {
        try
        {
            var baseInfo = new DirectoryInfo(RootDirectory);
            foreach (var fileInfo in baseInfo.GetFiles())
                TryDelete(fileInfo);
            foreach (var directoryInfo in baseInfo.GetDirectories())
            {
                TryDelete(directoryInfo);
            }
        }
        catch (Exception ex)
        {
            LogManager.Warning($"an error occurred while attempting to clean temporary data directories: {ex.Message}");
        }
    }

    protected static void TryDelete(DirectoryInfo info)
    {
        try
        {
            info.Delete(true);
        }
        catch (Exception ex)
        {
            LogManager.Warning($"An error occurred while attempting to delete a directory: {ex.Message}");
        }
    }

    private static void TryDelete(FileInfo info)
    {
        try
        {
            info.Delete();
        }
        catch (Exception ex)
        {
            LogManager.Warning($"An error occurred while attempting to delete a file: {ex.Message}");
        }
    }

    private string GetOrMakeRootDirectory(string? root)
    {
        var tempPath = string.IsNullOrEmpty(root)
            ? Path.GetTempPath()
            : Environment.ExpandEnvironmentVariables(root);

        FileHelper.EnsureDirectoryExists(tempPath);

        if (!_listOfDirectories.Contains(tempPath))
            _listOfDirectories.Add(tempPath);

        return tempPath;
    }
}
