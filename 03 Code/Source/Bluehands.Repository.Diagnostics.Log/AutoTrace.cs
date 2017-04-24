using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class AutoTrace : IDisposable
    {
		private readonly Log m_Log;
		private readonly Func<string> m_Message;
	    private readonly Stopwatch m_StopWatch;
	    private readonly string m_Caller;

	    public AutoTrace(Log log, Func<string> messageFactory, [CallerMemberName] string caller = "")
	    {
			if (log == null) { throw new ArgumentNullException(nameof(log)); }
		    if (messageFactory == null) throw new ArgumentNullException(nameof(messageFactory));
		    if (string.IsNullOrEmpty(caller)) { throw new ArgumentNullException(nameof(caller)); }

			m_Log = log;
			m_Message = messageFactory;
			m_StopWatch = Stopwatch.StartNew();
			m_Caller = caller;

			m_Log.Trace(m_Message + "Enter", m_Caller);
			LogMessageWriterBase.Indent++;
		}

	    public void Dispose()
		{
			m_StopWatch.Stop();
			LogMessageWriterBase.Indent--;
			m_Log.Trace(m_Message + $" [{ m_StopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}ms Leave]", m_Caller);
		}
	}
}
