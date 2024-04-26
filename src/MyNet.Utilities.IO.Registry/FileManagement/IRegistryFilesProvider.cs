// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNet.Utilities.IO.Registry.FileManagement
{
    public interface IRegistryFilesProvider<T>
    {
        string Type { get; }

        Task<IEnumerable<T>> GetFilesAsync();

        IEnumerable<T> GetFiles();
    }
}
