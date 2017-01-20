using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Bluehands.Repository.Diagnostics.Log;

namespace Sandbox.Core
{
    public class Program
    {
        public StackFrame StackFrame { get; private set; }

        private static readonly Log log = new Log(typeof(Program));

        public static void Main(string[] args)
        {

            //log.Fatal("Geht es?");
            Test();
            //var caller = new Caller();
            //caller.DoIt();
        }

        private static void Test()
        {
            log.Fatal("Log von Sandbox.Core.Main -> Test");
            //log.Fatal("Log von Sandbox.Core.Main.Test", ex: new NotImplementedException());
        }
    }
}
