// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.IO
{
    public interface IDirectoryService
    {
        public string RootDirectory { get; }

        string CreateSubDirectory(string name);

        string CreateFile(string? fileExtension = null, string? preferredFileName = null);

        string GetFileName(string? fileExtension = null, string? preferredFileName = null);

        void Clean();

        void Delete();
    }
}
