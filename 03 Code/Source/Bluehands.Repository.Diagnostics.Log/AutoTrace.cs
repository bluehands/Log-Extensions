using System;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class AutoTrace : IDisposable
    {
        private readonly Log m_Log;
        private readonly string m_Message;

        private static readonly Stopwatch stopWatch = Stopwatch.StartNew();
        private readonly TimeSpan m_StopWatchStarted;
        public int Indent { get; private set; }

        public AutoTrace(Log logger, string message)
        {
            m_Log = logger;
            m_Message = message;
			Indent++;
			m_Log.Trace(m_Message + " Enter");
            m_StopWatchStarted = stopWatch.Elapsed;
            
        }

        public void Dispose()
        {
            Indent--;
            var formatedMiliseconds = GetFormatedMillisecondsString();

            m_Log.Trace(m_Message + $" Leave {formatedMiliseconds} ms");
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
