using System;
using System.Runtime.CompilerServices;

namespace Bluehands.Repository.Diagnostics.Log
{
	internal interface ILogMessageWriter
	{
		bool IsFatalEnabled { get; }
		bool IsErrorEnabled { get; }
		bool IsWarningEnabled { get; }
		bool IsInfoEnabled { get; }
		bool IsTraceEnabled { get; }
		bool IsDebugEnabled { get; }

		void WriteLogEntry(LogLevel logLevel, Func<string> messageFactory, [CallerMemberName] string callerMethodName = null, Exception ex = null);

	}
}
