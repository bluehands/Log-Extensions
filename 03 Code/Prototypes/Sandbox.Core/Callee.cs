using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.Core
{
    public class Callee
    {
        public void MyMethod()
        {
            StackTraceExtension.GetParrentCallingMethod();
        }
    }

    public static class StackTraceExtension
    {
        public static string GetParrentCallingMethod()
        {
            var stackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });
            var frames = stackTrace.GetFrames();
            var ourType = typeof(StackTraceExtension);
            var ourMethodName = nameof(GetParrentCallingMethod);

            for (int i = 0; i < frames.Length; i++)
            {
                var frame = frames[i];
                var method = frame.GetMethod();
                var declaringType = method.DeclaringType;
                var methodName = method.Name;                
                if (methodName.Equals(ourMethodName) && ourType == declaringType)
                {
                    var parentIndex = i + 2;
                    if (parentIndex < frames.Length)
                    {
                        return frames[i + 2].GetMethod().Name;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
                Console.WriteLine(method);
            }


            throw new NotImplementedException();
        }
    }
}
