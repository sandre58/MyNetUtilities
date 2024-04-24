// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Sequences
{
    public interface ISequence<out T>
    {
        T NextValue { get; }

        T CurrentValue { get; }
    }
}
