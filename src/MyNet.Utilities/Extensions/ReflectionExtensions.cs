// -----------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

#if NET9_0_OR_GREATER
using System.Threading;
#endif

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace MyNet.Utilities;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class ReflectionExtensions
{
    private static readonly Dictionary<Type, IList<PropertyInfo>> PropertiesCache = [];

#if NET9_0_OR_GREATER
    private static readonly Lock LockObject = new();
#else
    private static readonly object LockObject = new();
#endif

    public static IList<PropertyInfo> GetPublicProperties(this Type type)
    {
        lock (LockObject)
        {
            if (!PropertiesCache.ContainsKey(type))
            {
                PropertiesCache.Add(type, [.. type.GetProperties().Where(x => x.CanWrite || x.CanRead)]);
            }
        }

        lock (LockObject)
        {
            return PropertiesCache[type];
        }
    }

    public static IList<PropertyInfo> GetPublicPropertiesWithAttribute<TAttribute>(this Type type)
        where TAttribute : Attribute
        => [.. type.GetPublicProperties().Where(x => x.HasAttribute<TAttribute>())];

    public static bool HasAttribute<T>(this PropertyInfo property)
        where T : Attribute
        => property.GetCustomAttributes<T>().Any() || property.PropertyType.GetCustomAttributes<T>().Any();

    public static bool HasPublicSetterOrGetter(this PropertyInfo property)
    {
        var setMethod = property.GetSetMethod();
        var getMethod = property.GetGetMethod();
        return (setMethod != null && setMethod.IsPublic) || (getMethod != null && getMethod.IsPublic);
    }

    public static IEnumerable<T?> GetValuesOfType<T>(this IEnumerable<PropertyInfo> properties, object obj)
        => properties.Where(x => typeof(T).IsAssignableFrom(x.PropertyType)).Select(x => (T?)x.GetValue(obj)).Where(x => x is not null);

    public static object? GetDefault(this Type t)
    {
        var f = GetDefault<object?>;
        return f.Method.GetGenericMethodDefinition().MakeGenericMethod(t).Invoke(null, null);
    }

    public static T? GetAttribute<T>(this Enum value)
        where T : Attribute
        => value.GetType().GetField(value.ToString())?.GetCustomAttributes<T>().FirstOrDefault();

    public static object? GetDeepPropertyValue(this object rootObject, string path) => GetDeepPropertyValue(rootObject, path.Split(["."], StringSplitOptions.RemoveEmptyEntries));

    public static object? GetDeepPropertyValue(this object rootObject, IList<string> propertyNames)
    {
        var result = rootObject;

        if (!propertyNames.Any()) return result;
        foreach (var propertyName in propertyNames)
        {
            var properties = result?.GetType().GetPublicProperties();

            var propertyInfo = properties?.FirstOrDefault(x => x.Name == propertyName);
            if (propertyInfo == null)
            {
                return null;
            }

            result = propertyInfo.GetValue(result, null);
        }

        return result;
    }

    public static T? GetDeepPropertyValue<T>(this object obj, string path) => (T?)GetDeepPropertyValue(obj, path);

    public static T? GetDeepPropertyValue<T>(this object obj, IList<string> propertyNames) => (T?)GetDeepPropertyValue(obj, propertyNames);

    /// <summary>
    /// Gets the member inheritance chain as a stack.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <param name="expression">The member expression.</param>
    /// <returns>The inheritance chain for the given member expression as a stack.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stack<MemberInfo> GetMembers<TModel, TProperty>(this Expression<Func<TModel, TProperty>> expression)
    {
        var stack = new Stack<MemberInfo>();

        var currentExpression = expression.Body;
        while (true)
        {
            var memberExpression = currentExpression?.GetMemberExpression();
            if (memberExpression == null)
            {
                break;
            }

            stack.Push(memberExpression.Member);
            currentExpression = memberExpression.Expression;
        }

        return stack;
    }

    /// <exception cref="ArgumentException">If the expression does not represent a property.</exception>
    public static string? GetPropertyName<T>(this Expression<Func<T>> propertyExpression) => ((propertyExpression.Body as MemberExpression)?.Member as PropertyInfo)?.Name;

    public static string? GetPropertyName<TSource, T>(this Expression<Func<TSource, T>> propertyAccessor)
        => (propertyAccessor.Body as MemberExpression ?? ((UnaryExpression)propertyAccessor.Body).Operand as MemberExpression)?.Member.Name;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static MemberExpression? GetMemberExpression(this Expression expression)
    {
        MemberExpression? memberExpression = null;
        switch (expression.NodeType)
        {
            case ExpressionType.Convert:
                {
                    var body = (UnaryExpression)expression;
                    memberExpression = body.Operand as MemberExpression;
                    break;
                }

            case ExpressionType.MemberAccess:
                memberExpression = expression as MemberExpression;
                break;
        }

        return memberExpression;
    }

    private static T? GetDefault<T>() => default;
}
