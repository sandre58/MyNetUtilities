// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities
{
    public interface ISimilar
    {
        bool IsSimilar(object? obj);
    }

    public interface ISimilar<in T>
    {
        bool IsSimilar(T? obj);
    }
}
