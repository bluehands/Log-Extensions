using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class LogMessageWriter
    {
        private readonly MethodNameExtracter m_MethodNameExtracter;
        private readonly Logger m_NLogLog;
        private readonly NLogMessageBuilder m_NLogMessageBuilder;

		[ThreadStatic] public static int Indent;

		public LogMessageWriter(Type messageCreator)
        {
            m_MethodNameExtracter = new MethodNameExtracter(messageCreator);
            m_NLogMessageBuilder = new NLogMessageBuilder(messageCreator.FullName);
            m_NLogLog = LogManager.GetLogger(Guid.NewGuid().ToString());
        }

	    public bool IsFatalEnabled { get { return m_NLogLog.IsFatalEnabled; } }
		public bool IsErrorEnabled { get { return m_NLogLog.IsErrorEnabled; } }
		public bool IsWarningEnabled { get { return m_NLogLog.IsWarnEnabled; } }
		public bool IsInfoEnabled { get { return m_NLogLog.IsInfoEnabled; } }
		public bool IsTraceEnabled { get { return m_NLogLog.IsTraceEnabled; } }
		public bool IsDebugEnabled { get { return m_NLogLog.IsDebugEnabled; } }

	    public void WriteLogEntry(LogLevel logLevel, string message)
	    {
			WriteLogEntry(logLevel, message, null);
		}

        public void WriteLogEntry(LogLevel logLevel, string message, Exception ex)
        {
            try
            {
	            if (message != null && IsLogLevelEnabled(logLevel))
	            {
					var logEventInfo = GetLogEventInfo(logLevel, message, Indent, ex);

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


        private LogEventInfo GetLogEventInfo(LogLevel logLevel, string message, int indent, Exception ex)
        {
            var callerInfo = m_MethodNameExtracter.ExtractCallerInfoFromStackTrace();

            var logEventInfo = m_NLogMessageBuilder.BuildNLogEventInfo(logLevel, message, ex, callerInfo, indent);
            return logEventInfo;
        }
    }
}