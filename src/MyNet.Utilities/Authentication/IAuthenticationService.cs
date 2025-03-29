// -----------------------------------------------------------------------
// <copyright file="IAuthenticationService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Security.Principal;

namespace MyNet.Utilities.Authentication;

public interface IAuthenticationService<out TPrincipal>
    where TPrincipal : IPrincipal
{
    event EventHandler<AuthenticatedEventArgs>? Authenticated;

    bool IsAuthenticated { get; }

    TPrincipal CurrentPrincipal { get; }

    void Authenticate();

    void Unauthenticate();
}
