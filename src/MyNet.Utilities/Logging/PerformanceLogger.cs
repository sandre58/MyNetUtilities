// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MyNet.Utilities.Logging
{
    public sealed class PerformanceLogger : IDisposable
    {
        private static readonly Stack<PerformanceLogger> OperationGroups = new();

        private static readonly object TimeLocker = new();
        private static readonly object CurrentObject = new();

        private static readonly IDictionary<TraceLevel, Action<string>> GroupLogAction = new Dictionary<TraceLevel, Action<string>>
        {
            [TraceLevel.Trace] = x => LogManager.Trace(x),
            [TraceLevel.Debug] = x => LogManager.Debug(x),
            [TraceLevel.Info] = x => LogManager.Info(x),
        };

        private readonly Dictionary<string, (TraceLevel trace, TimeSpan time)> _registeredTimes = [];

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

        internal void AddTime(string key, TimeSpan time, TraceLevel level)
        {
            lock (TimeLocker)
            {
                _registeredTimes[key] = _registeredTimes.TryGetValue(key, out var value) ? (level, value.time + time) : (level, time);
            }
        }

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

        private static void Pop()
        {
            if (OperationGroups.Count > 0)
                _ = OperationGroups.Pop();
        }

        public void Dispose()
        {
            lock (CurrentObject)
            {
                Pop();

                _stopwatch.Stop();

                LogManager.Trace($"END - {_title} : {_stopwatch.Elapsed}");

                if (Current == null)
                    TraceTimes();
                else
                {
                    Current.AddTime(_title, _stopwatch.Elapsed, _traceLevel);
                    Current.AddGroupTime(this);
                }
            }
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

        private static void DisplayTime(KeyValuePair<string, (TraceLevel, TimeSpan)> item) =>
            GroupLogAction[item.Value.Item1]($"{item.Key} - {item.Value.Item2}");
    }
}
