// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.IO.Registry.FileManagement;

namespace MyNet.Utilities.IO.FileHistory.Registry
{
    internal class RegistryRecentFile : RegistryFile
    {
        public string? Name { get; set; }

        public bool IsPinned { get; set; }

        public bool IsRecoveredFile { get; set; }
    }
}
