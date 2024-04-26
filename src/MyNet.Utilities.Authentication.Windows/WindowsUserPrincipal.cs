// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Security.Principal;
using MyNet.Utilities.Extensions;

namespace MyNet.Utilities.Authentication.Windows
{
    public class WindowsUserPrincipal(IIdentity identity, string[] roles) : GenericPrincipal(identity, roles)
    {
        public string Name { get; } = identity.GetName();

        public string Domain { get; } = identity.GetDomain();
    }
}
