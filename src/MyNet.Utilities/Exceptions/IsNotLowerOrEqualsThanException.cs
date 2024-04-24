// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Exceptions
{
    public class IsNotLowerOrEqualsThanException : TranslatableException
    {
        public IsNotLowerOrEqualsThanException(string property, object? target)
            : base($"the value of '{property}' must be lower than {(target is DateTime date ? date.ToLocalTime() : target)}.", "FieldXMustBeLowerOrEqualsThanYError", property, target is DateTime date1 ? date1.ToLocalTime() : target) { }
    }
}
