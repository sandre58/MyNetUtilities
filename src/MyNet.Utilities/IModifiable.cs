﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities
{
    public interface IModifiable
    {
        void ResetIsModified();

        bool IsModified();
    }
}
