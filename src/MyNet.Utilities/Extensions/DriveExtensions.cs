// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.IO;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities
#pragma warning restore IDE0130 // Namespace does not match folder structure
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
