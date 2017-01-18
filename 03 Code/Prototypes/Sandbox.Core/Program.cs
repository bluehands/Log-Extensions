using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sandbox.Core
{
    public class Program
    {
        public StackFrame StackFrame { get; private set; }

        public static void Main(string[] args)
        {
                
                var caller = new Caller();
            caller.DoIt();
            var stackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });

            var frames = stackTrace.GetFrames();
            var framesEnumerable = frames.AsEnumerable();
            var framesList = new List<StackFrame>();
            foreach (var frame in framesEnumerable)
                framesList.Add(frame);

            var allStackFrames = new List<StackFrameWithIndex>();

            //stackTrace.Select((f, i) => new StackFrameWithIndex(i, f)).ToList();

            for (int j = 0; j < framesList.Count; j++)
            {
                allStackFrames.Add(new StackFrameWithIndex(j, framesList[j]));
            }

            //var filteredStackframes = allStackFrames.Where(p => !SkipAssembly(p.StackFrame)).ToList();




            //[NotNull]
            //Type loggerType;
            //int firstUserFrame = FindCallingMethodOnStackTrace(st, loggerType);

        }

        //private static bool SkipAssembly(StackFrame stackFrame)
        //{
        //    var method = stackFrame.GetMethod();
        //    var assembly = method.DeclaringType != null ? method.DeclaringType.GetAssembly() : method.Module.GetAssembly();
        //    //skip stack frame if the method declaring type assembly is from hidden assemblies list
        //    var skipAssembly = SkipAssembly(assembly);
        //    return skipAssembly;
        //}

        //        public static Assembly GetAssembly(this Type type)
        //        {
        //#if !NETSTANDARD
        //            return type.Assembly;
        //#else
        //            var typeInfo = type.GetTypeInfo();
        //            return typeInfo.Assembly;
        //#endif
        //        }
    }

    class StackFrameWithIndex
    {
        public StackFrameWithIndex(int stackFrameIndex, StackFrame stackFrame)
        {
            int StackFrameIndex = stackFrameIndex;
            StackFrame StackFrame = stackFrame;
        }

        public int StackFrameIndex { get; private set; }
        public StackFrame StackFrame { get; set; }
    }
}
