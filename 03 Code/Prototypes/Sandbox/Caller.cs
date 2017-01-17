using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    class Caller
    {
        public void DoIt()
        {
            StackTraceExtension.GetFirstParrentCallingMethod();
            var callee = new Callee();
            callee.MyMethod();
        }

    }
}
