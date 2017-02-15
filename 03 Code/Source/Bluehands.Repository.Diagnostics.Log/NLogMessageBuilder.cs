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

        public LogEventInfo BuildNLogEventInfo(LogLevel logLevel, string message, Exception ex, CallerInfo callerInfo, int indent)
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

        private void SetNLogProperties(LogEventInfo logEventInfo, CallerInfo callerInfo)
        {
            logEventInfo.Properties["typeOfMessageCreator"] = callerInfo.TypeOfMessageCreator;
            logEventInfo.Properties["classOfMessageCreator"] = callerInfo.ClassOfMessageCreator;
            logEventInfo.Properties["methodOfMessageCreator"] = callerInfo.MethodNameOfMessageCreator;
        }


        private static string ConvertIndentToWhiteSpaces(int indent)
        {
            var whiteSpaces = "";

            switch (indent)
            {
                case 1:
                    return " ";
                case 2:
                    return "  ";
                case 3:
                    return "   ";
                case 4:
                    return "    ";
                case 5:
                    return "     ";
                case 6:
                    return "      ";
                case 7:
                    return "       ";
                case 8:
                    return "        ";
                case 9:
                    return "         ";
                case 10:
                    return "          ";
                default:
                    for (var i = 0; i < indent; i++)
                    {
                        whiteSpaces += " ";
                    }
                    return whiteSpaces;
            }
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
