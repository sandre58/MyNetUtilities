// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Helpers
{
    public static class MathHelper
    {
        public static (double min, double max) GetMinMax(double a, double b) => a >= b ? (b, a) : (a, b);

        public static (float min, float max) GetMinMax(float a, float b) => a >= b ? (b, a) : (a, b);

        public static (decimal min, decimal max) GetMinMax(decimal a, decimal b) => a >= b ? (b, a) : (a, b);

        public static (int min, int max) GetMinMax(int a, int b) => a >= b ? (b, a) : (a, b);
    }
}
