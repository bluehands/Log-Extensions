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
            for (var i = frames.Length - 1; i > 0; i--)
            {
                var frame = frames[i];
                var isSearchedFrame = CheckThisFrame(i, frames, frame);

                if (isSearchedFrame)
                {
                    callerInfo = GetInfosOfNeededMethod(frames, i);
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

        private bool CheckThisFrame(int i, StackFrame[] frames, StackFrame frame)
        {
            var method = frame.GetMethod();
            var declaringType = method.DeclaringType;
            if (m_CallerTypeOfLogMessageWriter == declaringType)
            {
                if (i + frameCount < frames.Length)
                {
                    return true;
                }
            }
            return false;
        }

        private static CallerInfo GetInfosOfNeededMethod(StackFrame[] frames, int i)
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
