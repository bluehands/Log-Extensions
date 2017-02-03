using System;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class Log<T> : Log
    {
        public Log()
            : base(typeof(T))
        {

        }
    }

    public class Log
    {
        private readonly LogMessageWriter m_LogMessageWriter;

        public Log(Type messageCreator)
        {
            m_LogMessageWriter = new LogMessageWriter(messageCreator, typeof(Log));
        }

        public static Log For(Type type)
        {
            return new Log(type);
        }

        public void Fatal(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, message);
        }

        public void Fatal(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, message, ex);
        }

        public void Error(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, message);
        }

        public void Error(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, message, ex);
        }

        public void Warning(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, message);
        }

        public void Warning(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, message, ex);
        }

        public void Info(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, message, ex);
        }

        public void Info(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, message);
        }

        public void Debug(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, message);
        }

        public void Debug(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, message, ex);
        }

        public void Trace(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message);
        }

        public void Trace(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message, ex);
        }
    }
}