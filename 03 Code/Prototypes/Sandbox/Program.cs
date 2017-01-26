using System;
using Bluehands.Repository.Diagnostics.Log;

namespace Sandbox
{
    class Program
    {
        private static readonly Log log = new Log(typeof(Program));

        private static void Main()
        {
            Test();
        }

        private static void Test()
        {
            var exeption = new NotImplementedException();

            log.Fatal("Log von Sandbox.Test");
            log.Fatal(exeption, "Log von Sandbox.Test");

            log.Error("Log von Sandbox.Test");
            log.Error(exeption, "Log von Sandbox.Test");

            log.Warning("Log von Sandbox.Test");
            log.Warning(exeption, "Log von Sandbox.Test");

            log.Info("Log von Sandbox.Test");
            log.Info(exeption, "Log von Sandbox.Test");

            log.Debug("Log von Sandbox.Test");
            log.Debug(exeption, "Log von Sandbox.Test");

            log.Trace("Log von Sandbox.Test");
            log.Trace(exeption, "Log von Sandbox.Test");
        }
    }
}
