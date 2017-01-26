using System;
using System.Collections.Generic;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class NLogMessageBuilder
    {
        private readonly MethodNameExtracter m_MethodNameExtracter;

        public NLogMessageBuilder(Type callerOfLogMessageWriter)
        {
            m_MethodNameExtracter = new MethodNameExtracter(callerOfLogMessageWriter);
        }

        public LogEventInfo GetLogEventInfo(LogLevel logLevel, string message, Exception ex)
        {
            var logEventInfo = BuildLogEventInfo(logLevel, message, ex);

            IDictionary<string, string> callerDictionary = m_MethodNameExtracter.ExtractCallerInfosFromStackTrace();
            foreach (var caller in callerDictionary)
            {
                logEventInfo.Properties[caller.Key] = caller.Value;
            }
            return logEventInfo;
        }

        private static LogEventInfo BuildLogEventInfo(LogLevel logLevel, string message, Exception ex)
        {
            var logEventInfo = new LogEventInfo();

            logEventInfo.Level = GetNLogLevel(logLevel);
            logEventInfo.Message = message;

            logEventInfo.Properties["test"] = "hallotest";
            if (ex != null)
            {
                logEventInfo.Exception = ex;
            }
            return logEventInfo;
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
