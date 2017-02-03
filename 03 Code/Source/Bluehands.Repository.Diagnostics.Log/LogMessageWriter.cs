using System;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class LogMessageWriter
    {
        private readonly MethodNameExtracter m_MethodNameExtracter;
        private readonly Logger m_NLogLog;
        private readonly NLogMessageBuilder m_NLogMessageBuilder;
        
        public LogMessageWriter(Type messageCreator)
        {
            m_MethodNameExtracter = new MethodNameExtracter(messageCreator);
            m_NLogMessageBuilder = new NLogMessageBuilder(messageCreator.FullName);
            m_NLogLog = LogManager.GetLogger(Guid.NewGuid().ToString(), messageCreator);
        }

        public void WriteLogEntry(LogLevel logLevel, string message)
        {
            WriteLogEntry(logLevel, message, null);
        }

        public void WriteLogEntry(LogLevel logLevel, string message, Exception ex)
        {
            try
            {
                if (message != null)
                {
                    var logEventInfo = GetLogEventInfo(logLevel, message, ex);

                    m_NLogLog.Log(logEventInfo);
                }
            }
            catch (Exception exx)
            {
                Console.WriteLine(exx);
            }
        }

        private LogEventInfo GetLogEventInfo(LogLevel logLevel, string message, Exception ex)
        {
            var callerInfo = m_MethodNameExtracter.ExtractCallerInfoFromStackTrace();

            var logEventInfo = m_NLogMessageBuilder.BuildNLogEventInfo(logLevel, message, ex, callerInfo);
            return logEventInfo;
        }
    }
}