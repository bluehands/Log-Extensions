using System;
using System.Collections.Generic;

namespace Bluehands.Repository.Diagnostics.Log
{
    abstract class LogMessageWriterBase : ILogMessageWriter
	{

		protected readonly Type m_MessageCreator;
	    protected readonly string m_MessageCreatorFriendlyName;

		protected LogMessageWriterBase(Type messageCreator)
		{
			m_MessageCreator = messageCreator;
		    m_MessageCreatorFriendlyName = messageCreator.GetFriendlyName();
		}

		public abstract bool IsFatalEnabled { get; }
		public abstract bool IsErrorEnabled { get; }
		public abstract bool IsWarningEnabled { get; }
		public abstract bool IsInfoEnabled { get; }
		public abstract bool IsTraceEnabled { get; }
		public abstract bool IsDebugEnabled { get; }


		public abstract void WriteLogEntry(LogLevel logLevel, Func<string> messageFactory, string callerMethodName = null, Exception ex = null, params KeyValuePair<string, string>[] customProperties);

		protected abstract bool IsLogLevelEnabled(LogLevel logLevel);

	}
}
