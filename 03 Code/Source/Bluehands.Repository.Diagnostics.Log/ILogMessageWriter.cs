using System;

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

		void WriteLogEntry(LogLevel logLevel, string message, int indent);
		void WriteLogEntry(LogLevel logLevel, string message, int indent, Exception ex);
	}
}