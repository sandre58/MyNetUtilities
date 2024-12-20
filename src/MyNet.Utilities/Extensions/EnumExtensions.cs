﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public static class EnumExtensions
    {
        public static int CountFlags<T>(this T options) where T : Enum => Enum.GetValues(typeof(T)).Cast<Enum>().Count(options.HasFlag);
    }
}
