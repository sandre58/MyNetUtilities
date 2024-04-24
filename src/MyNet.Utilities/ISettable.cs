// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities
{
    public interface ISettable<in T>
    {
        void SetFrom(T? from);
    }

    public interface ISettable
    {
        void SetFrom(object? from);
    }
}
