using System;
using Microsoft.Extensions.Logging;
namespace Sandbox.Core
{
    public class MyClassWithInjectedILogger
    {
        private readonly ILogger<MyClassWithInjectedILogger> m_Logger;

        public MyClassWithInjectedILogger(ILogger<MyClassWithInjectedILogger> logger)
        {
            m_Logger = logger;
            m_Logger.SetCorrelation(Guid.NewGuid().ToString());
        }

        public void DoAction(string name)
        {
            using (m_Logger.AutoTrace("Scope1"))
            {
                m_Logger.LogDebug(() => "MyClass log for debug");
                m_Logger.LogInfo(() => "MyClass log for info");
                m_Logger.LogError(() => "MyClass log for error", new ArgumentException("Error message", nameof(name)));
            }

            using (m_Logger.BeginScope("Scope2"))
            {
                m_Logger.LogDebug(() => "MyClass log for debug in scope");
            }

            m_Logger.LogDebug(20, "Doing hard work but not using the extension. {0}", name);
        }
    }
}