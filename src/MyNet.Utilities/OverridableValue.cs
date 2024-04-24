// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace MyNet.Utilities
{
    public class OverridableValue<T> : INotifyPropertyChanged
    {
        private Func<T?>? _getInheritedValue;
        private T? _overrideValue;
        private bool _isOverride = false;

        public event PropertyChangedEventHandler? PropertyChanged
        {
            add => PropertyChangedHandler += value;
            remove => PropertyChangedHandler -= value;
        }

        private event PropertyChangedEventHandler? PropertyChangedHandler;

        public OverridableValue(Func<T?> getInheritedValue) => _getInheritedValue = getInheritedValue;

        public OverridableValue() { }

        public T? Value => !_isOverride ? InheritedValue : _overrideValue;

        public T? OverrideValue => _overrideValue;

        public bool IsInherited => _isOverride;

        public T? InheritedValue => _getInheritedValue is not null ? _getInheritedValue() : default;

        public void Initialize(Func<T?> getInheritedValue) => _getInheritedValue = getInheritedValue;

        public void Initialize<TItem>(TItem item, Expression<Func<T>> propertyExpression)
            where TItem : INotifyPropertyChanged
        {
            _getInheritedValue = propertyExpression.Compile();

            item.PropertyChanged += (sender, e) =>
            {
                var propertyName = propertyExpression.GetPropertyName();

                if (e.PropertyName == propertyName)
                {
                    RaisePropertyChanged(nameof(Value));
                    RaisePropertyChanged(nameof(InheritedValue));
                }
            };
        }

        public void Initialize<TItem>(TItem item, Func<TItem, T?> getInheritedValue, params string[] properties)
            where TItem : INotifyPropertyChanged
        {
            _getInheritedValue = () => getInheritedValue(item);

            item.PropertyChanged += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.PropertyName) && properties.Contains(e.PropertyName))
                {
                    RaisePropertyChanged(nameof(Value));
                    RaisePropertyChanged(nameof(InheritedValue));
                }
            };
        }

        public void Override(T value)
        {
            _overrideValue = value;
            _isOverride = true;

            RaisePropertyChanged(nameof(OverrideValue));
            RaisePropertyChanged(nameof(Value));
        }

        public void Reset()
        {
            _overrideValue = default;
            _isOverride = false;

            RaisePropertyChanged(nameof(OverrideValue));
            RaisePropertyChanged(nameof(Value));
        }

        protected void RaisePropertyChanged(string? propertyName) => PropertyChangedHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public override string? ToString() => Value?.ToString();
    }
}
