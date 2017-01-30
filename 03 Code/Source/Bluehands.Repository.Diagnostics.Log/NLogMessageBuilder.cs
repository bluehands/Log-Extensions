using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public sealed class NLogMessageBuilder
    {
        private readonly MethodNameExtracter m_MethodNameExtracter;

        public NLogMessageBuilder(Type groundType)
        {
            if (groundType == null)
            {
                throw new ArgumentNullException(nameof(groundType));
            }
            m_MethodNameExtracter = new MethodNameExtracter(groundType);
        }

        public LogEventInfo GetLogEventInfo(LogLevel logLevel, string message, Type callerOfGround, Exception ex)
        {
            var logEventInfo = BuildNLogEventInfo(logLevel, message, callerOfGround, ex);
            return logEventInfo;
        }

        private LogEventInfo BuildNLogEventInfo(LogLevel logLevel, string message, Type callerOfGround, Exception ex)
        {
            var logEventInfo = new LogEventInfo
            {
                Message = message,
                Level = GetNLogLevel(logLevel),
                LoggerName = callerOfGround.FullName
            };
            SetNLogProperties(logEventInfo);

            if (ex != null)
            {
                logEventInfo.Exception = ex;
            }
            return logEventInfo;
        }

        private void SetNLogProperties(LogEventInfo logEventInfo)
        {
            var callerInfo = m_MethodNameExtracter.ExtractCallerInfoFromStackTrace();
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
