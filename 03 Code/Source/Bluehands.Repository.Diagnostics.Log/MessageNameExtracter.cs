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
            var frames = GetStackTraceFrames();
            var callerInfo = SearchFrameForCallerInfo(frames);
            return callerInfo;
        }

        private CallerInfo SearchFrameForCallerInfo(StackFrame[] frames)
        {
            for (var i = frames.Length - 1; i > 0; i--)
            {
                var declaringType = GetDeclaringTypeOfCurrentFrame(frames, i);
                var isSearchedType = CheckIsSearchedType(frames, i, declaringType);

                if (isSearchedType)
                {
                    return GetCallerInfos(frames, i);
                }
            }

            //ToDo: Throw exception
            return new CallerInfo();
        }

        private bool CheckIsSearchedType(StackFrame[] frames, int i, Type declaringType)
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

        private static StackFrame[] GetStackTraceFrames()
        {
            var stackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });
            var frames = stackTrace.GetFrames();
            return frames;
        }

        private static Type GetDeclaringTypeOfCurrentFrame(StackFrame[] frames, int i)
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
