// -----------------------------------------------------------------------
// <copyright file="PerformanceLogger.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;

#if NET9_0_OR_GREATER
using System.Threading;
#endif

namespace MyNet.Utilities.Logging;

public sealed class PerformanceLogger : IDisposable
{
    private static readonly Stack<PerformanceLogger> OperationGroups = new();

#if NET9_0_OR_GREATER
    private static readonly Lock TimeLocker = new();
    private static readonly Lock CurrentObject = new();
#else
    private static readonly object TimeLocker = new();
    private static readonly object CurrentObject = new();
#endif

    private static readonly Dictionary<TraceLevel, Action<string>> GroupLogAction = new()
    {
        [TraceLevel.Trace] = LogManager.Trace,
        [TraceLevel.Debug] = LogManager.Debug,
        [TraceLevel.Info] = LogManager.Info
    };

    private readonly Dictionary<string, (TraceLevel Level, TimeSpan Time)> _registeredTimes = [];

    private readonly string _title;
    private readonly TraceLevel _traceLevel;
    private readonly Stopwatch _stopwatch;

    internal PerformanceLogger(string title, TraceLevel traceLevel = TraceLevel.Debug)
    {
        _title = title ?? throw new ArgumentNullException(nameof(title));
        _traceLevel = traceLevel;

        OperationGroups.Push(this);

        LogManager.Trace($"START - {_title}");

        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    internal static PerformanceLogger? Current => OperationGroups.Count == 0 ? null : OperationGroups.Peek();

    private TimeSpan TotalTime => _stopwatch.Elapsed;

    public void Dispose()
    {
        lock (CurrentObject)
        {
            Pop();

            _stopwatch.Stop();

            LogManager.Trace($"END - {_title} : {_stopwatch.Elapsed}");

            if (Current == null)
            {
                TraceTimes();
            }
            else
            {
                Current.AddTime(_title, _stopwatch.Elapsed, _traceLevel);
                Current.AddGroupTime(this);
            }
        }
    }

    internal void AddTime(string key, TimeSpan time, TraceLevel level)
    {
        lock (TimeLocker)
        {
            _registeredTimes[key] = _registeredTimes.TryGetValue(key, out var value) ? (level, value.Time + time) : (level, time);
        }
    }

    private static void Pop()
    {
        if (OperationGroups.Count > 0)
            _ = OperationGroups.Pop();
    }

    private static void DisplayTime(KeyValuePair<string, (TraceLevel Level, TimeSpan Time)> item) =>
        GroupLogAction[item.Value.Level]($"{item.Key} - {item.Value.Time}");

    private void AddGroupTime(PerformanceLogger group)
    {
        if (group._registeredTimes.Count == 0) return;

        KeyValuePair<string, (TraceLevel, TimeSpan)>[] times;

        lock (TimeLocker)
        {
            times = [.. group._registeredTimes];
        }

        foreach (var time in times)
            AddTime($"{group._title} - {time.Key}", time.Value.Item2, time.Value.Item1);
    }

    private void TraceTimes()
    {
        var stars = new string('*', 20);
        GroupLogAction[_traceLevel]($"{stars} {_title} {stars}");

        foreach (var item in _registeredTimes)
        {
            DisplayTime(item);
        }

        GroupLogAction[_traceLevel]($"Total Time : {TotalTime}");
        GroupLogAction[_traceLevel]($"{stars} {_title} {stars}");
    }
}
