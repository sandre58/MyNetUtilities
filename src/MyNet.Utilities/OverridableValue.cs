// -----------------------------------------------------------------------
// <copyright file="OverridableValue.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace MyNet.Utilities;

public class OverridableValue<T> : INotifyPropertyChanged
{
    private Func<T?>? _getInheritedValue;

    public OverridableValue(Func<T?> getInheritedValue) => _getInheritedValue = getInheritedValue;

    public OverridableValue() { }

    public event PropertyChangedEventHandler? PropertyChanged
    {
        add => PropertyChangedHandler += value;
        remove => PropertyChangedHandler -= value;
    }

    private event PropertyChangedEventHandler? PropertyChangedHandler;

    public T? Value => !IsInherited ? InheritedValue : OverrideValue;

    public T? OverrideValue { get; private set; }

    public bool IsInherited { get; private set; }

    public T? InheritedValue => _getInheritedValue is not null ? _getInheritedValue() : default;

    public void Initialize(Func<T?> getInheritedValue) => _getInheritedValue = getInheritedValue;

    public void Initialize<TItem>(TItem item, Expression<Func<T>> propertyExpression)
        where TItem : INotifyPropertyChanged
    {
        _getInheritedValue = propertyExpression.Compile();

        item.PropertyChanged += (_, e) =>
        {
            var propertyName = propertyExpression.GetPropertyName();

            if (e.PropertyName != propertyName) return;
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(InheritedValue));
        };
    }

    public void Initialize<TItem>(TItem item, Func<TItem, T?> getInheritedValue, params string[] properties)
        where TItem : INotifyPropertyChanged
    {
        _getInheritedValue = () => getInheritedValue(item);

        item.PropertyChanged += (_, e) =>
        {
            if (string.IsNullOrEmpty(e.PropertyName) || !properties.Contains(e.PropertyName)) return;
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(InheritedValue));
        };
    }

    public void Override(T value)
    {
        OverrideValue = value;
        IsInherited = true;

        OnPropertyChanged(nameof(OverrideValue));
        OnPropertyChanged(nameof(Value));
    }

    public void Reset()
    {
        OverrideValue = default;
        IsInherited = false;

        OnPropertyChanged(nameof(OverrideValue));
        OnPropertyChanged(nameof(Value));
    }

    public override string? ToString() => Value?.ToString();

    protected void OnPropertyChanged(string? propertyName) => PropertyChangedHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
