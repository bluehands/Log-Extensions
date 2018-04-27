using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Bluehands.Repository.Diagnostics.Log
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
            Trace.WriteLine("Pushing");
            CurrentContext = CurrentContext.Push(context);
            return new PopWhenDisposed();
        }

        static void Pop()
        {
            Trace.WriteLine("Popping");
            CurrentContext = CurrentContext.Pop();
            Trace.WriteLine("Popped");
        }

        sealed class PopWhenDisposed : IDisposable
        {
            bool m_Disposed;

            public PopWhenDisposed()
            {
                Trace.WriteLine($"Handle create: {GetHashCode()}");
            }

            public void Dispose()
            {
                Trace.WriteLine($"Disposing {GetHashCode()}");
                if (m_Disposed)
                {
                    Trace.WriteLine($"Dispose {GetHashCode()} aborted");
                    return;
                }
                Pop();
                m_Disposed = true;
                Trace.WriteLine($"Disposed {GetHashCode()}");
            }
        }
    }
}