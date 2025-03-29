// -----------------------------------------------------------------------
// <copyright file="FileExtensionsAllowedAttribute.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using MyNet.Utilities.IO.FileExtensions;
using MyNet.Utilities.Resources;

namespace MyNet.Utilities.IO.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class FileExtensionsAllowedAttribute : ValidationAttribute
{
    public FileExtensionsAllowedAttribute(string extension)
        : this(extension.Split(';')) { }

    public FileExtensionsAllowedAttribute(FileExtensionInfo[] extensionInfos)
        : this(extensionInfos.SelectMany(x => x.Extensions).Distinct().ToArray()) { }

    public FileExtensionsAllowedAttribute(FileExtensionInfo extensionInfo)
        : this(extensionInfo.Extensions) { }

    public FileExtensionsAllowedAttribute(string[] extensions)
    {
        Extensions = extensions;
        Extension = null;
        ExtensionInfos = null;
        ExtensionInfo = null;
        ErrorMessageResourceName = nameof(InternalResources.FieldXMustContainsAllowedExtensionsYError);
        ErrorMessageResourceType = typeof(InternalResources);
    }

    public bool AllowEmpty { get; set; } = true;

    public string[] Extensions { get; }

    public FileExtensionInfo? ExtensionInfo { get; }

    public FileExtensionInfo[]? ExtensionInfos { get; }

    public string? Extension { get; }

    public override string FormatErrorMessage(string name) => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, string.Join(" | ", Extensions));

    public override bool IsValid(object? value)
        => (AllowEmpty && string.IsNullOrEmpty(value?.ToString())) || (!string.IsNullOrEmpty(value?.ToString()) && value is string filepath && Extensions.Contains(Path.GetExtension(filepath)));
}
