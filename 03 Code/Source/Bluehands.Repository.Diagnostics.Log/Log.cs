using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using NLog;

namespace Bluehands.Diagnostics.Log
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
        private class AutoTraceHandler : IDisposable
        {
            private readonly Log m_Log;
            private readonly string m_Message;
            private readonly Stopwatch m_StopWatch;
            private readonly StackTrace m_StackTrace;
            private readonly int m_StackFrameNumber;

            public AutoTraceHandler(Log log, string message, int stackFrameNumber)
            {
                m_Log = log;
                m_Message = message;
                m_StackTrace = new StackTrace();
                m_StackFrameNumber = stackFrameNumber;
                m_StopWatch = Stopwatch.StartNew();
                log.WriteLog(LogLevel.Trace, m_StackTrace, m_StackFrameNumber, null, "{0} [Enter]", message);
                s_CurrentIndent++;
            }

            public void Dispose()
            {
                try
                {
                    m_StopWatch.Stop();
                    s_CurrentIndent--;
                    m_Log.WriteLog(LogLevel.Trace, m_StackTrace, m_StackFrameNumber, null, "{0} [Leave {1} ms]", m_Message, m_StopWatch.Elapsed.TotalMilliseconds);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

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

        public bool IsValid { get { return m_IsValid; } }
        public bool IsFatal { get { return IsEnabled(LogLevel.Fatal); } }
        public bool IsError { get { return IsEnabled(LogLevel.Error); } }
        public bool IsWarning { get { return IsEnabled(LogLevel.Warning); } }
        public bool IsInfo { get { return IsEnabled(LogLevel.Info); } }
        public bool IsDebug { get { return IsEnabled(LogLevel.Debug); } }
        public bool IsTrace { get { return IsEnabled(LogLevel.Trace); } }
        public bool IsEnabled(LogLevel level)
        {
            var nLogLevel = GetNLogLevel(level);
            return m_NLog.IsEnabled(nLogLevel);
        }
        //[StringFormatMethod("message")]
        public void Fatal(string message)
        {
            WriteLog(LogLevel.Fatal, null, 2, null, message, null);
        }
        //[StringFormatMethod("message")]
        public void Fatal(string message, params object[] args)
        {
            WriteLog(LogLevel.Fatal, null, 2, null, message, args);
        }
        [Obsolete("Use overload with exception in first place")]
        public void Fatal(string message, Exception ex, params object[] args)
        {
            WriteLog(LogLevel.Fatal, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Fatal(Exception ex, string message, params object[] args)
        {
            WriteLog(LogLevel.Fatal, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Error(string message)
        {
            WriteLog(LogLevel.Error, null, 2, null, message, null);
        }
        //[StringFormatMethod("message")]
        public void Error(string message, params object[] args)
        {
            WriteLog(LogLevel.Error, null, 2, null, message, args);
        }
        [Obsolete("Use overload with exception in first place")]
        public void Error(string message, Exception ex, params object[] args)
        {
            WriteLog(LogLevel.Error, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Error(Exception ex, string message, params object[] args)
        {
            WriteLog(LogLevel.Error, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Warning(string message)
        {
            WriteLog(LogLevel.Warning, null, 2, null, message, null);
        }
        //[StringFormatMethod("message")]
        public void Warning(string message, params object[] args)
        {
            WriteLog(LogLevel.Warning, null, 2, null, message, args);
        }
        [Obsolete("Use overload with exception in first place")]
        public void Warning(string message, Exception ex, params object[] args)
        {
            WriteLog(LogLevel.Warning, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Warning(Exception ex, string message, params object[] args)
        {
            WriteLog(LogLevel.Warning, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Info(string message)
        {
            WriteLog(LogLevel.Info, null, 2, null, message, null);
        }
        //[StringFormatMethod("message")]
        public void Info(string message, params object[] args)
        {
            WriteLog(LogLevel.Info, null, 2, null, message, args);
        }
        [Obsolete("Use overload with exception in first place")]
        public void Info(string message, Exception ex, params object[] args)
        {
            WriteLog(LogLevel.Info, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Info(Exception ex, string message, params object[] args)
        {
            WriteLog(LogLevel.Info, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Debug(string methodName = null, string message = "")
        {
            WriteLog(LogLevel.Debug, null, 2, null, message, null);
        }
        //[StringFormatMethod("message")]
        public void Debug(string message, params object[] args)
        {
            WriteLog(LogLevel.Debug, null, 2, null, message, args);
        }
        
        //[StringFormatMethod("message")]
        public void Debug(Exception ex, string message, params object[] args)
        {
            WriteLog(LogLevel.Debug, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Trace(string message)
        {
            WriteLog(LogLevel.Trace, null, 2, null, message, null);
        }
        //[StringFormatMethod("message")]
        public void Trace(string message, params object[] args)
        {
            WriteLog(LogLevel.Trace, null, 2, null, message, args);
        }
        [Obsolete("Use overload with exception in first place")]
        public void Trace(string message, Exception ex, params object[] args)
        {
            WriteLog(LogLevel.Trace, null, 2, ex, message, args);
        }
        //[StringFormatMethod("message")]
        public void Trace(Exception ex, string message, params object[] args)
        {
            WriteLog(LogLevel.Trace, null, 2, ex, message, args);
        }
        internal void TraceAspectEnter(StackTrace stackTrace, string message)
        {
            WriteLog(LogLevel.Trace, stackTrace, 3, null, "{0} [Enter]", message);
            s_CurrentIndent++;
        }
        internal void TraceAspectExit(StackTrace stackTrace, string message, TimeSpan elapsed)
        {
            s_CurrentIndent--;
            WriteLog(LogLevel.Trace, stackTrace, 3, null, "{0} [Leave {1} ms]", message, elapsed.TotalMilliseconds);
        }
        //[StringFormatMethod("message")]
        internal void LogExceptionAspectError(Exception ex, string message, params object[] args)
        {
            WriteLog(LogLevel.Error, null, 3, ex, message, args);
        }

        public IDisposable AutoTrace()
        {
            return InternalAutoTrace(string.Empty);
        }

        public IDisposable AutoTrace(string message)
        {
            return InternalAutoTrace(message);
        }

        private IDisposable InternalAutoTrace(string message)
        {
            if (!m_IsValid)
            {
                return null;
            }
            try
            {
                if (!IsEnabled((LogLevel.Trace)))
                {
                    return null;
                }
                return new AutoTraceHandler(this, message, 3);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override string ToString()
        {
            return m_NLog != null ? m_NLog.Name : "invalid";
        }

        //[StringFormatMethod("message")]
        private void WriteLog(LogLevel logLevel, StackTrace stackTrace, int stackFrameNumber, Exception ex, string message, params object[] args)
        {
            try
            {
                if (!m_IsValid)
                {
                    return;
                }
                if (!IsEnabled(logLevel))
                {
                    return;
                }
                var logEventInfo = new LogEventInfo(GetNLogLevel(logLevel), m_FullName, CultureInfo.InvariantCulture, message, args);
                if (stackTrace == null)
                {
#if NETSTANDARD
                    stackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });
#else
                    stackTrace = new StackTrace();
#endif
                }
                var stackFrame = stackTrace.GetFrames()[stackFrameNumber];
                //logEventInfo.SetStackTrace(stackTrace, stackFrameNumber);
                Enrich(logEventInfo, stackFrame);
                logEventInfo.Exception = ex;
                m_NLog.Log(logEventInfo);

            }
            catch (Exception exx)
            {
                System.Diagnostics.Debug.WriteLine(exx);
            }
        }

        private static NLog.LogLevel GetNLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Warning:
                    return NLog.LogLevel.Warn;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                default:
                    return NLog.LogLevel.Trace;
            }
        }
        private void Enrich(LogEventInfo logEventInfo, StackFrame stackFrame)
        {
            IDictionary<object, object> properties = logEventInfo.Properties;
            //string methodName = stackFrame.GetMethod().Name;
            //properties.Add("Method", methodName);
            properties.Add("Class", m_TypeName);
            properties.Add("Namespace", m_Namespace);
            properties.Add("Indent", GetIndent());

            //properties.Add("SessionToken", m_ObfuscatedSessionToken);
            //properties.Add("ApplicationContextKey", m_ApplicationContextId);
            //properties.Add("ApplicationName", m_ApplicationName);
            //properties.Add("UserGuid", m_UserGuid);
            //properties.Add("UserId", m_UserId);
            //properties.Add("DatabaseGuid", m_DatabaseGuid);
            //properties.Add("DatabaseId", m_DatabaseId);
            //properties.Add("DatabaseUserContextKey", m_DatabaseUserContextId);
        }

        private static string GetIndent()
        {
            switch (s_CurrentIndent)
            {
                case 0:
                    return "";
                case 1:
                    return " ";
                case 2:
                    return "  ";
                case 3:
                    return "   ";
                case 4:
                    return "    ";
                case 5:
                    return "     ";
                case 6:
                    return "      ";
                default:
                    return new StringBuilder().Append(' ', s_CurrentIndent).ToString();
            }
        }

        private void GetTypeInfo(Type type)
        {
            m_FullName = type.FullName;
            m_Namespace = type.Namespace ?? string.Empty;
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

        private void AppendGenericParameters(object p, StringBuilder sb)
        {
            throw new NotImplementedException();
        }

        void AppendGenericParameters(Type[] genericArguments, StringBuilder sb)
        {
            for (var i = 0; i < genericArguments.Length; i++)
            {
                var type = genericArguments[i];
                if (type.GetTypeInfo().IsGenericType)               //IsGenericType ist nicht mehr teil des .NetCore, deshalb Umweg über GetTypeOf()
                {
                    var buider = new StringBuilder();
                    BuildGenericTypeName(type, buider);
                    sb.Append(buider);
                }
                else
                {
                    sb.Append(type.Name);
                }
                if (i < genericArguments.Length - 1)
                {
                    sb.Append(",");
                }
            }
        }
    }




   
}