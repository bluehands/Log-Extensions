using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using System.Threading;

namespace Bluehands.Repository.Diagnostics.Log
{
    internal class CallerInfo
    {

		public string TypeOfMessageCreator { get; private set; }
        public string ClassOfMessageCreator { get; private set; }
        public string MethodNameOfMessageCreator { get; private set; }
		public string ThreadIdOfMessageCreator { get; private set; }
        
        public CallerInfo(string typeOfMessageCreator, string classOfMessageCreator, string methodNameOfMessageCreator, string threadIdOfMessageCreator)
        {
            TypeOfMessageCreator = typeOfMessageCreator;
            ClassOfMessageCreator = classOfMessageCreator;
            MethodNameOfMessageCreator = methodNameOfMessageCreator;
	        ThreadIdOfMessageCreator = threadIdOfMessageCreator;
        }

	    public CallerInfo(IEnumerable<StackFrame> frames, Type messageCreator)
	    {
		    if (frames == null || !frames.Any()) { throw new ArgumentNullException(nameof(frames)); }
		    if (messageCreator == null) { throw new ArgumentNullException(nameof(messageCreator)); }

			foreach (var frame in frames)
			{
				if (messageCreator == GetDeclaringTypeOf(frame))
				{
						InitializeFrom(frame);
						return;
				}
			}

			throw new InstanceNotFoundException("Message creator with type: " + messageCreator + "not found in StackFrames.");
	    }

		public CallerInfo(string currentStack)
		{
			MethodNameOfMessageCreator = currentStack;
		}

		private static Type GetDeclaringTypeOf(StackFrame frame)
		{
			var method = frame.GetMethod();
			var declaringType = method.DeclaringType;
			return declaringType;
		}

		private void InitializeFrom(StackFrame frame)
		{
			var method = frame.GetMethod();

			TypeOfMessageCreator = method.DeclaringType?.FullName;
			ClassOfMessageCreator = method.DeclaringType?.Name;
			MethodNameOfMessageCreator = method.Name;
			ThreadIdOfMessageCreator = Thread.CurrentThread.ManagedThreadId.ToString();
		}
	}
}
