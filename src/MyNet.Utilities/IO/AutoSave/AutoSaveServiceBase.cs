// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using MyNet.Utilities.Exceptions;
using MyNet.Utilities.Logging;

namespace MyNet.Utilities.IO.AutoSave
{
    public abstract class AutoSaveServiceBase : IAutoSaveService, IDisposable
    {
        private sealed class Suspender : IDisposable
        {
            private readonly bool _savedIsSuspended;

            private AutoSaveServiceBase Service { get; }

            public Suspender(AutoSaveServiceBase service)
            {
                _savedIsSuspended = service.IsSuspended;
                service.Cancel();
                service.IsSuspended = true;

                Service = service;
            }

            public void Dispose() => Service.IsSuspended = _savedIsSuspended;
        }

        private readonly System.Timers.Timer _timer = new();
        private CancellationTokenSource? _autoSaveAsyncTokenSource;
        private bool _disposedValue;

        public bool IsEnabled { get; private set; }

        public bool IsSaving { get; private set; }

        public bool IsSuspended { get; private set; }

        public int Interval => (int)(_timer.Interval / 1000);

        protected AutoSaveServiceBase(bool isEnabled = true, int intervalInSeconds = 300)
        {
            SetInterval(intervalInSeconds);
            if (isEnabled)
                Enable();

            _timer.Elapsed += OnTimerElapsedAsync;
        }

        public void SetInterval(int intervalInSeconds) => _timer.Interval = intervalInSeconds * 1000;

        public void Enable()
        {
            if (IsEnabled) return;

            IsEnabled = true;
            Start();
        }

        public void Disable()
        {
            if (!IsEnabled) return;

            IsEnabled = false;
            Stop();
        }

        public virtual void Start()
        {
            if (!IsEnabled) return;

            _autoSaveAsyncTokenSource = new CancellationTokenSource();
            _timer.Start();
        }

        public virtual void Stop()
        {
            if (!IsEnabled) return;

            Cancel();
            _timer.Stop();
        }

        public virtual void Cancel() => _autoSaveAsyncTokenSource?.Cancel();

        public IDisposable Suspend() => new Suspender(this);

        protected virtual async Task LauchSaveAsync()
        {
            if (IsEnabled && !IsSuspended)
            {
                _timer.Stop();
                await SaveAsync().ConfigureAwait(false);
            }
        }

        private async Task SaveAsync()
        {
            IsSaving = true;

            try
            {
                _ = await SaveCoreAsync(_autoSaveAsyncTokenSource?.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Save cancellation do nothing
            }
            catch (TranslatableException ex)
            {
                LogManager.Warning(ex.Message);
            }
            catch (Exception e)
            {
                LogManager.Error(e);
            }
            finally
            {
                IsSaving = false;
            }

            Start();
        }

        protected abstract Task<bool> SaveCoreAsync(CancellationToken? cancellationToken = null);

        private async void OnTimerElapsedAsync(object? sender, EventArgs e) => await LauchSaveAsync().ConfigureAwait(false);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _autoSaveAsyncTokenSource?.Dispose();

                    _timer.Elapsed -= OnTimerElapsedAsync;
                    _timer.Dispose();
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
