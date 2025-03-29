// -----------------------------------------------------------------------
// <copyright file="ResourcesHelper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;

namespace MyNet.Utilities.Helpers;

public static class ResourcesHelper
{
    public static Stream? ReadFromResourceFile(string endingFileName, Assembly assembly)
    {
        var manifestResourceName = Array.Find(assembly.GetManifestResourceNames(), x => x.EndsWith(endingFileName, StringComparison.OrdinalIgnoreCase));

        return !string.IsNullOrEmpty(manifestResourceName) ? assembly.GetManifestResourceStream(manifestResourceName) : null;
    }
}
