using System;
using Bluehands.Repository.Diagnostics.Log;

namespace Sandbox
{
    class Program
    {
        private static readonly Log<Program> log = new Log<Program>();

        private static void Main()
        {

            using (log.AutoTrace("Nachricht von AutoTrace"))
            {
                //Hier kommen jetzt Methoden
                log.Debug("Log von Main über AutoTrace");
            }
            Test();
            using (log.AutoTrace("Nachricht von AutoTrace"))
            {
                //Hier kommen jetzt Methoden
                log.Debug("Log von Main über AutoTrace");
            }
        }

        private static void Test()
        {
            using (log.AutoTrace("Nachricht von AutoTrace"))
            {
                //Hier kommen jetzt Methoden
                log.Debug("yyyyyyyyyyyy");
            }

            //var exeption = new NotImplementedException();

            //log.Fatal("Log von Sandbox.Test");
            //log.Fatal(exeption, "Log von Sandbox.Test");

            //log.Error("Log von Sandbox.Test");
            //log.Error(exeption, "Log von Sandbox.Test");

            //log.Warning("Log von Sandbox.Test");
            //log.Warning(exeption, "Log von Sandbox.Test");

            //log.Info("Log von Sandbox.Test");
            //log.Info(exeption, "Log von Sandbox.Test");

            //log.Debug("Log von Sandbox.Test");
            //log.Debug(exeption, "Log von Sandbox.Test");

            //log.Trace("Log von Sandbox.Test");
            //log.Trace(exeption, "Log von Sandbox.Test");
        }
    }
}
