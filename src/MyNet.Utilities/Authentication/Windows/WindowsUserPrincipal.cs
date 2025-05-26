// -----------------------------------------------------------------------
// <copyright file="WindowsUserPrincipal.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Principal;

namespace MyNet.Utilities.Authentication.Windows;

public class WindowsUserPrincipal(IIdentity identity, string[] roles) : GenericPrincipal(identity, roles)
{
    public string Name { get; } = identity.GetName();

    public string Domain { get; } = identity.GetDomain();
}
