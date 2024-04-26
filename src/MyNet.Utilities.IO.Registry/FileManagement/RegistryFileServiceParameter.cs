// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace MyNet.Utilities.IO.Registry.FileManagement
{
    public class RegistryFileServiceParameter(string baseRegistry) : IRegistryFileServiceParameters
    {
        public int SavedMaxCount { get; set; }

        public string BaseRegistry { get; set; } = baseRegistry;

        public ICollection<string> SupportedTypes { get; set; } = [];
    }
}
