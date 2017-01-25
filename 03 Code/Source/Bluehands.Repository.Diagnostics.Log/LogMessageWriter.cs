using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class LogMessageWriter
    {
        private readonly Logger m_NLogLog = LogManager.GetLogger("LoggerName");
        //private static Logger nLogLog = LogManager.GetCurrentClassLogger();
        private readonly LogMessageBuilder m_LogMessageBuilder;

        public LogMessageWriter(Type caller)
        {
            m_LogMessageBuilder = new LogMessageBuilder(caller);
        }

        public void WriteLogEntry(LogLevel logLevel, string message)
        {
            WriteLogEntry(logLevel, message, null);
        }

        public void WriteLogEntry(LogLevel logLevel, string message, Exception ex)
        {
            var logEventInfo = m_LogMessageBuilder.GetLogEventInfo(logLevel, message, ex);
            m_NLogLog.Log(logEventInfo);
        }
    }
}