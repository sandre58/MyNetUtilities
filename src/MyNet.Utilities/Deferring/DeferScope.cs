// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Utilities.Deferring
{
    internal sealed class DeferScope : IDisposable
    {
        private readonly Deferrer _deferrerScopeBase;

        public DeferScope(Deferrer sender)
        {
            _deferrerScopeBase = sender;

            _deferrerScopeBase.Push(this);
        }

        public void Dispose()
        {
            _deferrerScopeBase.Pop();

            _deferrerScopeBase.EndDefer();
        }
    }
}
