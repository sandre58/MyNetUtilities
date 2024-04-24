// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Exceptions
{
    public class NotEnoughDiskSpaceException : TranslatableException
    {
        public NotEnoughDiskSpaceException()
            : base("Not enough disk space.", "NotEnoughSpaceDisk") { }
    }
}
