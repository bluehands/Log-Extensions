using System;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class AutoTrace : AutoTraceBase, IDisposable
    {
		private static readonly Stopwatch StopWatch = Stopwatch.StartNew();
		private readonly TimeSpan m_StopWatchStarted;

		public AutoTrace(LogMessageWriter logWriter, string message) : base(logWriter, message)
        {
			LogWriter.WriteLogEntry(LogLevel.Trace, Message + " Enter");
			LogMessageWriter.Indent++;
			m_StopWatchStarted = StopWatch.Elapsed;
		}

		public void Dispose()
		{
			LogMessageWriter.Indent--;
			LogWriter.WriteLogEntry(LogLevel.Trace, Message + $" Took {GetFormatedMillisecondsString()}ms. Leave");
		}

		private string GetFormatedMillisecondsString()
		{
			var end = StopWatch.Elapsed - m_StopWatchStarted;
			var miliseconds = end.TotalMilliseconds;

			return $"{miliseconds:0.000}";
		}

	}
}
