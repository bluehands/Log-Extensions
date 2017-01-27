using System;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class MethodNameExtracter
    {
        private const int frameCount = 1;

        private readonly Type m_CallerTypeOfLogMessageWriter;

        public MethodNameExtracter(Type callerTypeOfLogMessageWriter)
        {
            m_CallerTypeOfLogMessageWriter = callerTypeOfLogMessageWriter;
        }

        public CallerInfo ExtractCallerInfoFromStackTrace()
        {
            var callerInfo = new CallerInfo();
            var frames = GetStackTraceFrames();

            if (frames == null) return null;
            callerInfo = SearchFrameForCallerInfo(callerInfo, frames);
            return callerInfo;
        }

        private CallerInfo SearchFrameForCallerInfo(CallerInfo callerInfo, StackFrame[] frames)
        {
            for (var i = frames.Length - 1; i > 0; i--)
            {
                var declaringType = GetDeclaringType(frames, i);
                var isSearchedType = CheckType(frames, i, declaringType);

                if (isSearchedType)
                {
                    callerInfo = GetCallerInfos(frames, i);
                }
            }

            return callerInfo;
        }

        private static StackFrame[] GetStackTraceFrames()
        {
            var stackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });
            var frames = stackTrace.GetFrames();
            return frames;
        }

        private bool CheckType(StackFrame[] frames, int i, Type declaringType)
        {
            if (m_CallerTypeOfLogMessageWriter == declaringType)
            {
                if (i + frameCount < frames.Length)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private static Type GetDeclaringType(StackFrame[] frames, int i)
        {
            var frame = frames[i];
            var method = frame.GetMethod();
            var declaringType = method.DeclaringType;
            return declaringType;
        }

        private static CallerInfo GetCallerInfos(StackFrame[] frames, int i)
        {
            var loggedMethod = frames[i + frameCount].GetMethod();

            var fullNameOfCallerOfLog = loggedMethod.DeclaringType?.FullName;
            var classNameOfCallerOfLog = loggedMethod.DeclaringType?.Name;
            var methodNameOfCallerOfLog = loggedMethod.Name;

            var callerInfo = new CallerInfo(fullNameOfCallerOfLog, classNameOfCallerOfLog,
                methodNameOfCallerOfLog);
            return callerInfo;
        }
    }
}
