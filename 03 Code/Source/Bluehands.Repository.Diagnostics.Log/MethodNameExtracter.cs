using System;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class MethodNameExtracter
    {
        private const int frameCount = 1;

        private readonly Type m_GroundType;

        public MethodNameExtracter(Type groundType)
        {
            if (groundType == null)
            {
                throw new ArgumentNullException(nameof(groundType));
            }
            m_GroundType = groundType;
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
            if (m_GroundType == declaringType)
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
            var messageCreatorMethod = frames[i + frameCount].GetMethod();

            var fullNameOfMessageCreator = messageCreatorMethod.DeclaringType?.FullName;
            var classNameOfMessageCreator = messageCreatorMethod.DeclaringType?.Name;
            var methodNameOfMessageCreator = messageCreatorMethod.Name;

            var callerInfo = new CallerInfo(fullNameOfMessageCreator, classNameOfMessageCreator,
                methodNameOfMessageCreator);
            return callerInfo;
        }
    }
}
