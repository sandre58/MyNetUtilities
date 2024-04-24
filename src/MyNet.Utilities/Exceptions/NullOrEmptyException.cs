// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Exceptions
{
    public class NullOrEmptyException : TranslatableException
    {
        public NullOrEmptyException(string property) : base($"Field {property} is required.", "FieldXIsRequiredError", property) { }
    }
}
