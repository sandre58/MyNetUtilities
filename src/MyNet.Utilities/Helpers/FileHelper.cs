// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.IO;
using MyNet.Utilities.Exceptions;

namespace MyNet.Utilities.Helpers
{
    public static class FileHelper
    {

        public static void EnsureDirectoryExists(string path)
        {
            if (File.Exists(path)) File.Delete(path);
            if (!Directory.Exists(path)) _ = Directory.CreateDirectory(path);
        }

        public static bool RemoveFile(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                    return true;
                }
                catch (IOException)
                {
                    throw new FileAlreadyUsedException(Path.GetFileName(filename));
                }
            }

            return false;
        }
    }
}
