// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Exceptions
{
    public class FileAlreadyUsedException : TranslatableException
    {
        public FileAlreadyUsedException(string filename)
            : base($"File {0} is used by another process", "FileXAlreadyUsedError", filename) { }
    }
}
