using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Bluehands.Diagnostics.LogExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Sandbox.Core;


namespace Sandbox.Example
{
    class Program
    {
        public static async Task Main()
        {
            LogManager.ThrowExceptions = true;
            AppDomain.CurrentDomain.Load("NLog.Targets.Syslog");
            AppDomain.CurrentDomain.Load("NLog.Extensions.AzureStorage");
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                .Build();

            var servicesProvider = new ServiceCollection()
                .AddTransient<MyClassWithInjectedILogger>()
                .AddLogging(loggingBuilder =>
                {
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
                    //configure Logging with NLog classic for .net core
                    //loggingBuilder.AddNLog(config);
                })
                .BuildServiceProvider();

            var myClass = servicesProvider.GetRequiredService<MyClassWithInjectedILogger>();
            myClass.DoAction("Action1");

            servicesProvider.Dispose();

            var myOtherClass = new MyClassWithClassicLog();
            var s = await myOtherClass.MyComposingMethod("Hello World");


            Console.WriteLine("Press ANY key to exit");
            Console.ReadKey();

        }
        



    }
}

