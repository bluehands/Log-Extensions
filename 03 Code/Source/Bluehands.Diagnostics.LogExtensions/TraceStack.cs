using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace Bluehands.Diagnostics.LogExtensions
{
    public static class TraceStack
    {
        static readonly AsyncLocal<ImmutableStack<string>> s_AsyncLocalString = new AsyncLocal<ImmutableStack<string>>();

        private static ImmutableStack<string> CurrentContext
        {
            get => s_AsyncLocalString.Value ?? ImmutableStack.Create<string>();
            set => s_AsyncLocalString.Value = value;
        }

        public static int Indent => CurrentContext.Count();

        public static string CurrentStack(string separator = "_") => string.Join(separator, CurrentContext.Reverse());

        public static IDisposable Push(string context)
        {
            CurrentContext = CurrentContext.Push(context);
            return new PopWhenDisposed();
        }

        static void Pop()
        {
            CurrentContext = CurrentContext.Pop();
        }

        sealed class PopWhenDisposed : IDisposable
        {
            bool m_Disposed;

            public PopWhenDisposed()
            {
            }

            public void Dispose()
            {
                if (m_Disposed)
                {
                    return;
                }
                Pop();
                m_Disposed = true;
            }
        }
    }
}