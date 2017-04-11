using System;
using System.Runtime.Remoting.Messaging;

namespace Bluehands.Repository.Diagnostics.Log
{
	[Serializable]
	internal class LogicalCallContextData : MarshalByRefObject, ILogicalThreadAffinative
	{
		public int Indent { get; set; }

		public string ContextId { get; set; }

		public LogicalCallContextData(string contextId)
		{
			Indent = 0;
			ContextId = contextId;
		}

	}
}
