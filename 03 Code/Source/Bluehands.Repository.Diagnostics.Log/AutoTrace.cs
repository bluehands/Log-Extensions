using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;


namespace Bluehands.Repository.Diagnostics.Log
{
    internal class AutoTrace : IDisposable
    {
        private readonly string m_Caller;
        private readonly ILogMessageWriter m_LogMessageWriter;
        private readonly Func<string> m_Message;
        private static readonly Stopwatch s_StopWatch = Stopwatch.StartNew();
        private readonly TimeSpan m_StartTime;

        public AutoTrace(ILogMessageWriter logWriter, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            try
            {
                if (logWriter == null) { return; }
                if (messageFactory == null) { return; }
                if (string.IsNullOrEmpty(caller)) { return; }
                m_Caller = caller;
                if (logWriter.IsTraceEnabled)
                {
                    m_LogMessageWriter = logWriter;
                    m_Message = messageFactory;
                    m_StartTime = s_StopWatch.Elapsed;

                    m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, () => m_Message() + " Enter", m_Caller);
                    LogMessageWriterBase.Indent++;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Dispose()
        {
            try
            {
                if (m_LogMessageWriter.IsTraceEnabled)
                {
                    var end = s_StopWatch.Elapsed - m_StartTime;
                    LogMessageWriterBase.Indent--;
                    m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, () => m_Message() + $" [{end.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}ms] Leave", m_Caller);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
