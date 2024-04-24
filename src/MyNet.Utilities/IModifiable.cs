// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities
{
    public interface IModifiable
    {
        /// <summary>
        /// Reset IsModified value.
        /// </summary>
        void ResetIsModified();

        /// <summary>
        /// Gets if the object is modified.
        /// </summary>
        bool IsModified();
    }
}
