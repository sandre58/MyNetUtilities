﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyNet.Utilities.Attributes;

namespace MyNet.Utilities
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        private List<PropertyInfo> _properties = null!;
        private List<FieldInfo> _fields = null!;

        public static bool operator ==(ValueObject? obj1, ValueObject? obj2) => Equals(obj1, null) ? Equals(obj2, null) : obj1.Equals(obj2);

        public static bool operator !=(ValueObject? obj1, ValueObject? obj2) => !(obj1 == obj2);

        public virtual bool Equals(ValueObject? other) => Equals(other as object);

        public override bool Equals(object? obj)
            => !(obj == null || GetType() != obj.GetType())
                && GetProperties().TrueForAll(p => PropertiesAreEqual(obj, p))
                && GetFields().TrueForAll(f => FieldsAreEqual(obj, f));

        private bool PropertiesAreEqual(object? obj, PropertyInfo p) => Equals(p.GetValue(this, null), p.GetValue(obj, null));

        private bool FieldsAreEqual(object? obj, FieldInfo f) => Equals(f.GetValue(this), f.GetValue(obj));

        private List<PropertyInfo> GetProperties()
        {
            _properties ??= GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList();

            return _properties;
        }

        private List<FieldInfo> GetFields()
        {
            _fields ??= GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .Where(f => !Attribute.IsDefined(f, typeof(IgnoreMemberAttribute))).ToList();

            return _fields;
        }

        public override int GetHashCode()
        {
            unchecked   //allow overflow
            {
                var hash = 17;
                foreach (var prop in GetProperties())
                {
                    var value = prop.GetValue(this, null);
                    hash = HashValue(hash, value);
                }

                foreach (var field in GetFields())
                {
                    var value = field.GetValue(this);
                    hash = HashValue(hash, value);
                }

                return hash;
            }
        }

        private static int HashValue(int seed, object? value)
        {
            var currentHash = value != null
                ? value.GetHashCode()
                : 0;

            return seed * 23 + currentHash;
        }
    }
}
