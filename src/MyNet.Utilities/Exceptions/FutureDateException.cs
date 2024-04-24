// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Exceptions
{
    public class FutureDateException : TranslatableException
    {
        public FutureDateException(string property) : base($"The field '{property}' must be in past.", "FieldXMustBeInPastError", property) { }
    }
}
