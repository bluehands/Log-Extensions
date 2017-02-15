using System;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class AutoTrace : IDisposable
    {
        private readonly LogMessageWriter m_LogMessageWriter;
        private readonly string m_Message;

        private static readonly Stopwatch stopWatch = Stopwatch.StartNew();
        private readonly TimeSpan m_StopWatchStarted;
        public int Indent { get; private set; }

        public AutoTrace(LogMessageWriter logMessageWriter, string message)
        {
            m_LogMessageWriter = logMessageWriter;
            m_Message = message;
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message + " Enter", Indent);
            m_StopWatchStarted = stopWatch.Elapsed;
            Indent++;
        }

        public void Dispose()
        {
            Indent--;
            var formatedMiliseconds = GetFormatedMillisecondsString();

            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, m_Message + $" Leave {formatedMiliseconds} ms", Indent);
        }

        private string GetFormatedMillisecondsString()
        {
            var end = stopWatch.Elapsed - m_StopWatchStarted;
            var miliseconds = end.TotalMilliseconds;

            var formatedMiliseconds = $"{miliseconds:0.000}";
            return formatedMiliseconds;
        }
    }
}
