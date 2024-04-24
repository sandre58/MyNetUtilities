// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Security.Principal;

namespace MyNet.Utilities.Authentication
{
    public interface IAuthenticationService<out TPrincipal>
        where TPrincipal : IPrincipal
    {
        void Authenticate();

        void Unauthenticate();

        bool IsAuthenticated { get; }

        TPrincipal CurrentPrincipal { get; }

        public event EventHandler<AuthenticatedEventArgs>? Authenticated;
    }
}
