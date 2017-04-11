using System;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    internal class LogMessageWriter
    {
	    private const string ContextDataKey = "feec7c1e-fd19-40d4-a7ac-195df21c6063";

        private readonly Logger m_NLogLog;
        private readonly NLogMessageBuilder m_NLogMessageBuilder;
	    private readonly Type m_MessageCreator;

		
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
					contextData = new LogicalCallContextData(CreateNewGuid());
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
			    contextData = new LogicalCallContextData(CreateNewGuid());
			    CallContext.SetData(ContextDataKey, contextData);
			    return contextData.ContextId;
		    }
	    }

	    public LogMessageWriter(Type messageCreator)
	    {
		    m_MessageCreator = messageCreator;
            m_NLogMessageBuilder = new NLogMessageBuilder(messageCreator.FullName);
            m_NLogLog = LogManager.GetLogger(Guid.NewGuid().ToString());
        }

	    public bool IsFatalEnabled => m_NLogLog.IsFatalEnabled;
	    public bool IsErrorEnabled => m_NLogLog.IsErrorEnabled;
	    public bool IsWarningEnabled => m_NLogLog.IsWarnEnabled;
	    public bool IsInfoEnabled => m_NLogLog.IsInfoEnabled;
	    public bool IsTraceEnabled => m_NLogLog.IsTraceEnabled;
	    public bool IsDebugEnabled => m_NLogLog.IsDebugEnabled;

	    public void WriteLogEntry(LogLevel logLevel, string callerMethodName, string message)
	    {
			WriteLogEntry(logLevel, callerMethodName, message, null);
		}

        public void WriteLogEntry(LogLevel logLevel, string callerMethodName, string message, Exception ex)
        {
            try
            {
	            if (!string.IsNullOrEmpty(callerMethodName) && message != null && IsLogLevelEnabled(logLevel))
	            {
					var logEventInfo = GetLogEventInfo(logLevel, callerMethodName, message, Indent, ex);

					m_NLogLog.Log(logEventInfo);
	            }
            }
            catch (Exception exx)
            {
                Console.WriteLine(exx);
            }
        }

	    private bool IsLogLevelEnabled(LogLevel logLevel)
	    {
		    switch (logLevel)
		    {
				case LogLevel.Fatal:
					return m_NLogLog.IsFatalEnabled;
				case LogLevel.Error:
					return m_NLogLog.IsErrorEnabled; 
			    case LogLevel.Warning:
					return m_NLogLog.IsWarnEnabled;
				case LogLevel.Info:
					return m_NLogLog.IsInfoEnabled; 
			    case LogLevel.Debug:
					 return m_NLogLog.IsDebugEnabled;
				case LogLevel.Trace:
					return m_NLogLog.IsTraceEnabled;
				default:
				    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "The requested LogLevel is not supported by NLog!");
		    }
	    }

        private LogEventInfo GetLogEventInfo(LogLevel logLevel, string callerMethodName, string message, int indent, Exception ex)
        {
	        var callerInfo = new CallerInfo(m_MessageCreator.FullName, m_MessageCreator.Name, callerMethodName, ContextId);

            var logEventInfo = m_NLogMessageBuilder.BuildNLogEventInfo(logLevel, message, ex, callerInfo, indent);
            return logEventInfo;
        }

		private static string CreateNewGuid()
		{
			return Guid.NewGuid().ToString().Substring(0, 8);
		}
	}
}