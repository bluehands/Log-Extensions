﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class MethodNameExtracter
    {
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
            var callerInfo = new CallerInfo(frames, m_MessageCreator);
            return callerInfo;
        }

        private static IEnumerable<StackFrame> GetStackTraceFrames()
        {
            var stackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });
            var frames = stackTrace.GetFrames();
            return frames;
        }
    }
}
