﻿// -----------------------------------------------------------------------
// <copyright file="ArrayExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ArrayExtensions
{
    public static void ForEach(this Array array, Action<Array, int[]> action)
    {
        if (array.LongLength == 0)
            return;

        var walker = new ArrayTraverse(array);
        do
            action(array, walker.Position);
        while (walker.Step());
    }

    internal sealed class ArrayTraverse
    {
        private readonly int[] _maxLengths;

        public ArrayTraverse(Array array)
        {
            _maxLengths = new int[array.Rank];
            for (var i = 0; i < array.Rank; ++i)
                _maxLengths[i] = array.GetLength(i) - 1;

            Position = new int[array.Rank];
        }

        public int[] Position { get; }

        public bool Step()
        {
            for (var i = 0; i < Position.Length; ++i)
            {
                if (Position[i] >= _maxLengths[i]) continue;

                Position[i]++;
                for (var j = 0; j < i; j++)
                    Position[j] = 0;

                return true;
            }

            return false;
        }
    }
}
