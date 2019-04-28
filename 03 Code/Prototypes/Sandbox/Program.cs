using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Bluehands.Diagnostics.LogExtensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.NLogTarget;
using NLog;
using NLog.Targets.Syslog;

namespace Sandbox
{
    class Program
    {
        private static readonly Log s_Log = new Log<Program>();

        public static async Task Main()
        {
            Program.Test();
            Console.ReadLine();
            return;

            //ConfigureLogging();
            s_Log.Correlation = Guid.NewGuid().ToString();
            using (s_Log.AutoTrace())
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

            var myClass = new MyClass();
            myClass.MyComposingMethod("Hello logging").Wait();
            myClass.MyThrowingExceptionMethod();
            Console.ReadLine();
        }
        private static void Test()
        {
            //s_Log.Correlation = Guid.NewGuid().ToString();
            using (s_Log.AutoTrace("Nachricht von AutoTrace"))
            {
                s_Log.Debug($"Log entry 1, Thread {Thread.CurrentThread.ManagedThreadId}.");
                s_Log.Debug($"Log entry 2, Thread {Thread.CurrentThread.ManagedThreadId}.");
                s_Log.Debug($"Log entry 3, Thread {Thread.CurrentThread.ManagedThreadId}.");
            }

            var exeption = new NotImplementedException();

            s_Log.Fatal("Log von Sandbox.Test");
            s_Log.Fatal("Log von Sandbox.Test", exeption);

            s_Log.Error("Log von Sandbox.Test");
            s_Log.Error("Log von Sandbox.Test", exeption);

            s_Log.Warning("Log von Sandbox.Test");
            s_Log.Warning("Log von Sandbox.Test", exeption);

            s_Log.Info("Log von Sandbox.Test");
            s_Log.Info("Log von Sandbox.Test", exeption);

            s_Log.Debug("Log von Sandbox.Test");
            s_Log.Debug("Log von Sandbox.Test", exeption);

            s_Log.Trace("Log von Sandbox.Test");
            s_Log.Trace("Log von Sandbox.Test", exeption);
        }
        private static void ConfigureLogging()
        {
            if (LogManager.Configuration.FindTargetByName("syslog") is SyslogTarget syslogTarget)
            {
                var syslogServer = ConfigurationManager.AppSettings["Syslog.Server"];
                var port = ConfigurationManager.AppSettings["Syslog.Port"];
                syslogTarget.MessageSend.Tcp.Port = Convert.ToInt32(port);
                syslogTarget.MessageSend.Tcp.Server = syslogServer;
            }

            if (LogManager.Configuration.FindTargetByName("ai") is ApplicationInsightsTarget aiTarget)
            {
                aiTarget.InstrumentationKey = ConfigurationManager.AppSettings["AI.InstrumentationKey"];
            }


            LogManager.ThrowExceptions = true;
        }
    }
    public class MyClass
    {
        private readonly MyWorkerClass m_MyWorkerClass;
        private readonly Log<MyClass> m_Log = new Log<MyClass>();

        public MyClass()
        {
            m_MyWorkerClass = new MyWorkerClass();
        }

        public async Task<string> MyComposingMethod(string text)
        {
            m_Log.Correlation = Guid.NewGuid().ToString();
            using (m_Log.AutoTrace(() => $"The text parameter is '{text}'"))
            {
                try
                {
                    await m_MyWorkerClass.MyAsyncWorkerMethod(text);
                    m_MyWorkerClass.MySyncWorkerMethod(text);
                    return text;
                }
                catch (Exception ex)
                {
                    m_Log.Error("Something goes wrong", ex);
                    throw;
                }
            }
        }

        public void MyThrowingExceptionMethod()
        {
            try
            {
                throw new InvalidOperationException("Message of invalid operation");
            }
            catch (Exception ex)
            {
                m_Log.Error("Unexpected error", ex);
            }
        }

    }

    public class MyWorkerClass
    {
        private readonly Log<MyWorkerClass> m_Log = new Log<MyWorkerClass>();
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
                var t = Task.Run(() => File.WriteAllText("MyFile.txt", text));
                return t;
            }
        }
    }
}

