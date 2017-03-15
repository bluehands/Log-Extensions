using System;
using System.Runtime.CompilerServices;


namespace Bluehands.Repository.Diagnostics.Log
{
	public class Log<T> : Log
	{
		public Log()
			: base(typeof(T))
		{

		}
	}

	public class Log : ILog
    {
        private readonly ILogMessageWriter m_LogMessageWriter;

	    [ThreadStatic] public static int Indent;

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
            return new AutoTrace(this, message);
        }

        public void Fatal(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, message, Indent);
        }

        public void Fatal(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, message, Indent, ex);
        }

		public bool IsFatalEnabled { get { return m_LogMessageWriter.IsFatalEnabled; } }

        public void Error(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, message, Indent);
        }

        public void Error(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, message, Indent, ex);
        }

		public bool IsErrorEnabled { get { return m_LogMessageWriter.IsErrorEnabled; } }

        public void Warning(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, message, Indent);
        }

        public void Warning(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, message, Indent, ex);
        }

		public bool IsWarningEnabled { get { return m_LogMessageWriter.IsWarningEnabled; } }

        public void Info(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, message, Indent, ex);
        }

        public void Info(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, message, Indent);
        }

		public bool IsInfoEnabled { get { return m_LogMessageWriter.IsInfoEnabled; } }

        public void Debug(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, message, Indent);
        }

        public void Debug(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, message, Indent, ex);
        }

		public bool IsDebugEnabled { get { return m_LogMessageWriter.IsDebugEnabled; } }

        public void Trace(string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message, Indent);
        }

        public void Trace(Exception ex, string message)
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message, Indent, ex);
        }

		public bool IsTraceEnabled { get { return m_LogMessageWriter.IsTraceEnabled; } }
    }
}