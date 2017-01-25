using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class LogMessageBuilder
    {
        private readonly Type m_Caller;
        public LogMessageBuilder(Type caller)
        {
            m_MessageNameOfCaller = string.Empty;
            m_MessageNameOfCallerOfTheCaller = string.Empty;

            m_Caller = caller;
        }


        private string m_MessageNameOfCaller;
        private string m_MessageNameOfCallerOfTheCaller;


        public LogEventInfo GetLogEventInfo(LogLevel logLevel, string message, Exception ex)
        {
            var fullTypeName = GetTypeName();
            var methodNameExtracter = new MethodNameExtracter();
            methodNameExtracter.ExtractMethodNameFromStackTrace();
            m_MessageNameOfCaller = methodNameExtracter.CallerOfLogMessageWriter;
            m_MessageNameOfCallerOfTheCaller = methodNameExtracter.CallerOfTheCallerOfLogMessageWriter;


            var bothCallers = fullTypeName + "." + m_MessageNameOfCallerOfTheCaller + "->" + m_MessageNameOfCaller;

            var logLevelNLog = GetNLogLevel(logLevel);
            var logEventInfo = new LogEventInfo(logLevelNLog, bothCallers, message);
            if (ex != null)
            {
                logEventInfo.Exception = ex;
            }
            return logEventInfo;
        }

        private string GetTypeName()
        {
            var fullTypeName = string.Empty;
            try
            {
                if (m_Caller != null)
                {
                    fullTypeName = m_Caller.FullName;
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex);
            }
            return fullTypeName;
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
