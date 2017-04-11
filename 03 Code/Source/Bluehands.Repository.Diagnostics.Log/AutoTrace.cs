using System;
using System.Diagnostics;
using System.Globalization;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class AutoTrace : IDisposable
    {
		private readonly Log m_Log;
		private readonly string m_Message;
	    private readonly Stopwatch m_StopWatch;

		private string m_MethodId;

		public AutoTrace(Log log, string message)
		{
			if (log == null) { throw new ArgumentNullException(nameof(log));}

			m_Log = log;
			m_Message = message;
			m_StopWatch = Stopwatch.StartNew();

			m_Log.Trace("Enter");
			LogMessageWriter.Indent++;
		}

	    public void Dispose()
		{
			m_StopWatch.Stop();
			LogMessageWriter.Indent--;
			m_Log.Trace(m_Message + $" [{ m_StopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}ms Leave]");
		}
	}
}
