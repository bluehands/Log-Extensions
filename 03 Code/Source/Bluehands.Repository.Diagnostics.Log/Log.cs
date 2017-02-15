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
        private int m_Indent;

        public Log(Type messageCreator)
        {
            m_LogMessageWriter = new LogMessageWriter(messageCreator);
        }

        public static Log For(Type type)
        {
            return new Log(type);
        }

        public AutoTrace AutoTrace(string message)
        {
            var autoTrace = new AutoTrace(m_LogMessageWriter, message);
            m_Indent = autoTrace.Indent;

            return autoTrace;
        }


        public void Fatal(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, message, m_Indent);
        }

        public void Fatal(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, message, m_Indent, ex);
        }

        public void Error(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, message, m_Indent);
        }

        public void Error(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, message, m_Indent, ex);
        }

        public void Warning(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, message, m_Indent);
        }

        public void Warning(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, message, m_Indent, ex);
        }

        public void Info(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, message, m_Indent, ex);
        }

        public void Info(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, message, m_Indent);
        }

        public void Debug(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, message, m_Indent);
        }

        public void Debug(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, message, m_Indent, ex);
        }

        public void Trace(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message, m_Indent);
        }

        public void Trace(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message, m_Indent, ex);
        }
    }
}