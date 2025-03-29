// -----------------------------------------------------------------------
// <copyright file="CharHelper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace MyNet.Utilities.Helpers;

public static class CharHelper
{
    public static char[] GetAlphabet() => [.. Enumerable.Range('A', 26).Select(x => (char)x)];
}
