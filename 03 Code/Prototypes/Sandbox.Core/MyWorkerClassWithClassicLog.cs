using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Bluehands.Diagnostics.LogExtensions;

namespace Sandbox.Core
{
    public class MyWorkerClassWithClassicLog
    {
        private readonly Log<MyWorkerClassWithClassicLog> m_Log = new Log<MyWorkerClassWithClassicLog>();
        public void MySyncWorkerMethod(string text)
        {
            using (m_Log.AutoTrace())
            {
                m_Log.Debug("Some debug messages");
                File.WriteAllText("MyFile.txt", text);
            }
        }

        public Task MyAsyncWorkerMethod(string text)
        {
            using (m_Log.AutoTrace())
            {
                m_Log.Info("Some information messages");
                var t = Task.Run(() => { 
                    File.WriteAllText("MyFile.txt", text);
                    DoSomeLogs(m_Log);
                });
                return t;
            }
        }

        private static void DoSomeLogs(Log<MyWorkerClassWithClassicLog> log)
        {
            using (log.AutoTrace("Nachricht von AutoTrace"))
            {
                log.Debug($"Log entry 1, Thread {Thread.CurrentThread.ManagedThreadId}.");
                log.Debug($"Log entry 2, Thread {Thread.CurrentThread.ManagedThreadId}.");
                log.Debug($"Log entry 3, Thread {Thread.CurrentThread.ManagedThreadId}.");
            }

            var exception = new NotImplementedException();

            log.Fatal("Log von Sandbox.Test");
            log.Fatal("Log von Sandbox.Test", exception);

            log.Error("Log von Sandbox.Test");
            log.Error("Log von Sandbox.Test", exception);

            log.Warning("Log von Sandbox.Test");
            log.Warning("Log von Sandbox.Test", exception);

            log.Info("Log von Sandbox.Test");
            log.Info("Log von Sandbox.Test", exception);

            log.Debug("Log von Sandbox.Test");
            log.Debug("Log von Sandbox.Test", exception);

            log.Trace("Log von Sandbox.Test");
            log.Trace("Log von Sandbox.Test", exception);
        }
    }
}