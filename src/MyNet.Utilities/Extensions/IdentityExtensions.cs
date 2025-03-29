// -----------------------------------------------------------------------
// <copyright file="IdentityExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using System.Security.Principal;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class IdentityExtensions
{
    public static string GetDomain(this IIdentity identity) => GetDomain(identity.Name);

    public static string GetDomain(string? identity) => identity?.Split('\\').FirstOrDefault() ?? string.Empty;

    public static string GetName(this IIdentity identity) => GetName(identity.Name);

    public static string GetName(string? identity) => identity?.Split('\\').LastOrDefault() ?? string.Empty;
}
