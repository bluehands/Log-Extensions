using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Bluehands.Repository.Diagnostics.Log
{
    class AutoTrace : IDisposable
    {
        readonly string m_Caller;
        readonly ILogMessageWriter m_LogMessageWriter;
        readonly string m_Message;
        static readonly Stopwatch s_StopWatch = Stopwatch.StartNew();
        readonly TimeSpan m_StartTime;
        readonly IDisposable m_TraceStackHandle;

        public AutoTrace(ILogMessageWriter logWriter, Func<string> messageFactory, [CallerMemberName] string caller = "", params KeyValuePair<string, string>[] customProperties)
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
                    m_Message = GetMessage(messageFactory);
                    m_StartTime = s_StopWatch.Elapsed;

                    m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, () => LogFormatters.TraceEnter(m_Message), m_Caller, customProperties: customProperties);

                    m_TraceStackHandle = TraceStack.Push(LogFormatters.ContextPart(m_Caller));
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        string GetMessage(Func<string> messageFactory)
        {
            try
            {
                return messageFactory.Invoke();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return "---Could not get Message--";
            }
        }

        public void Dispose()
        {
            if (m_LogMessageWriter == null)
            {
                return;
            }
            try
            {
                if (m_LogMessageWriter.IsTraceEnabled)
                {
                    var end = s_StopWatch.Elapsed - m_StartTime;
                    m_TraceStackHandle.Dispose();
                    m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, () => LogFormatters.TraceLeave(m_Message, end.TotalMilliseconds), m_Caller);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}
