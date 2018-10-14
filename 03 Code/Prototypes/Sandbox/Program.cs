using System;
using System.IO;
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

            var myClass = new MyClass();
            myClass.MyComposingMethod("Hello logging").Wait();
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

