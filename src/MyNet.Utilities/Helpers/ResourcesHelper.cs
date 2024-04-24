// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Reflection;

namespace MyNet.Utilities.Helpers
{
    public static class ResourcesHelper
    {
        public static Stream? ReadFromResourceFile(string endingFileName, Assembly assembly)
        {
            var manifestResourceName = Array.Find(assembly.GetManifestResourceNames(), x => x.EndsWith(endingFileName));

            return !string.IsNullOrEmpty(manifestResourceName) ? assembly.GetManifestResourceStream(manifestResourceName) : null;
        }
    }
}
