// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using MyNet.Utilities.IO.FileExtensions;
using MyNet.Utilities.Resources;

namespace MyNet.Utilities.IO.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class FileExtensionsAllowedAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public bool AllowEmpty { get; set; } = true;

        public FileExtensionsAllowedAttribute(string extensions) : this(extensions.Split(';')) { }

        public FileExtensionsAllowedAttribute(FileExtensionInfo[] extensions) : this(extensions.SelectMany(x => x.Extensions).Distinct().ToArray()) { }

        public FileExtensionsAllowedAttribute(FileExtensionInfo extensions) : this(extensions.Extensions) { }

        public FileExtensionsAllowedAttribute(string[] extensions)
        {
            _extensions = extensions;
            ErrorMessageResourceName = nameof(InternalResources.FieldXMustContainsAllowedExtensionsYError);
            ErrorMessageResourceType = typeof(InternalResources);
        }

        public override string FormatErrorMessage(string name) => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, string.Join(" | ", _extensions));

        public override bool IsValid(object? value)
            => AllowEmpty && string.IsNullOrEmpty(value?.ToString()) || !string.IsNullOrEmpty(value?.ToString()) && value is string filepath && _extensions.Contains(Path.GetExtension(filepath));
    }
}
