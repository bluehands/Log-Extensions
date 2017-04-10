using System;
using System.Diagnostics;
using System.Globalization;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class AutoTrace : IDisposable
    {
		private readonly Log m_Log;
		private readonly string m_Message;
	    private readonly IDisposable m_StackFrame;

		private readonly Stopwatch m_StopWatch;

		public AutoTrace(Log log, string caller, string message)
		{
			if (log == null) { throw new ArgumentNullException(nameof(log));}
			if (caller == null) { throw new ArgumentNullException(nameof(caller));}

			m_Log = log;
			m_Message = message;
			m_StopWatch = Stopwatch.StartNew();

			var guid = Guid.NewGuid().ToString("N");
			var methodId = $"{caller}_{guid.Substring(0, 8)}";
			m_StackFrame = ImmutableContextStack.Push(methodId);

			m_Message = $"{ImmutableContextStack.CurrentStack}{(string.IsNullOrEmpty(m_Message) ? m_Message : ": " + m_Message)}";
			m_Log.Trace(m_Message + " Enter", methodId);
			LogMessageWriter.Indent++;
		}

	    public void Dispose()
		{
			m_StopWatch.Stop();
			LogMessageWriter.Indent--;
			m_Log.Trace(m_Message + $" [{ m_StopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}ms Leave]");
			m_StackFrame.Dispose();
		}

	}
}
