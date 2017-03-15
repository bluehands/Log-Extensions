using System;
using System.Threading;
using Bluehands.Repository.Diagnostics.Log;

namespace Sandbox
{
    class Program
    {
        private static readonly Log<Program> log = new Log<Program>();

        private static void Main()
        {
            using (log.AutoTrace("AutoTrace active:"))
            {
				log.Debug("Creating threads...");
	            for (var i = 0; i < 2; i++)
	            {
		            var newThread = new Thread(Test);
		            newThread.Name = i.ToString();
					newThread.Start();
	            }
                //Test();
			}
            
        }

        private static void Test()
        {
            using (log.AutoTrace("Nachricht von AutoTrace"))
            {
				log.Debug($"Log entry 1, Thread {Thread.CurrentThread.ManagedThreadId}.");
				log.Debug($"Log entry 2, Thread {Thread.CurrentThread.ManagedThreadId}.");
				log.Debug($"Log entry 3, Thread {Thread.CurrentThread.ManagedThreadId}.");
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
