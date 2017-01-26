using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class MethodNameExtracter
    {
        private const int frameCount = 1;

        private readonly Type m_SearchedCallerOfLogMessageWriter;

        public MethodNameExtracter(Type searchedCallerOfLogMessageWriter)
        {
            m_SearchedCallerOfLogMessageWriter = searchedCallerOfLogMessageWriter;
        }

        public CallerInfos ExtractCallerInfoFromStackTrace()
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
                    if (i + frameCount < frames.Length)
                    {
                        var methodInfo = frames[i + frameCount].GetMethod();
                        var namespaceOfCallerOfLog = methodInfo.DeclaringType?.FullName;
                        var classNameOfLog = methodInfo.DeclaringType?.Name;
                        var methodNameOfCallerOfLog = methodInfo.Name;

                        var callerInfos = new CallerInfos(namespaceOfCallerOfLog, classNameOfLog,
                            methodNameOfCallerOfLog);

                        return callerInfos;
                    }
                }
            }
            return null;
        }
    }
}
