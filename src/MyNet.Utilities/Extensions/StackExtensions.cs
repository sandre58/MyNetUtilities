// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace MyNet.Utilities
{
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
}
