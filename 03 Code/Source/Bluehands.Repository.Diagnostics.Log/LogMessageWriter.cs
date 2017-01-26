using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class LogMessageWriter
    {
        private readonly Logger m_NLogLog;
        //private static Logger nLogLog = LogManager.GetCurrentClassLogger();
        private readonly NLogMessageBuilder m_NLogMessageBuilder;

        public LogMessageWriter(Type callerOfLog, Type callerOfLogMessageWriter)
        {
            m_NLogMessageBuilder = new NLogMessageBuilder(callerOfLogMessageWriter);
            m_NLogLog = LogManager.GetLogger(callerOfLog.FullName);
        }

        public void WriteLogEntry(LogLevel logLevel, string message)
        {
            WriteLogEntry(logLevel, message, null);
        }

        public void WriteLogEntry(LogLevel logLevel, string message, Exception ex)
        {
            var logEventInfo = m_NLogMessageBuilder.GetLogEventInfo(logLevel, message, ex);
            m_NLogLog.Log(logEventInfo);
        }
    }
}