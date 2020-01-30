using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Bluehands.Diagnostics.LogExtensions
{
    internal class MicrosoftExtensionsLoggingLogMessageWriter<T> : LogMessageWriterBase
    {
        private readonly ILogger<T> m_Logger;

        public MicrosoftExtensionsLoggingLogMessageWriter(ILogger<T> logger) : base(typeof(T))
        {
            try
            {
                m_Logger = logger;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public override bool IsFatalEnabled
        {
            get
            {
                try
                {
                    return m_Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Critical);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override bool IsErrorEnabled
        {
            get
            {
                try
                {
                    return m_Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Error);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override bool IsWarningEnabled
        {
            get
            {
                try
                {
                    return m_Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Warning);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override bool IsInfoEnabled
        {
            get
            {
                try
                {
                    return m_Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Information);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override bool IsTraceEnabled
        {
            get
            {
                try
                {
                    return m_Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Trace);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override bool IsDebugEnabled
        {
            get
            {
                try
                {
                    return m_Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Debug);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override bool IsDisabled
        {
            get
            {
                try
                {
                    return m_Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.None);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override void WriteLogEntry(LogEventInfo logEventInfo, Exception ex = null)
        {
            try
            {
                m_Logger.Log(GetMicrosoftExtensionLoggingLogLevel(logEventInfo.Level), new EventId(0), logEventInfo, ex, DefaultFormatter);
            }
            catch (Exception exx)
            {
                Debug.WriteLine(exx);
            }
        }

        private static string DefaultFormatter(LogEventInfo logEventInfo, Exception ex)
        {
            var msg = $"{logEventInfo.Correlation} {logEventInfo.Indent} {logEventInfo.CallContext} {logEventInfo.TypeName} {logEventInfo.ClassName}:{logEventInfo.MethodName} {logEventInfo.MessageFactory()} {ex?.ToString()}";
            return msg;
        }

        private static Microsoft.Extensions.Logging.LogLevel GetMicrosoftExtensionLoggingLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    return Microsoft.Extensions.Logging.LogLevel.Critical;
                case LogLevel.Error:
                    return Microsoft.Extensions.Logging.LogLevel.Error;
                case LogLevel.Warning:
                    return Microsoft.Extensions.Logging.LogLevel.Warning;
                case LogLevel.Info:
                    return Microsoft.Extensions.Logging.LogLevel.Information;
                case LogLevel.Debug:
                    return Microsoft.Extensions.Logging.LogLevel.Debug;
                case LogLevel.Trace:
                    return Microsoft.Extensions.Logging.LogLevel.Trace;
                default:
                    return Microsoft.Extensions.Logging.LogLevel.Trace;
            }
        }
    }
}