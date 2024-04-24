// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.IO;

namespace MyNet.Utilities
{
    public enum DiskDriveInfo
    {
        NoError,
        DiskNotFound,
        InsuficientSpace
    }

    public static class DriveExtensions
    {
        public static DiskDriveInfo HasEnoughSpace(this DriveInfo driveInfo, double space) => !driveInfo.IsReady
                ? DiskDriveInfo.DiskNotFound
                : driveInfo.TotalFreeSpace >= space ? DiskDriveInfo.NoError : DiskDriveInfo.InsuficientSpace;
    }
}
