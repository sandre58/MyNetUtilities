// -----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ObjectExtensions
{
    /// <summary>
    /// The Clone Method that will be recursively used for the deep clone.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Assumed")]
    private static readonly MethodInfo? CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

    public static string ToStringOrEmpty(object? obj) => obj?.ToString() ?? string.Empty;

    public static T CastIn<T>(this object obj) => (T)obj;

    public static T Clone<T>(this T obj)
        where T : ICloneable
        => (T)obj.Clone();

    public static TResult? To<TIn, TResult>(this TIn? value, Func<TIn, TResult> func) => value is null ? default : func.Invoke(value);

    /// <summary>
    /// Returns TRUE if the type is a primitive one, FALSE otherwise.
    /// </summary>
    public static bool IsPrimitive(this Type type) => type == typeof(string) || type is { IsValueType: true, IsPrimitive: true };

    /// <summary>
    /// Returns a Deep Clone / Deep Copy of an object using a recursive call to the CloneMethod specified above.
    /// </summary>
    public static object? DeepCopy(this object obj) => DeepCloneInternal(obj, new Dictionary<object, object?>(Comparers.ReferenceEqualityComparer.Instance));

    /// <summary>
    /// Returns a Deep Clone / Deep Copy of an object of type T using a recursive call to the CloneMethod specified above.
    /// </summary>
    public static T? DeepCopy<T>(this T obj) => (T?)((object?)obj)?.DeepCopy();

    public static void DeepSet(this object toObject, object? obj)
    {
        if (obj == null || obj.GetType().IsPrimitive())
            return;

        var typeToReflect = obj.GetType();

        if (typeof(Delegate).IsAssignableFrom(typeToReflect))
            return;

        if (typeToReflect.IsArray)
        {
            var arrayType = typeToReflect.GetElementType();
            if (arrayType != null && !arrayType.IsPrimitive())
            {
                var clonedArray = (Array)toObject;
                clonedArray.ForEach((array, indices) => array.SetValue(DeepCloneInternal(clonedArray.GetValue(indices), new Dictionary<object, object?>(Comparers.ReferenceEqualityComparer.Instance)), indices));
            }
        }

        CopyFields(obj, new Dictionary<object, object?>(Comparers.ReferenceEqualityComparer.Instance), toObject, typeToReflect);
        RecursiveCopyBaseTypePrivateFields(obj, new Dictionary<object, object?>(Comparers.ReferenceEqualityComparer.Instance), toObject, typeToReflect);
    }

    public static void DeepSet<T>(this T toObject, T obj) => ((object?)toObject)?.DeepSet(obj);

    private static object? DeepCloneInternal(object? obj, IDictionary<object, object?> visited)
    {
        if (obj == null)
            return null;

        var typeToReflect = obj.GetType();
        if (typeToReflect.IsPrimitive())
            return obj;

        if (visited.TryGetValue(obj, out var value))
            return value;

        if (typeof(Delegate).IsAssignableFrom(typeToReflect))
            return obj;

        var cloneObject = CloneMethod?.Invoke(obj, null);
        if (typeToReflect.IsArray)
        {
            var arrayType = typeToReflect.GetElementType();
            if (arrayType != null && !arrayType.IsPrimitive())
            {
                var clonedArray = (Array?)cloneObject;
                clonedArray?.ForEach((array, indices) => array.SetValue(DeepCloneInternal(clonedArray.GetValue(indices), visited), indices));
            }
        }

        visited.Add(obj, cloneObject);
        CopyFields(obj, visited, cloneObject, typeToReflect);
        RecursiveCopyBaseTypePrivateFields(obj, visited, cloneObject, typeToReflect);
        return cloneObject;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Assumed")]
    private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object?> visited, object? cloneObject, Type typeToReflect)
    {
        if (typeToReflect.BaseType == null) return;
        RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
        CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Assumed")]
    private static void CopyFields(object originalObject, IDictionary<object, object?> visited, object? cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool>? filter = null)
    {
        foreach (var fieldInfo in typeToReflect.GetFields(bindingFlags))
        {
            if (filter != null && !filter(fieldInfo))
                continue;

            if (typeof(Delegate).IsAssignableFrom(fieldInfo.FieldType))
                continue;

            var originalFieldValue = fieldInfo.GetValue(originalObject);
            var clonedFieldValue = DeepCloneInternal(originalFieldValue, visited);
            fieldInfo.SetValue(cloneObject, clonedFieldValue);
        }
    }
}
