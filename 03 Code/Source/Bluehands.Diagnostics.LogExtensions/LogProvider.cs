using Microsoft.Extensions.Logging;

namespace Bluehands.Diagnostics.LogExtensions
{
    public class LogProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new Log(categoryName);
        }
        public void Dispose()
        {

        }
    }
}