using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    class Callee
    {
        public void MyMethod()
        {
            StackTraceExtension.GetFirstParrentCallingMethod();
        }
    }

    public static class StackTraceExtension
    {
        public static string GetFirstParrentCallingMethod()
        {
            var stackTrace = new StackTrace();

            var frames = stackTrace.GetFrames();
            var myMethodName = nameof(GetFirstParrentCallingMethod);
            var myType = typeof (StackTraceExtension);

            for (int i = 0; i < frames.Length; i++)
            {
                var declaringType = frames[i].GetMethod().DeclaringType;
                var methodName = frames[i].GetMethod().Name;

                if (methodName.Equals(myMethodName) && declaringType.Equals(myType))
                {
                    var firstStack = frames[i + 2].GetMethod().Name;
                    return firstStack;
                }
            }
            

            throw new NotImplementedException();
        }
    }
}
