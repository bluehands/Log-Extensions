using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public sealed class NLogMessageBuilder
    {
        //private readonly MethodNameExtracter m_MethodNameExtracter;
        private readonly string m_LoggerName;
        
        public NLogMessageBuilder(string loggerName)
        {
            if (loggerName != null)
            {
                m_LoggerName = loggerName;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public LogEventInfo BuildNLogEventInfo(LogLevel logLevel, string message, Exception ex, CallerInfo callerInfo)
        {
            var logEventInfo = new LogEventInfo
            {
                Message = message,
                Level = GetNLogLevel(logLevel),
                LoggerName = m_LoggerName
            };
            SetNLogProperties(logEventInfo, callerInfo);

            if (ex != null)
            {
                logEventInfo.Exception = ex;
            }
            return logEventInfo;
        }

        private void SetNLogProperties(LogEventInfo logEventInfo, CallerInfo callerInfo)
        {
            logEventInfo.Properties["namespace"] = callerInfo.TypeOfCallerOfGround;
            logEventInfo.Properties["class"] = callerInfo.ClassNameOfGround;
            logEventInfo.Properties["method"] = callerInfo.MethodNameOfCallerOfGround;
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
