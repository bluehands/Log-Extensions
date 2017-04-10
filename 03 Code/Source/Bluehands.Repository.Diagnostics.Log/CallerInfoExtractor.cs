using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;


namespace Bluehands.Repository.Diagnostics.Log
{
    internal class CallerInfoExtractor
    {
        private readonly Type m_MessageCreator;

        public CallerInfoExtractor(Type messageCreator)
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

	    public CallerInfo ExtractCallerInfoFromImmutableContextStack(string caller)
	    {
		    var currentContext = ImmutableContextStack.CurrentContext;
		    var currentStack = currentContext.Single(c => c.Contains(caller));
		    return new CallerInfo(currentStack);
	    }

        private static IEnumerable<StackFrame> GetStackTraceFrames()
        {
            var stackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });
            var frames = stackTrace.GetFrames();
            return frames;
        }
    }
}
