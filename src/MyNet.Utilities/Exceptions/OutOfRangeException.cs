// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Exceptions
{
    public class OutOfRangeException : TranslatableException
    {
        public OutOfRangeException() : base("the specified value is out of range.") { }

        public OutOfRangeException(string property, object min, object max) : base($"the field '{property}' is out of range.", "FieldXMustBeBetweenYAndZError", property, min, max) { }
    }
}
