using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		void WriteLogEntry(LogLevel logLevel, string callerMethodName, string message);
		void WriteLogEntry(LogLevel logLevel, string callerMethodName, string message, Exception ex);

	}
}
