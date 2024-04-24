// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyNet.Utilities.Extensions;

namespace MyNet.Utilities.IO.FileHistory
{
    public class RecentFile(string name, string path, DateTime? lastAccessDate, DateTime? modificationDate, bool isPinned, bool isRecoveredFile = false)
    {
        public string Name { get; } = name.IsRequiredOrThrow();

        public string Path { get; } = path.IsRequiredOrThrow();

        public DateTime? LastAccessDate { get; } = lastAccessDate;

        public DateTime? ModificationDate { get; } = modificationDate;

        public bool IsPinned { get; set; } = isPinned;

        public bool IsRecoveredFile { get; } = isRecoveredFile;

        public override string ToString() => Path;

        public override bool Equals(object? obj) => obj is RecentFile recentFile && Path == recentFile.Path;

        public override int GetHashCode() => Name.GetHashCode();
    }
}
