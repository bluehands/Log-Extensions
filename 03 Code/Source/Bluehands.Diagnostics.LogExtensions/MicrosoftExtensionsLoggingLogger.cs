using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
// ReSharper disable UnusedMember.Global

namespace Bluehands.Diagnostics.LogExtensions
{
    public static class MicrosoftExtensionsLoggingLogger
    {
        public static IDisposable AutoTrace<T>(this ILogger<T> logger, string message = "", [CallerMemberName] string caller = "")
        {
            return logger.AutoTrace(() => message, caller);
        }

        public static IDisposable AutoTrace<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            if (!logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Trace))
            {
                return DefaultDisposable.Instance;
            }
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            return new AutoTrace(writer, messageFactory, caller);
        }

        public static void Fatal<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.Fatal(messageFactory, null, caller);
        }

        public static void Fatal<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(LogLevel.Fatal, messageFactory, caller, ex);
        }

        public static void Error<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.Error(messageFactory, null, caller);
        }

        public static void Error<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(LogLevel.Error, messageFactory, caller, ex);
        }

        public static void Warning<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.Warning(messageFactory, null, caller);
        }

        public static void Warning<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(LogLevel.Warning, messageFactory, caller, ex);
        }

        public static void Info<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.Info(messageFactory, null, caller);
        }

        public static void Info<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(LogLevel.Info, messageFactory, caller, ex);
        }

        public static void Debug<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.Debug(messageFactory, null, caller);
        }

        public static void Debug<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(LogLevel.Debug, messageFactory, caller, ex);
        }
        
        public static void Trace<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.Trace(messageFactory, null, caller);
        }

        public static void Trace<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(LogLevel.Trace, messageFactory, caller, ex);
        }

        public static void SetCorrelation<T>(this ILogger<T> logger, string value)
        {
            TrackCorrelation.Correlation = value;
        }
    }
}
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Logging
{
    using Bluehands.Diagnostics.LogExtensions;
    public static class MicrosoftExtensionsLoggingLogger
    {
        public static IDisposable AutoTrace<T>(this ILogger<T> logger, string message = "", [CallerMemberName] string caller = "")
        {
            return logger.AutoTrace(() => message, caller);
        }

        public static IDisposable AutoTrace<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            if (!logger.IsEnabled(LogLevel.Trace))
            {
                return DefaultDisposable.Instance;
            }
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            return new AutoTrace(writer, messageFactory, caller);
        }

        public static void LogFatal<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.LogFatal(messageFactory, null, caller);
        }

        public static void LogFatal<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(Bluehands.Diagnostics.LogExtensions.LogLevel.Fatal, messageFactory, caller, ex);
        }

        public static void LogError<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.LogError(messageFactory, null, caller);
        }

        public static void LogError<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(Bluehands.Diagnostics.LogExtensions.LogLevel.Error, messageFactory, caller, ex);
        }

        public static void LogWarning<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.LogWarning(messageFactory, null, caller);
        }

        public static void LogWarning<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(Bluehands.Diagnostics.LogExtensions.LogLevel.Warning, messageFactory, caller, ex);
        }

        public static void LogInfo<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.LogInfo(messageFactory, null, caller);
        }

        public static void LogInfo<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(Bluehands.Diagnostics.LogExtensions.LogLevel.Info, messageFactory, caller, ex);
        }

        public static void LogDebug<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.LogDebug(messageFactory, null, caller);
        }

        public static void LogDebug<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(Bluehands.Diagnostics.LogExtensions.LogLevel.Debug, messageFactory, caller, ex);
        }

        public static void LogTrace<T>(this ILogger<T> logger, Func<string> messageFactory, [CallerMemberName] string caller = "")
        {
            logger.LogTrace(messageFactory, null, caller);
        }

        public static void LogTrace<T>(this ILogger<T> logger, Func<string> messageFactory, Exception ex, [CallerMemberName] string caller = "")
        {
            var writer = new MicrosoftExtensionsLoggingLogMessageWriter<T>(logger);
            writer.WriteLogEntry(Bluehands.Diagnostics.LogExtensions.LogLevel.Trace, messageFactory, caller, ex);
        }

        public static void SetCorrelation<T>(this ILogger<T> logger, string value)
        {
            TrackCorrelation.Correlation = value;
        }
    }
}