using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Bluehands.Repository.Diagnostics.Log
{
	public static class ImmutableContextStack
	{
		private static readonly string s_Name = Guid.NewGuid().ToString("N");

		sealed class Wrapper : MarshalByRefObject
		{
			public ImmutableStack<string> Value { get; set; }
		}

		public static string CurrentStack => string.Join("->", CurrentContext.Reverse());

		public static ImmutableStack<string> CurrentContext
		{
			get
			{
				var ret = CallContext.LogicalGetData(s_Name) as Wrapper;
				return ret == null ? ImmutableStack.Create<string>() : ret.Value;
			}
			set
			{
				CallContext.LogicalSetData(s_Name, new Wrapper { Value = value });
			}
		}

		public static IDisposable Push(string caller)
		{
			CurrentContext = CurrentContext.Push(caller);
			return new PopWhenDisposed();
		}

		public static void Pop()
		{
			CurrentContext = CurrentContext.Pop();
		}

		sealed class PopWhenDisposed : IDisposable
		{
			bool m_Disposed;

			public void Dispose()
			{
				if (m_Disposed) { return;}
				Pop();
				m_Disposed = true;
			}
		}


	}
}
