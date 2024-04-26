// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Threading;

namespace MyNet.Utilities.Authentication.Windows
{
    [SupportedOSPlatform("windows")]
    public class WindowsAuthenticationService : WindowsAuthenticationService<WindowsUserPrincipal>
    {
        public static readonly WindowsUserPrincipal Anonymous = new(new GenericIdentity(string.Empty), []);

        protected override WindowsUserPrincipal CreatePrincipal(IIdentity identity) => new(identity, []);

        protected override WindowsUserPrincipal GetAnonymous() => Anonymous;
    }

    [SupportedOSPlatform("windows")]
    public abstract class WindowsAuthenticationService<TPrincipal> : IAuthenticationService<TPrincipal>
        where TPrincipal : IPrincipal
    {
        public bool IsAuthenticated => Thread.CurrentPrincipal?.Identity?.IsAuthenticated ?? false;

        public TPrincipal CurrentPrincipal => (TPrincipal?)Thread.CurrentPrincipal ?? GetAnonymous();

        public event EventHandler<AuthenticatedEventArgs>? Authenticated;

        public virtual void Authenticate() => Authenticate(CreatePrincipal(WindowsIdentity.GetCurrent()));

        public virtual void Unauthenticate() => Authenticate(GetAnonymous());

        protected virtual void Authenticate(TPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            AppDomain.CurrentDomain.SetThreadPrincipal(Thread.CurrentPrincipal);

            Authenticated?.Invoke(this, new AuthenticatedEventArgs(IsAuthenticated));
        }

        protected abstract TPrincipal GetAnonymous();

        protected abstract TPrincipal CreatePrincipal(IIdentity identity);
    }
}
