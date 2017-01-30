using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class LogMessageWriter
    {
        private readonly Logger m_NLogLog;
        //private static Logger nLogLog = LogManager.GetCurrentClassLogger();
        private readonly NLogMessageBuilder m_NLogMessageBuilder;
        private readonly Type m_CallerOfGround;

        public LogMessageWriter(Type callerOfGround, Type ground)
        {
            m_NLogMessageBuilder = new NLogMessageBuilder(ground);
            m_NLogLog = LogManager.GetLogger(Guid.NewGuid().ToString());
            m_CallerOfGround = callerOfGround;
        }

        public void WriteLogEntry(LogLevel logLevel, string message)
        {
            WriteLogEntry(logLevel, message, null);
        }

        public void WriteLogEntry(LogLevel logLevel, string message, Exception ex)
        {
            try
            {
                var logEventInfo = m_NLogMessageBuilder.GetLogEventInfo(logLevel, message, m_CallerOfGround, ex);
                m_NLogLog.Log(logEventInfo);
            }
            catch (Exception exx)
            {
                Console.WriteLine(exx);
            }
        }
    }
}