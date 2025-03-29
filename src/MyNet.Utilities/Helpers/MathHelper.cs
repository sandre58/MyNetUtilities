// -----------------------------------------------------------------------
// <copyright file="MathHelper.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Helpers;

public static class MathHelper
{
    public static (double Min, double Max) GetMinMax(double a, double b) => a >= b ? (b, a) : (a, b);

    public static (float Min, float Max) GetMinMax(float a, float b) => a >= b ? (b, a) : (a, b);

    public static (decimal Min, decimal Max) GetMinMax(decimal a, decimal b) => a >= b ? (b, a) : (a, b);

    public static (int Min, int Max) GetMinMax(int a, int b) => a >= b ? (b, a) : (a, b);
}
