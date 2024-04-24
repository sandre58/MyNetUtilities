// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Exceptions
{
    public class IsNotUpperOrEqualsThanException : TranslatableException
    {
        public IsNotUpperOrEqualsThanException(string property, object? target)
            : base($"the value of '{property}' must be upper than {target}.", "FieldXMustBeUpperOrEqualsThanYError", property, target) { }
    }
}
