// -----------------------------------------------------------------------
// <copyright file="ValueObject.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyNet.Utilities.Attributes;

namespace MyNet.Utilities;

public abstract class ValueObject : IEquatable<ValueObject>
{
    private List<PropertyInfo>? _properties;
    private List<FieldInfo>? _fields;

    public static bool operator ==(ValueObject? obj1, ValueObject? obj2) => obj1?.Equals(obj2) ?? Equals(obj2, null);

    public static bool operator !=(ValueObject? obj1, ValueObject? obj2) => !(obj1 == obj2);

    public virtual bool Equals(ValueObject? other) => Equals(other as object);

    public override bool Equals(object? obj)
        => !(obj == null || GetType() != obj.GetType())
           && GetProperties().TrueForAll(p => PropertiesAreEqual(obj, p))
           && GetFields().TrueForAll(f => FieldsAreEqual(obj, f));

    public override int GetHashCode()
    {
        var hash = GetProperties().Select(prop => prop.GetValue(this, null)).Aggregate(17, HashValue);

        return GetFields().Select(field => field.GetValue(this)).Aggregate(hash, HashValue);
    }

    private static int HashValue(int seed, object? value)
    {
        var currentHash = value?.GetHashCode() ?? 0;

        return (seed * 23) + currentHash;
    }

    private bool PropertiesAreEqual(object? obj, PropertyInfo p) => Equals(p.GetValue(this, null), p.GetValue(obj, null));

    private bool FieldsAreEqual(object? obj, FieldInfo f) => Equals(f.GetValue(this), f.GetValue(obj));

    private List<PropertyInfo> GetProperties()
    {
        _properties ??= [.. GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute)))];

        return _properties;
    }

    private List<FieldInfo> GetFields()
    {
        _fields ??= [.. GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).Where(f => !Attribute.IsDefined(f, typeof(IgnoreMemberAttribute)))];

        return _fields;
    }
}
