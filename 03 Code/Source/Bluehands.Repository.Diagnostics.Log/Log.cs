using System;
using System.Reflection;
using System.Text;
using NLog;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class Log<T> : Log
    {
        public Log()
            : base(typeof(T))
        {

        }
    }

    public class Log
    {
        //private class AutoTraceHandler : IDisposable
        //{
        //    private readonly Log m_Log;
        //    private readonly string m_Message;
        //    private readonly Stopwatch m_StopWatch;
        //    private readonly StackTrace m_StackTrace;
        //    private readonly int m_StackFrameNumber;

        //    public LogMessageWriter logMessageWriter = new LogMessageWriter();

        //    public AutoTraceHandler(Log log, string message, int stackFrameNumber)
        //    {
        //        m_Log = log;
        //        m_Message = message;
        //        //m_StackTrace = new StackTrace();
        //        m_StackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });
        //        m_StackFrameNumber = stackFrameNumber;
        //        m_StopWatch = Stopwatch.StartNew();
        //        logMessageWriter.WriteLog(LogLevel.Trace, m_StackTrace, m_StackFrameNumber, null, "{0} [Enter]", message);
        //        s_CurrentIndent++;
        //    }

        //}

        [ThreadStatic]
        private static int s_CurrentIndent;
        private readonly Logger m_NLog;

        private string m_TypeName;
        private string m_Namespace;
        private string m_FullName;
        private readonly bool m_IsValid;


        public Log(Type caller)
        {
            try
            {
                if (caller != null)
                {
                    GetTypeInfo(caller);
                    m_NLog = LogManager.GetLogger(m_FullName);
                    m_IsValid = true;
                }
                else
                {
                    m_IsValid = false;
                }
            }
            catch (Exception)
            {
                m_IsValid = false;
            }
        }

        //public bool IsValid { get { return m_IsValid; } }
        //public bool IsFatal { get { return IsEnabled(LogLevel.Fatal); } }
        //public bool IsError { get { return IsEnabled(LogLevel.Error); } }
        //public bool IsWarning { get { return IsEnabled(LogLevel.Warning); } }
        //public bool IsInfo { get { return IsEnabled(LogLevel.Info); } }
        //public bool IsDebug { get { return IsEnabled(LogLevel.Debug); } }
        //public bool IsTrace { get { return IsEnabled(LogLevel.Trace); } }
        //public bool IsEnabled(LogLevel level)
        //{
        //    var nLogLevel = LogMessageWriter.GetNLogLevel(level);
        //    return m_NLog.IsEnabled(nLogLevel);
        //}

            

        private readonly LogMessageWriter _logMessageWriter = new LogMessageWriter();

        //[StringFormatMethod("message")]
        public void Fatal(string message)
        {
            _logMessageWriter.WriteLogEntry(LogLevel.Fatal, message, OriginCaller.grandGrandGrandParent, m_FullName, m_TypeName);
        }

        public void Fatal(Exception ex, string message)
        {
            _logMessageWriter.WriteLogEntry(LogLevel.Fatal, message, OriginCaller.grandGrandGrandParent, m_FullName, m_TypeName, ex);
        }

        //public void Fatal(Func<string> message)
        //{

        //}

        ////[StringFormatMethod("message")]
        //public void Fatal(string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Fatal, null, 2, null, message, args);
        //}
        //[Obsolete("Use overload with exception in first place")]
        //public void Fatal(string message, Exception ex, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Fatal, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Fatal(Exception ex, string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Fatal, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Error(string message)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Error, null, 2, null, message, null);
        //}
        ////[StringFormatMethod("message")]
        //public void Error(string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Error, null, 2, null, message, args);
        //}
        //[Obsolete("Use overload with exception in first place")]
        //public void Error(string message, Exception ex, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Error, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Error(Exception ex, string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Error, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Warning(string message)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Warning, null, 2, null, message, null);
        //}
        ////[StringFormatMethod("message")]
        //public void Warning(string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Warning, null, 2, null, message, args);
        //}
        //[Obsolete("Use overload with exception in first place")]
        //public void Warning(string message, Exception ex, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Warning, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Warning(Exception ex, string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Warning, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Info(string message)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Info, null, 2, null, message, null);
        //}
        ////[StringFormatMethod("message")]
        //public void Info(string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Info, null, 2, null, message, args);
        //}
        //[Obsolete("Use overload with exception in first place")]
        //public void Info(string message, Exception ex, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Info, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Info(Exception ex, string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Info, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Debug(string methodName = null, string message = "")
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Debug, null, 2, null, message, null);
        //}
        ////[StringFormatMethod("message")]
        //public void Debug(string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Debug, null, 2, null, message, args);
        //}

        ////[StringFormatMethod("message")]
        //public void Debug(Exception ex, string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Debug, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Trace(string message)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Trace, null, 2, null, message, null);
        //}
        ////[StringFormatMethod("message")]
        //public void Trace(string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Trace, null, 2, null, message, args);
        //}
        //[Obsolete("Use overload with exception in first place")]
        //public void Trace(string message, Exception ex, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Trace, null, 2, ex, message, args);
        //}
        ////[StringFormatMethod("message")]
        //public void Trace(Exception ex, string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Trace, null, 2, ex, message, args);
        //}
        //internal void TraceAspectEnter(StackTrace stackTrace, string message)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Trace, stackTrace, 3, null, "{0} [Enter]", message);
        //    s_CurrentIndent++;
        //}
        //internal void TraceAspectExit(StackTrace stackTrace, string message, TimeSpan elapsed)
        //{
        //    s_CurrentIndent--;
        //    LogMessageWriter.WriteLog(LogLevel.Trace, stackTrace, 3, null, "{0} [Leave {1} ms]", message, elapsed.TotalMilliseconds);
        //}
        ////[StringFormatMethod("message")]
        //internal void LogExceptionAspectError(Exception ex, string message, params object[] args)
        //{
        //    LogMessageWriter.WriteLog(LogLevel.Error, null, 3, ex, message, args);
        //}

        //public IDisposable AutoTrace()
        //{
        //    return InternalAutoTrace(string.Empty);
        //}

        //public IDisposable AutoTrace(string message)
        //{
        //    return InternalAutoTrace(message);
        //}

        //private IDisposable InternalAutoTrace(string message)
        //{
        //    if (!m_IsValid)
        //    {
        //        return null;
        //    }
        //    try
        //    {
        //        if (!IsEnabled((LogLevel.Trace)))
        //        {
        //            return null;
        //        }
        //        return new AutoTraceHandler(this, message, 3);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public override string ToString()
        //{
        //    return m_NLog != null ? m_NLog.Name : "invalid";
        //}

        //private static string GetIndent()
        //{
        //    switch (s_CurrentIndent)
        //    {
        //        case 0:
        //            return "";
        //        case 1:
        //            return " ";
        //        case 2:
        //            return "  ";
        //        case 3:
        //            return "   ";
        //        case 4:
        //            return "    ";
        //        case 5:
        //            return "     ";
        //        case 6:
        //            return "      ";
        //        default:
        //            return new StringBuilder().Append(' ', s_CurrentIndent).ToString();
        //    }
        //}

        private void GetTypeInfo(Type type)
        {
            m_FullName = type.FullName;
            m_Namespace = type.Namespace ?? string.Empty;
#if NETSTANDARD
            if (type.GetTypeInfo().IsGenericType)
            {
                var sb = new StringBuilder();
                BuildGenericTypeName(type, sb);
                m_TypeName = sb.ToString();
            }
            else
            {
                m_TypeName = type.Name;
            }
#endif
        }

        void BuildGenericTypeName(Type type, StringBuilder sb)
        {
            var name = type.Name;
            var pos = name.IndexOf('`');
            if (pos > 0)
            {
                name = name.Substring(0, pos);
            }

            sb.Append(name);
            sb.Append("<");
            //AppendGenericParameters(type.GetGenericArguments(), sb);      //nicht mehr in .NETCore vorhanden, noch kein Äquivalent gefunden
            sb.Append(">");
        }

        //void AppendGenericParameters(Type[] genericArguments, StringBuilder sb)
        //{
        //    for (var i = 0; i < genericArguments.Length; i++)
        //    {
        //        var type = genericArguments[i];
        //        //if (type.GetTypeInfo().IsGenericType)               //IsGenericType ist nicht mehr teil des .NetCore, deshalb Umweg über GetTypeOf()
        //        //{
        //        //    var buider = new StringBuilder();
        //        //    BuildGenericTypeName(type, buider);
        //        //    sb.Append(buider);
        //        //}
        //        //else
        //        //{
        //        //    sb.Append(type.Name);
        //        //}
        //        if (i < genericArguments.Length - 1)
        //        {
        //            sb.Append(",");
        //        }
        //    }
        //}
    }
}