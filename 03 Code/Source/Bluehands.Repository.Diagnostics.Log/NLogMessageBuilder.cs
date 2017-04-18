using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    internal sealed class NLogMessageBuilder
    {
        private readonly string m_LoggerName;

        public NLogMessageBuilder(string loggerName)
        {
	        if (string.IsNullOrEmpty(loggerName))
	        {
		        throw new ArgumentNullException(nameof(loggerName));
	        }

	        m_LoggerName = loggerName;
        }

        public LogEventInfo BuildLogEventInfo(LogLevel logLevel, string message, Exception ex, CallerInfo callerInfo, int indent)
        {
            var logEventInfo = new LogEventInfo
            {
                Message = message,
                Level = GetNLogLevel(logLevel),
                LoggerName = m_LoggerName
            };
            SetNLogProperties(logEventInfo, callerInfo);
            logEventInfo.Properties["indent"] = ConvertIndentToWhiteSpaces(indent);

            if (ex != null)
            {
                logEventInfo.Exception = ex;
            }
            return logEventInfo;
        }

        private static void SetNLogProperties(LogEventInfo logEventInfo, CallerInfo callerInfo)
        {
	        if (logEventInfo == null) { throw new ArgumentNullException(nameof(logEventInfo)); }
	        if (callerInfo == null) { throw new ArgumentNullException(nameof(callerInfo)); }

			logEventInfo.Properties["typeOfMessageCreator"] = callerInfo.TypeOfMessageCreator;
            logEventInfo.Properties["classOfMessageCreator"] = callerInfo.ClassOfMessageCreator;
            logEventInfo.Properties["callerMethodName"] = callerInfo.CallerMethodName;
	        logEventInfo.Properties["callerContextId"] = callerInfo.CallerContextId;
        }


        private static string ConvertIndentToWhiteSpaces(int indent)
        {
            var whiteSpaces = "";

	        for (var i = 0; i < indent; i++)
	        {
		        whiteSpaces += "\t";
	        }

	        return whiteSpaces;
        }

        private static NLog.LogLevel GetNLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Warning:
                    return NLog.LogLevel.Warn;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                default:
                    return NLog.LogLevel.Trace;
            }
        }
    }
}
