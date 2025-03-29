// -----------------------------------------------------------------------
// <copyright file="FileExtensionsTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.IO.FileExtensions;
using Xunit;

namespace MyNet.Utilities.UnitTests;

public class FileExtensionsTests
{
    [Fact]
    public void Add_AddsSingleExtension()
    {
        var builder = new FileExtensionFilterBuilder();
        builder.Add(new FileExtensionInfo("Test", [".txt"]));

        Assert.Equal(0, builder.IndexOfExtension(".txt"));
    }

    [Fact]
    public void AddRange_AddsMultipleExtensions()
    {
        var builder = new FileExtensionFilterBuilder();
        var extensions = new List<FileExtensionInfo>
        {
            new("Test1", [".txt"]),
            new("Test2", [".jpg"])
        };

        builder.AddRange(extensions);

        Assert.Equal(1, builder.IndexOfExtension(".jpg"));
    }

    [Fact]
    public void AddMerge_AddsExtensionAtFirstPosition()
    {
        var builder = new FileExtensionFilterBuilder();
        builder.Add(new FileExtensionInfo("Existing", [".txt"]));
        builder.AddMerge("New", true, new FileExtensionInfo("Test", [".jpg"]));

        Assert.Equal(0, builder.IndexOfExtension(".jpg"));
    }

    [Fact]
    public void AddMerge_AddsExtensionAtLastPosition()
    {
        var builder = new FileExtensionFilterBuilder();
        builder.Add(new FileExtensionInfo("Existing", [".txt"]));
        builder.AddMerge("New", false, new FileExtensionInfo("Test", [".jpg"]));

        Assert.Equal(1, builder.IndexOfExtension(".jpg"));
    }

    [Fact]
    public void AddContentMerge_AddsAllExtensions()
    {
        var builder = new FileExtensionFilterBuilder();
        builder.Add(new FileExtensionInfo("Existing1", [".txt"]));
        builder.Add(new FileExtensionInfo("Existing2", [".jpg"]));

        builder.AddContentMerge("Merged");

        Assert.Equal(1, builder.IndexOfExtension(".jpg"));
        Assert.Equal(0, builder.IndexOfExtension(".txt"));
    }

    [Fact]
    public void GenerateFilters_ReturnsCorrectFilters()
    {
        var builder = new FileExtensionFilterBuilder();
        builder.Add(new FileExtensionInfo("Test1", [".txt"]));
        builder.Add(new FileExtensionInfo("Test2", [".jpg"]));

        var filters = builder.GenerateFilters();

        Assert.Equal("Test1 (*.txt)|*.txt|Test2 (*.jpg)|*.jpg", filters);
    }

    [Fact]
    public void IndexOfExtension_ReturnsCorrectIndex()
    {
        var builder = new FileExtensionFilterBuilder();
        builder.Add(new FileExtensionInfo("Test1", [".txt"]));
        builder.Add(new FileExtensionInfo("Test2", [".jpg"]));

        var index = builder.IndexOfExtension(".jpg");

        Assert.Equal(1, index);
    }

    [Fact]
    public void GetFileFilter_ReturnsCorrectNameAndExtensions()
    {
        var fileTypeExtension = new FileExtensionInfo("Test", [".txt", ".jpg"]);

        var (name, extensions) = fileTypeExtension.GetFileFilter();

        Assert.Equal("Test (*.txt;*.jpg)", name);
        Assert.Equal("*.txt;*.jpg", extensions);
    }

    [Fact]
    public void GetAllFilesFilter_ReturnsCorrectNameAndExtensions()
    {
        var fileTypeExtension = FileExtensionInfoProvider.AllFiles;

        var (name, extensions) = fileTypeExtension.GetFileFilter();

        Assert.Equal("AllFiles", name);
        Assert.Equal("*.*", extensions);
    }

    [Fact]
    public void GetFileFilters_ReturnsCorrectFiltersForSingleExtension()
    {
        var fileTypeExtension = new FileExtensionInfo("Test", [".txt"]);

        var filters = fileTypeExtension.GetFileFilters();

        Assert.Equal("Test (*.txt)|*.txt", filters);
    }

    [Fact]
    public void GetFileFilters_ReturnsCorrectFiltersForMultipleExtensions()
    {
        var fileTypeExtensions = new List<FileExtensionInfo>
        {
            new("Test1", [".txt", ".csv", ".test"]),
            new("Test2", [".jpg"])
        };

        var filters = fileTypeExtensions.GetFileFilters();

        Assert.Equal("Test1 (*.txt;*.csv;*.test)|*.txt;*.csv;*.test|Test2 (*.jpg)|*.jpg", filters);
    }

    [Fact]
    public void GetExtensionNames_ReturnsLowercaseExtensions()
    {
        var fileTypeExtension = new FileExtensionInfo("Test", [".TXT", ".JPG"]);

        var extensions = fileTypeExtension.GetExtensionNames();

        var list = extensions.ToList();
        Assert.Contains(".txt", list);
        Assert.Contains(".jpg", list);
    }

    [Fact]
    public void FilterFiles_ReturnsFilteredFiles()
    {
        var fileTypeExtension = new FileExtensionInfo("Test", [".txt"]);
        var fileNames = new List<string> { "file1.txt", "file2.jpg", "file3.exe" };

        var filteredFiles = fileTypeExtension.FilterFiles(fileNames);

        var list = filteredFiles as string[] ?? [.. filteredFiles];
        Assert.Contains("file1.txt", list);
        Assert.DoesNotContain("file2.jpg", list);
        Assert.DoesNotContain("file3.exe", list);
    }

    [Theory]
    [InlineData(".txt", "file.txt", true)]
    [InlineData(".txt", "file.TXT", true)]
    [InlineData(".txt", "file.jpg", false)]
    public void IsValid_ReturnsCorrectValidity(string extension, string filename, bool expected)
    {
        var fileTypeExtension = new FileExtensionInfo("Test", [extension]);

        var isValid = fileTypeExtension.IsValid(filename);

        Assert.Equal(expected, isValid);
    }

    [Fact]
    public void Concat_ConcatenatesExtensions()
    {
        var fileTypeExtension1 = new FileExtensionInfo("Test1", [".txt"]);
        var fileTypeExtension2 = new FileExtensionInfo("Test2", [".jpg"]);

        var concatenatedExtension = fileTypeExtension1.Concat(fileTypeExtension2, "Concatenated");

        Assert.Equal("Concatenated", concatenatedExtension.Key);
        Assert.Contains(".txt", concatenatedExtension.Extensions);
        Assert.Contains(".jpg", concatenatedExtension.Extensions);
    }
}
