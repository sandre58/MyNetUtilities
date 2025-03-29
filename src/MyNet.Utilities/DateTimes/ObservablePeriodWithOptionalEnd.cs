// -----------------------------------------------------------------------
// <copyright file="ObservablePeriodWithOptionalEnd.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace MyNet.Utilities.DateTimes;

public class ObservablePeriodWithOptionalEnd(DateTime start, DateTime? end = null) : PeriodWithOptionalEnd(start, end), INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged
    {
        add => PropertyChangedHandler += value;
        remove => PropertyChangedHandler -= value;
    }

    private event PropertyChangedEventHandler? PropertyChangedHandler;

    public override void SetInterval(DateTime start, DateTime? end = null)
    {
        var oldStart = Start;
        var oldEnd = End;
        base.SetInterval(start, end);
        if (oldStart != Start)
            OnPropertyChanged(nameof(Start));
        if (oldEnd != End)
            OnPropertyChanged(nameof(End));
    }

    protected void OnPropertyChanged(string? propertyName) => PropertyChangedHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
