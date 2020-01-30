using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bluehands.Diagnostics.LogExtensions
{
    abstract class LogMessageWriterBase : ILogMessageWriter
    {

        protected string MessageCreatorFriendlyName { get; }
        protected string MessageCreatorFullName { get; }

        protected LogMessageWriterBase(string messageCreatorFullName)
        {
            try
            {
                var pos = messageCreatorFullName.LastIndexOf(".", StringComparison.Ordinal);
                if (pos > 0)
                {
                    MessageCreatorFriendlyName = messageCreatorFullName.Substring(pos + 1, messageCreatorFullName.Length - pos - 1);
                }
                else
                {
                    MessageCreatorFriendlyName = messageCreatorFullName;
                }
            }
            catch (Exception ex)
            {
                MessageCreatorFriendlyName = messageCreatorFullName;
                Debug.WriteLine(ex);
            }

            MessageCreatorFullName = messageCreatorFullName;
        }
        protected LogMessageWriterBase(Type messageCreator)
        {
            MessageCreatorFriendlyName = messageCreator.GetFriendlyName();
            MessageCreatorFullName = messageCreator.FullName;
        }

        public abstract bool IsFatalEnabled { get; }
        public abstract bool IsErrorEnabled { get; }
        public abstract bool IsWarningEnabled { get; }
        public abstract bool IsInfoEnabled { get; }
        public abstract bool IsTraceEnabled { get; }
        public abstract bool IsDebugEnabled { get; }
        public abstract bool IsDisabled { get; }

        public void WriteLogEntry(LogLevel logLevel, Func<string> messageFactory, string callerMethodName = null, Exception ex = null, params KeyValuePair<string, string>[] customProperties)
        {
            try
            {
                if (string.IsNullOrEmpty(callerMethodName)) { throw new ArgumentNullException(nameof(callerMethodName)); }
                if (messageFactory == null) { throw new ArgumentNullException(nameof(messageFactory)); }

                if (IsLogLevelEnabled(logLevel))
                {
                    var logEventInfo = new LogEventInfo
                    {
                        MessageFactory = messageFactory,
                        Level = logLevel,
                        ClassName = MessageCreatorFriendlyName,
                        TypeName = MessageCreatorFullName,
                        MethodName = callerMethodName,
                        CallContext = TraceStack.CurrentStack(LogFormatters.ContextPartSeparator),
                        Correlation = TrackCorrelation.Correlation,
                        Indent = TraceStack.Indent.ConvertIndentToWhiteSpaces(),
                        CustomProperties = customProperties
                    };
                    WriteLogEntry(logEventInfo, ex);
                }
            }
            catch (Exception exx)
            {
                Debug.WriteLine(exx);
            }
        }
        public abstract void WriteLogEntry(LogEventInfo logEventInfo, Exception ex = null);

        protected bool IsLogLevelEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    return IsFatalEnabled;
                case LogLevel.Error:
                    return IsErrorEnabled;
                case LogLevel.Warning:
                    return IsWarningEnabled;
                case LogLevel.Info:
                    return IsInfoEnabled;
                case LogLevel.Debug:
                    return IsDebugEnabled;
                case LogLevel.Trace:
                    return IsTraceEnabled;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "The requested LogLevel is not supported by NLog!");
            }
        }


    }
}
