using System;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class MethodNameExtracter
    {
        private const int frameCount = 1;

        private readonly Type m_MessageCreator;

        public MethodNameExtracter(Type messageCreator)
        {
            if (messageCreator == null)
            {
                throw new ArgumentNullException(nameof(messageCreator));
            }
            m_MessageCreator = messageCreator;
        }

        public CallerInfo ExtractCallerInfoFromStackTrace()
        {
            var frames = GetStackTraceFrames();
            var callerInfo = SearchFrameForCallerInfo(frames);
            return callerInfo;
        }

        private CallerInfo SearchFrameForCallerInfo(StackFrame[] frames)
        {
            for (var i = 0; i < frames.Length; i++)
            {
                var declaringType = GetDeclaringTypeOfCurrentFrame(frames, i);
                var isSearchedType = CheckIsSearchedType(declaringType);

                if (isSearchedType)
                {
                    return GetCallerInfos(frames, i);
                }
            }
            throw new NotImplementedException();
        }

        private bool CheckIsSearchedType(Type declaringType)
        {
            if (m_MessageCreator == declaringType)
            {
                return true;
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
            var methodNameOfMessageCreator = frames[i].GetMethod();

            var fullNameOfMessageCreator = methodNameOfMessageCreator.DeclaringType?.FullName;
            var classNameOfMessageCreator = methodNameOfMessageCreator.DeclaringType?.Name;
            
            var callerInfo = new CallerInfo(fullNameOfMessageCreator, classNameOfMessageCreator,
                methodNameOfMessageCreator.ToString());

            return callerInfo;
        }
    }
}
