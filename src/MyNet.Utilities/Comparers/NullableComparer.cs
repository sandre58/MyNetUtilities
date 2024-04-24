// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace MyNet.Utilities.Comparers
{
    public class NullableComparer<T> : IComparer<T?>, IComparer<IComparable<T>?>
          where T : struct, IComparable<T>
    {

        public int Compare(T? x, T? y)
        {
            //Compare nulls acording MSDN specification

            //Two nulls are equal
            if (x == null && y == null)
                return 0;

            //Any object is greater than null
            if (x != null && y == null)
                return 1;

            if (x == null)
                return -1;

            //Otherwise compare the two values
            return x.Value.CompareTo(y!.Value);
        }

        public int Compare(IComparable<T>? x, IComparable<T>? y)
        {
            //Compare nulls acording MSDN specification

            //Two nulls are equal
            if (x == null && y == null)
                return 0;

            //Any object is greater than null
            if (x != null && y == null)
                return 1;

            if (x == null)
                return -1;

            //Otherwise compare the two values
            return x.CompareTo((T)y!);
        }
    }
}
