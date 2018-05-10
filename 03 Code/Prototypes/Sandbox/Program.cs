using System;
using System.Threading;
using System.Threading.Tasks;
using Bluehands.Diagnostics.LogExtensions;

namespace Sandbox
{
    class Program
    {
        private static readonly Log s_Log = new Log<Program>();

        public static async Task Main()
        {
            s_Log.Correlation = Guid.NewGuid().ToString();
            using (s_Log.AutoTrace(""))
            {
                s_Log.Debug("Creating threads...");
                for (var i = 0; i < 2; i++)
                {
                    var newThread = new Thread(Test) { Name = i.ToString() };
                    newThread.Start();
                }

                var t1 = Task.Run(
                    () =>
                    {
                        using (s_Log.AutoTrace())
                        {
                            s_Log.Debug("Running from Task t1");
                        }
                    }

                    );
                var t2 = Task.Run(
                    () =>
                    {
                        using (s_Log.AutoTrace())
                        {
                            s_Log.Debug("Running from Task t2");
                        }
                    }

                );

                await Task.WhenAll(t1, t2);
            }

            Console.ReadLine();
        }

        private static void Test()
        {
            s_Log.Correlation = Guid.NewGuid().ToString();
            using (s_Log.AutoTrace("Nachricht von AutoTrace"))
            {
                s_Log.Debug($"Log entry 1, Thread {Thread.CurrentThread.ManagedThreadId}.");
                s_Log.Debug($"Log entry 2, Thread {Thread.CurrentThread.ManagedThreadId}.");
                s_Log.Debug($"Log entry 3, Thread {Thread.CurrentThread.ManagedThreadId}.");
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
