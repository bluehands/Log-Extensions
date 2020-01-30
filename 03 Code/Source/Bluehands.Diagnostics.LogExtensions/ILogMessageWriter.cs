using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Bluehands.Diagnostics.LogExtensions
{
    internal interface ILogMessageWriter
    {
        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsWarningEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsTraceEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsDisabled { get; }
        void WriteLogEntry(LogLevel logLevel, Func<string> messageFactory, [CallerMemberName] string callerMethodName = null, Exception ex = null, params KeyValuePair<string, string>[] customProperties);
        void WriteLogEntry(LogEventInfo logEventInfo, Exception ex = null);

    }
}
