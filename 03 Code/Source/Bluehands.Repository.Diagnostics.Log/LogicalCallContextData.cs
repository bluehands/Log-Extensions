using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Bluehands.Repository.Diagnostics.Log
{
	[Serializable]
	public class LogicalCallContextData : MarshalByRefObject, ILogicalThreadAffinative
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
