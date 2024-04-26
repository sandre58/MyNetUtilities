﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace MyNet.Utilities.IO.Registry.FileManagement
{
    public interface IRegistryFileServiceParameters
    {
        int SavedMaxCount { get; set; }

        string BaseRegistry { get; set; }

        ICollection<string> SupportedTypes { get; set; }
    }
}
