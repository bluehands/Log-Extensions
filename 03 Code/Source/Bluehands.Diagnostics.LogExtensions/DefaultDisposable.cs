using System;

namespace Bluehands.Diagnostics.LogExtensions
{
    sealed class DefaultDisposable : IDisposable
    {
        public static readonly DefaultDisposable Instance = new DefaultDisposable();

        DefaultDisposable()
        {
        }

        public void Dispose()
        {
        }
    }
}