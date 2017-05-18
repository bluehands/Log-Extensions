﻿using System.Threading;
using Bluehands.Repository.Diagnostics.Log;
using Bluehands.Repository.Diagnostics.Log.Aspects.Attributes;

namespace Sandbox
{
    class Program
    {
        private static readonly Log Log = new Log<Program>();

		[AutoTrace("AutoTrace active:")]
        private static void Main()
        {
            //using (Log.AutoTrace("AutoTrace active:"))
            //{
				Log.Debug("Creating threads...");
	            for (var i = 0; i < 2; i++)
	            {
		            var newThread = new Thread(Test) {Name = i.ToString()};
		            newThread.Start();
	            }
			//}
            
        }

        private static void Test()
        {
            using (Log.AutoTrace("Nachricht von AutoTrace"))
            {
				Log.Debug($"Log entry 1, Thread {Thread.CurrentThread.ManagedThreadId}.");
				Log.Debug($"Log entry 2, Thread {Thread.CurrentThread.ManagedThreadId}.");
				Log.Debug($"Log entry 3, Thread {Thread.CurrentThread.ManagedThreadId}.");
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
