// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities
{
    /// <summary>
    /// Supports copying, which creates a new instance of a class with the same values as an existing instance.
    /// </summary>
    public interface ICloneable<out T>
    {
        /// <summary>
        /// Creates a deep copy of the current object including its properties.
        /// </summary>     
        /// <returns>
        /// A copy of the current object.
        /// </returns>
        T Clone();
    }
}
