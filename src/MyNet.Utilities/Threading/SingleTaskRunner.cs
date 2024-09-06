// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using MyNet.Utilities.Logging;

namespace MyNet.Utilities.Threading
{
    public class SingleTaskRunner(Action<CancellationToken> action, Action<bool>? onRunningChanged = null, Action? onCancelled = null, ILogger? logger = null) : IDisposable
    {
        private readonly object _taskLocker = new();
        private readonly Action<CancellationToken> _actionToRun = action;
        private readonly Action<bool>? _onRunningChanged = onRunningChanged;
        private readonly Action? _onCancelled = onCancelled;
        private volatile bool _isRunning;
        private readonly ILogger? _log = logger;

        private CancellationTokenSource? _tokenSource;
        private bool _disposedValue;

        public bool IsRunning => _isRunning;

        private void RunSimpleTask()
        {
            _tokenSource = new CancellationTokenSource();
            try
            {
                _actionToRun(_tokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                _onCancelled?.Invoke();
            }
        }

        public void Cancel() => _tokenSource?.Cancel();

        public void Run()
        {
            lock (_taskLocker)
            {
                if (_isRunning) return;

                _isRunning = true;
                _onRunningChanged?.Invoke(_isRunning);
            }

            _ = Task.Run(() =>
              {
                  try
                  {
                      RunSimpleTask();
                  }
                  catch (Exception ex)
                  {
                      _log?.Error(ex);
                  }
                  finally
                  {
                      lock (_taskLocker)
                      {
                          _isRunning = false;
                          _onRunningChanged?.Invoke(_isRunning);
                      }
                  }
              });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _tokenSource?.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
