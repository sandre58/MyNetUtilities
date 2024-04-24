// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;

namespace MyNet.Utilities.Helpers
{
    public static class CharHelper
    {
        public static char[] GetAlphabet() => Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
    }
}
