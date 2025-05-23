﻿// -----------------------------------------------------------------------
// <copyright file="StackExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class StackExtensions
{
    public static void Remove<T>(this Stack<T> stack, T obj)
    {
        var temp = new Stack<T>();

        while (stack.Count > 0)
        {
            var element = stack.Pop();

            if (!Equals(element, obj))
            {
                temp.Push(element);
            }
        }

        while (temp.TryPop(out var element))
            stack.Push(element);
    }
}
