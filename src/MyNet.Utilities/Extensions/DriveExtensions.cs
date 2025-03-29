// -----------------------------------------------------------------------
// <copyright file="DriveExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public enum DiskDriveInfo
{
    NoError,
    DiskNotFound,
    InsufficientSpace
}

public static class DriveExtensions
{
    public static DiskDriveInfo HasEnoughSpace(this DriveInfo driveInfo, double space) => !driveInfo.IsReady
        ? DiskDriveInfo.DiskNotFound
        : driveInfo.TotalFreeSpace >= space ? DiskDriveInfo.NoError : DiskDriveInfo.InsufficientSpace;
}
