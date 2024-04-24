// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities
{
    public interface IEnumeration
    {
        string ResourceKey { get; }

        string Name { get; }

        object Value { get; }
    }
}
