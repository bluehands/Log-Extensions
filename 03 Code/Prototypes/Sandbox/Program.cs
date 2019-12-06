using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Bluehands.Diagnostics.LogExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;


namespace Sandbox.Example
{
    class Program
    {
        private static readonly Log s_Log = new Log<Program>();

        public static void Main()
        {
            LogManager.ThrowExceptions = true;
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                    .Build();

                var servicesProvider = BuildDi(config);
                using (servicesProvider as IDisposable)
                {
                    var runner = servicesProvider.GetRequiredService<Runner>();
                    runner.DoAction("Action1");


                    //DoSomeLogs();

                    Console.WriteLine("Press ANY key to exit");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }



        }

        private static void DoSomeLogs()
        {
            using (s_Log.AutoTrace("Nachricht von AutoTrace"))
            {
                s_Log.Debug($"Log entry 1, Thread {Thread.CurrentThread.ManagedThreadId}.");
                s_Log.Debug($"Log entry 2, Thread {Thread.CurrentThread.ManagedThreadId}.");
                s_Log.Debug($"Log entry 3, Thread {Thread.CurrentThread.ManagedThreadId}.");
            }

            var exception = new NotImplementedException();

            s_Log.Fatal("Log von Sandbox.Test");
            s_Log.Fatal("Log von Sandbox.Test", exception);

            s_Log.Error("Log von Sandbox.Test");
            s_Log.Error("Log von Sandbox.Test", exception);

            s_Log.Warning("Log von Sandbox.Test");
            s_Log.Warning("Log von Sandbox.Test", exception);

            s_Log.Info("Log von Sandbox.Test");
            s_Log.Info("Log von Sandbox.Test", exception);

            s_Log.Debug("Log von Sandbox.Test");
            s_Log.Debug("Log von Sandbox.Test", exception);

            s_Log.Trace("Log von Sandbox.Test");
            s_Log.Trace("Log von Sandbox.Test", exception);
        }

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
                .AddTransient<Runner>() // Runner is the custom class
                
                .AddLogging(loggingBuilder =>
                {
                    // configure Logging with NLog
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddFilter((c, l) =>
                        {
                            return true;
                        }
                    );
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddConsole(c =>
                    {
                        c.IncludeScopes = false;
                    });
                    loggingBuilder.AddLogEnhancementWithNLog();
                    //loggingBuilder.AddNLog(config);
                })
                .BuildServiceProvider();
        }

    }
    public class Runner
    {
        private readonly Microsoft.Extensions.Logging.ILogger<Runner> m_Logger;

        public Runner(ILogger<Runner> logger)
        {
            m_Logger = logger;
            m_Logger.SetCorrelation(Guid.NewGuid().ToString());
        }

        public void DoAction(string name)
        {
            using (m_Logger.AutoTrace())
            {
                m_Logger.LogDebug(() => "MyClass log for debug");
                m_Logger.LogInfo(() => "MyClass log for info");
                m_Logger.LogError(() => "MyClass log for error", new ArgumentException("Error message", nameof(name)));
            }

            using (m_Logger.BeginScope("xxxx"))
            {
                m_Logger.LogDebug(() => "MyClass log for debug in scope");
            }

            //m_Logger.LogDebug(20, "Doing hard work! {Action}", name);
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

