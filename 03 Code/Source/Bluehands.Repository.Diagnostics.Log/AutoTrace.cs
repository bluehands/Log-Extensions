using System;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class AutoTrace : IDisposable
    {
        private readonly LogMessageWriter m_LogMessageWriter;
        private readonly string m_Message;

        private static Stopwatch s_StopWatch;
        private TimeSpan m_StopWatchStarted;
        public int Indent { get; set; }

        public AutoTrace(LogMessageWriter logMessageWriter, string message)
        {
            m_LogMessageWriter = logMessageWriter;
            m_Message = message;
            //Indent = indent;
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, message + " Enter", Indent);
            s_StopWatch = Stopwatch.StartNew();
            m_StopWatchStarted = s_StopWatch.Elapsed;
            Indent++;
            Console.WriteLine(Indent + " Enter für diese Message: " + message);
        }

        public void Dispose()
        {
            Indent--;
            Console.WriteLine(Indent + " Leave");
            var end = s_StopWatch.Elapsed - m_StopWatchStarted;
            m_LogMessageWriter.WriteLogEntry(LogLevel.Trace, m_Message + $" Leave {end} ms", Indent);
        }
    }
}
