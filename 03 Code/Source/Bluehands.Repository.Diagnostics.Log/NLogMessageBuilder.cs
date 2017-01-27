using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public sealed class NLogMessageBuilder
    {
        private static MethodNameExtracter s_MethodNameExtracter;

        public NLogMessageBuilder(Type callerOfLogMessageWriter)
        {
            s_MethodNameExtracter = new MethodNameExtracter(callerOfLogMessageWriter);
        }

        public LogEventInfo GetLogEventInfo(LogLevel logLevel, string message, Type callerOfLog, Exception ex)
        {
            var logEventInfo = BuildNLogEventInfo(logLevel, message, callerOfLog, ex);
            return logEventInfo;
        }

        private static LogEventInfo BuildNLogEventInfo(LogLevel logLevel, string message, Type callerOfLog, Exception ex)
        {
            var logEventInfo = new LogEventInfo
            {
                Message = message,
                Level = GetNLogLevel(logLevel),
                LoggerName = callerOfLog.FullName
            };
            SetNLogProperties(logEventInfo);

            if (ex != null)
            {
                logEventInfo.Exception = ex;
            }
            return logEventInfo;
        }

        private static void SetNLogProperties(LogEventInfo logEventInfo)
        {
            var callerInfo = s_MethodNameExtracter.ExtractCallerInfoFromStackTrace();
            logEventInfo.Properties["namespace"] = callerInfo.NamespaceOfCallerOfLog;
            logEventInfo.Properties["class"] = callerInfo.ClassNameOfLog;
            logEventInfo.Properties["method"] = callerInfo.MethodNameOfCallerOfLog;
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
