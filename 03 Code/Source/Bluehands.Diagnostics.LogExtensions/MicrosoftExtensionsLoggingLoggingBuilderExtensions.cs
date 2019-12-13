using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Bluehands.Diagnostics.LogExtensions
{
    public static class MicrosoftExtensionsLoggingLoggingBuilderExtensions
    {
        public static ILoggingBuilder AddLogEnhancementWithNLog(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LogProvider>());
            return builder;
        }
    }
}