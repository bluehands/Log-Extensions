using System;
using Bluehands.Repository.Diagnostics.Log;

namespace Sandbox
{
    class Program
    {

        private static readonly Log log = new Log(typeof(Program));

        static void Main()
        {
            Test();
        }

        private static void Test()
        {
            //log.Fatal("Log von Sandbox.Core.Test");

            var exeption = new NotImplementedException();

            log.Fatal(exeption, "Log von Sandbox.Core.Test");
        }
    }
}
