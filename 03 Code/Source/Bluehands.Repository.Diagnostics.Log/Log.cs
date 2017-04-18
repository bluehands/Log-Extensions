using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;


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
        private readonly ILogMessageWriter m_LogMessageWriter;

        public Log(Type messageCreator)
        {
            m_LogMessageWriter = new LogMessageWriter(messageCreator);
        }

        public static Log For(Type type)
        {
            return new Log(type);
        }


        public IDisposable AutoTrace(string message, [CallerMemberName] string caller = "")
        {
            return new AutoTrace(this, message);
        }

        public void Fatal(string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, caller, message);
        }

        public void Fatal(Exception ex, string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, caller, message, ex);
        }

		public bool IsFatalEnabled => m_LogMessageWriter.IsFatalEnabled;

	    public void Error(string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, caller, message);
        }

        public void Error(Exception ex, string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, caller, message, ex);
        }

		public bool IsErrorEnabled => m_LogMessageWriter.IsErrorEnabled;

	    public void Warning(string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, caller, message);
        }

        public void Warning(Exception ex, string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, caller, message, ex);
        }

		public bool IsWarningEnabled => m_LogMessageWriter.IsWarningEnabled;

	    public void Info(Exception ex, string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, caller, message, ex);
        }

        public void Info(string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, caller, message);
        }

		public bool IsInfoEnabled => m_LogMessageWriter.IsInfoEnabled;

	    public void Debug(string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, caller, message);
        }

        public void Debug(Exception ex, string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, caller, message, ex);
        }

		public bool IsDebugEnabled => m_LogMessageWriter.IsDebugEnabled;

	    public void Trace(string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, caller, message);
        }

        public void Trace(Exception ex, string message, [CallerMemberName] string caller = "")
        {
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, caller, message, ex);
        }

		public bool IsTraceEnabled => m_LogMessageWriter.IsTraceEnabled;
    }
}