using System;

namespace Bluehands.Repository.Diagnostics.Log
{

	public interface ILog
	{
		AutoTrace AutoTrace(string message);

		void Fatal(string message);

		void Fatal(Exception ex, string message);

		bool IsFatalEnabled { get; }

		void Error(string message);

		void Error(Exception ex, string message);
		
		bool IsErrorEnabled { get; }

		void Warning(string message);

		void Warning(Exception ex, string message);

		bool IsWarningEnabled { get; }

		void Info(Exception ex, string message);

		void Info(string message);

		bool IsInfoEnabled { get; }

		void Debug(string message);

		void Debug(Exception ex, string message);

		bool IsDebugEnabled { get; }

		void Trace(string message);

		void Trace(Exception ex, string message);

		bool IsTraceEnabled { get; }
	}
}