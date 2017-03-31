using System;
using System.Diagnostics.CodeAnalysis;


namespace Bluehands.Repository.Diagnostics.Log
{
	[ExcludeFromCodeCoverage]
	public class Log<T> : Log
	{
		public Log()
			: base(typeof(T))
		{

		}
	}

	[ExcludeFromCodeCoverage]
	public class Log
    {
        private readonly LogMessageWriter m_LogMessageWriter;

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
            return new AutoTrace(m_LogMessageWriter, message);
        }

        public void Fatal(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, message);
        }

        public void Fatal(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, message, ex);
        }

		public bool IsFatalEnabled { get { return m_LogMessageWriter.IsFatalEnabled; } }

        public void Error(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, message);
        }

        public void Error(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, message, ex);
        }

		public bool IsErrorEnabled { get { return m_LogMessageWriter.IsErrorEnabled; } }

        public void Warning(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, message);
        }

        public void Warning(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, message, ex);
        }

		public bool IsWarningEnabled { get { return m_LogMessageWriter.IsWarningEnabled; } }

        public void Info(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, message, ex);
        }

        public void Info(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, message);
        }

		public bool IsInfoEnabled { get { return m_LogMessageWriter.IsInfoEnabled; } }

        public void Debug(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, message);
        }

        public void Debug(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, message, ex);
        }

		public bool IsDebugEnabled { get { return m_LogMessageWriter.IsDebugEnabled; } }

        public void Trace(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message);
        }

        public void Trace(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message, ex);
        }

		public bool IsTraceEnabled { get { return m_LogMessageWriter.IsTraceEnabled; } }
    }
}