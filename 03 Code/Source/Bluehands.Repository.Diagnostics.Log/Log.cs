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
            return AutoTrace(() => message, caller);
        }

	    public IDisposable AutoTrace(Func<string> messageFactory, [CallerMemberName] string caller = "")
	    {
		    return new AutoTrace(this, messageFactory, caller);
	    }

        public void Fatal(string message, [CallerMemberName] string caller = "")
        {
            Fatal(() => message, null, caller);
        }

		public void Fatal(Func<string> messageFactory, [CallerMemberName] string caller = "")
		{
			Fatal(messageFactory, null, caller);
		}

		public void Fatal(string message, Exception ex, [CallerMemberName] string caller = "")
        {
            Fatal(() => message, ex, caller);
        }

		public void Fatal(Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
		{
			m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, caller, messageFactory, ex);
		}

		public bool IsFatalEnabled => m_LogMessageWriter.IsFatalEnabled;

	    public void Error(string message, [CallerMemberName] string caller = "")
        {
            Error(() => message, null, caller);
        }

		public void Error(Func<string> messageFactory, [CallerMemberName] string caller = "")
		{
			Error(messageFactory, null, caller);
		}

		public void Error(string message, Exception ex, [CallerMemberName] string caller = "")
        {
            Error(() => message, ex, caller);
        }

		public void Error(Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
		{
			m_LogMessageWriter.WriteLogEntry(LogLevel.Error, caller, messageFactory, ex);
		}

		public bool IsErrorEnabled => m_LogMessageWriter.IsErrorEnabled;

	    public void Warning(string message, [CallerMemberName] string caller = "")
        {
            Warning(() => message, null, caller);
        }

		public void Warning(Func<string> messageFactory, [CallerMemberName] string caller = "")
		{
			Warning(messageFactory, null, caller);
		}

		public void Warning(string message, Exception ex, [CallerMemberName] string caller = "")
        {
            Warning(() => message, ex, caller);
        }

		public void Warning(Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
		{
			m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, caller, messageFactory, ex);
		}

		public bool IsWarningEnabled => m_LogMessageWriter.IsWarningEnabled;

		public void Info(string message, [CallerMemberName] string caller = "")
		{
			Info(() => message, null, caller);
		}

		public void Info(Func<string> messageFactory, [CallerMemberName] string caller = "")
		{
			Info(messageFactory, null, caller);
		}

		public void Info(string message, Exception ex, [CallerMemberName] string caller = "")
        {
           Info(() => message, ex, caller);
        }

		public void Info(Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
		{
			m_LogMessageWriter.WriteLogEntry(LogLevel.Info, caller, messageFactory, ex);
		}

		public bool IsInfoEnabled => m_LogMessageWriter.IsInfoEnabled;

	    public void Debug(string message, [CallerMemberName] string caller = "")
        {
			Debug(() => message, null, caller);
        }

		public void Debug(Func<string> messageFactory, [CallerMemberName] string caller = "")
		{
			Debug(messageFactory, null, caller);
		}

		public void Debug(string message, Exception ex, [CallerMemberName] string caller = "")
        {
            Debug(() => message, ex, caller);
        }

		public void Debug(Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
		{
			m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, caller, messageFactory, ex);
		}

		public bool IsDebugEnabled => m_LogMessageWriter.IsDebugEnabled;

	    public void Trace(string message, [CallerMemberName] string caller = "")
        {
           Trace(() => message, null, caller);
        }

		public void Trace(Func<string> messageFactory, [CallerMemberName] string caller = "")
		{
			Trace(messageFactory, null, caller);
		}

		public void Trace(string message, Exception ex, [CallerMemberName] string caller = "")
        {
            Trace(() => message, ex, caller);
        }

		public void Trace(Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
		{
			m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, caller, messageFactory, ex);
		}

		public bool IsTraceEnabled => m_LogMessageWriter.IsTraceEnabled;
    }
}