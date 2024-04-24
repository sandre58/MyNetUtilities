// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;
using System.Security.Principal;

namespace MyNet.Utilities.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetDomain(this IIdentity identity) => GetDomain(identity.Name);

        public static string GetDomain(string? identity) => identity?.Split('\\').FirstOrDefault() ?? string.Empty;

        public static string GetName(this IIdentity identity) => GetName(identity.Name);

        public static string GetName(string? identity) => identity?.Split('\\').LastOrDefault() ?? string.Empty;
    }
}
