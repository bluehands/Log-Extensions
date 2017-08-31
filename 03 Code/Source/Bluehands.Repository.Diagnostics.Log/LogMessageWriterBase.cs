using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace Bluehands.Repository.Diagnostics.Log
{
    abstract class LogMessageWriterBase : ILogMessageWriter
	{
	    const string ContextDataKey = "feec7c1e-fd19-40d4-a7ac-195df21c6063";

		protected readonly Type m_MessageCreator;

		protected LogMessageWriterBase(Type messageCreator)
		{
			m_MessageCreator = messageCreator;
		}

		public abstract bool IsFatalEnabled { get; }
		public abstract bool IsErrorEnabled { get; }
		public abstract bool IsWarningEnabled { get; }
		public abstract bool IsInfoEnabled { get; }
		public abstract bool IsTraceEnabled { get; }
		public abstract bool IsDebugEnabled { get; }

		public static int Indent
		{
			[PermissionSet(SecurityAction.LinkDemand)]
			get
			{
                var contextData = CallContext.GetData(ContextDataKey) as LogicalCallContextData;
				return contextData?.Indent ?? 0;
			}
			[PermissionSet(SecurityAction.LinkDemand)]
			set
			{
				var contextData = CallContext.GetData(ContextDataKey) as LogicalCallContextData;
				if (contextData == null)
				{
                    contextData = new LogicalCallContextData(CreateShortGuid());
                    CallContext.SetData(ContextDataKey, contextData);
				}
				contextData.Indent = value;
			}
		}

		public static string ContextId
		{
			[PermissionSet(SecurityAction.LinkDemand)]
			get
			{
				var contextData = CallContext.GetData(ContextDataKey) as LogicalCallContextData;
				if (contextData != null)
				{
					return contextData.ContextId;
				}
				contextData = new LogicalCallContextData(CreateShortGuid());
                CallContext.SetData(ContextDataKey, contextData);
				return contextData.ContextId;
			}
		}

		public abstract void WriteLogEntry(LogLevel logLevel, Func<string> messageFactory, string callerMethodName = null, Exception ex = null, params KeyValuePair<string, string>[] customProperties);

		protected abstract bool IsLogLevelEnabled(LogLevel logLevel);

	    static string CreateShortGuid()
		{
			return Guid.NewGuid().ToString().Substring(0, 8);
		}
	}
}
