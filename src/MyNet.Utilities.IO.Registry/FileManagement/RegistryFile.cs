// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.IO.Registry.FileManagement
{
    public class RegistryFile
    {
        public RegistryFile()
        {
        }

        public string Path { get; set; } = string.Empty;

        public long LastAccessDate { get; set; }
    }
}
