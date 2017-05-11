using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;


namespace Bluehands.Repository.Diagnostics.Log
{
    internal class AutoTrace : IDisposable
    {
		private readonly ILogMessageWriter m_LogMessageWriter;
		private readonly Func<string> m_Message;
		private static readonly Stopwatch s_StopWatch = Stopwatch.StartNew();
	    private readonly TimeSpan m_StartTime;
		private readonly string m_Caller;

	    public AutoTrace(ILogMessageWriter logWriter, Func<string> messageFactory, [CallerMemberName] string caller = "")
	    {
			if (logWriter == null) { throw new ArgumentNullException(nameof(logWriter)); }
		    if (messageFactory == null) throw new ArgumentNullException(nameof(messageFactory));
		    if (string.IsNullOrEmpty(caller)) { throw new ArgumentNullException(nameof(caller)); }

			m_LogMessageWriter = logWriter;
			m_Message = messageFactory;
		    m_StartTime = s_StopWatch.Elapsed;
			m_Caller = caller;

			m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, () => m_Message() + " Enter", m_Caller);
			LogMessageWriterBase.Indent++;
		}

	    public void Dispose()
		{
			var end = s_StopWatch.Elapsed - m_StartTime;
			LogMessageWriterBase.Indent--;
			m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, () => m_Message() + $" [{ end.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}ms] Leave", m_Caller);
		}
	}
}
