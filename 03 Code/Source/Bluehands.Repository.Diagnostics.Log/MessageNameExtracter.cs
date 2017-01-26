using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class MethodNameExtracter
    {
        private const int takeTheUnknowmCaller = 1;

        private readonly Type m_SearchedCallerOfLogMessageWriter;

        public MethodNameExtracter(Type searchedCallerOfLogMessageWriter)
        {
            m_SearchedCallerOfLogMessageWriter = searchedCallerOfLogMessageWriter;
        }

        public Dictionary<string, string> ExtractCallerInfosFromStackTrace()
        {
            var stackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });

            var frames = stackTrace.GetFrames();

            if (frames == null) return null;
            for (var i = frames.Length - 1; i > 0; i--)
            {
                var frame = frames[i];
                var method = frame.GetMethod();
                var declaringType = method.DeclaringType;
                if (m_SearchedCallerOfLogMessageWriter == declaringType)
                {
                    if (i + takeTheUnknowmCaller < frames.Length)
                    {
                        var namespaceOfCallerOfLog = frames[i + takeTheUnknowmCaller].GetMethod().DeclaringType?.FullName;
                        var methodNameOfCallerOfLog = frames[i + takeTheUnknowmCaller].GetMethod().Name;

                        var namespaceLog = frame.GetMethod().DeclaringType?.FullName;
                        var methodNameLog = frame.GetMethod().Name;

                        var caller = new Dictionary<string, string>();
                        caller.Add(methodNameOfCallerOfLog, namespaceOfCallerOfLog);
                        caller.Add(methodNameLog, namespaceLog);


                        return caller;
                    }
                }
            }
            return null;
        }
    }
}
