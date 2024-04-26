// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.IO.Registry
{
    public interface IRegistry
    {
        string Key { get; }

        string Parent { get; }
    }
}
