// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.IO.Registry
{
    public static class IRegistryBaseExtensions
    {
        public static string GetItemFullKey(this IRegistry registryFile) => $@"{registryFile.Parent}\{registryFile.Key}";
    }
}
