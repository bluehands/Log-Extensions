using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;


namespace Bluehands.Diagnostics.LogExtensions
{
    public class Log<T> : Log
    {
        public Log() : base(typeof(T))
        {
        }
    }

    public class Log : ILogger
    {
        private readonly ILogMessageWriter m_LogMessageWriter;

        public Log(string messageCreatorName)
        {
            m_LogMessageWriter = new NLogMessageWriter(messageCreatorName);
        }
        public Log(Type messageCreator)
        {
            m_LogMessageWriter = new NLogMessageWriter(messageCreator);
        }

        public static Log For<T>()
        {
            return new Log<T>();
        }

        public static Log For(Type type)
        {
            return new Log(type);
        }

        // ReSharper disable ExplicitCallerInfoArgument
        public IDisposable AutoTrace(string message = "", [CallerMemberName] string caller = "", params KeyValuePair<string, string>[] customProperties)
        {
            return AutoTrace(() => message, caller, customProperties);
        }

        public IDisposable AutoTrace(Func<string> messageFactory, [CallerMemberName] string caller = "", params KeyValuePair<string, string>[] customProperties)
        {
            if (!IsTraceEnabled)
                return DefaultDisposable.Instance;
            return new AutoTrace(m_LogMessageWriter, messageFactory, caller, customProperties);
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
            m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, messageFactory, caller, ex);
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
            m_LogMessageWriter.WriteLogEntry(LogLevel.Error, messageFactory, caller, ex);
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
            m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, messageFactory, caller, ex);
        }

        public void Write(LogLevel logLevel, Func<string> messageFactory, Exception ex = null, [CallerMemberName] string caller = "", params KeyValuePair<string, string>[] customProperties)
        {
            m_LogMessageWriter.WriteLogEntry(logLevel, messageFactory, caller, ex, customProperties);
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
            m_LogMessageWriter.WriteLogEntry(LogLevel.Info, messageFactory, caller, ex);
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
            m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, messageFactory, caller, ex);
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
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, messageFactory, caller, ex);
        }

        // ReSharper restore ExplicitCallerInfoArgument

        public bool IsTraceEnabled => m_LogMessageWriter.IsTraceEnabled;

        public string Correlation
        {
            get => TrackCorrelation.Correlation;
            set => TrackCorrelation.Correlation = value;
        }


        void ILogger.Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (state is LogEventInfo logEventInfo)
            {
                m_LogMessageWriter.WriteLogEntry(logEventInfo, exception);
            }
            else
            {
                m_LogMessageWriter.WriteLogEntry(logLevel.ToLogExtensionsLogLevel(), () => formatter(state, exception));
            }
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return m_LogMessageWriter.IsTraceEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return m_LogMessageWriter.IsDebugEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return m_LogMessageWriter.IsInfoEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return m_LogMessageWriter.IsWarningEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return m_LogMessageWriter.IsErrorEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return m_LogMessageWriter.IsFatalEnabled;
                case Microsoft.Extensions.Logging.LogLevel.None:
                    return m_LogMessageWriter.IsDisabled;
                default:
                    return true;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return AutoTrace(state.ToString());
        }





    }
}