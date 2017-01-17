using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.Core
{
    public class Caller
    {
        public void DoIt()
        {
            StackTraceExtension.GetParrentCallingMethod();
            var callee = new Callee();
            callee.MyMethod();
        }
    }
}
