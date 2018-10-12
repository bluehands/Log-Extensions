using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;

namespace Bluehands.Diagnostics.LogExtensions
{
    public static class LogExtension
    {
        public static IApplicationBuilder UseLogCorrelation(this IApplicationBuilder builder, Log log)
        {
            return builder.Use(async (context, next) =>
            {
                var request = context.Request;
                var header = request.Headers.FirstOrDefault(kv => kv.Key.StartsWith("x-correlation", StringComparison.InvariantCultureIgnoreCase));
                log.Correlation = header.Value.FirstOrDefault();
                try
                {
                    using (log.AutoTrace(() => $"{request.Method}->{request.Path}"))
                    {
                        await next.Invoke();
                    }
                }
                catch (Exception ex)
                {
                    log.Debug("Unexpected error.", ex);
                    throw;
                }
                if (context.Response.StatusCode >= 400)
                {
                    log.Debug($"Request fails with code {context.Response.StatusCode}");
                }
            });
        }
        public static IApplicationBuilder UseRequestLogTracing(this IApplicationBuilder builder, Log log)
        {
            return builder.Use(async (context, next) =>
            {
                var request = context.Request;
                try
                {
                    using (log.AutoTrace(() => $"{request.Method}->{request.Path}"))
                    {
                        await next.Invoke();
                    }
                }
                catch (Exception ex)
                {
                    log.Debug("Unexpected error.", ex);
                    throw;
                }

                if (context.Response.StatusCode >= 400)
                {
                    log.Debug($"Request fails with code {context.Response.StatusCode}");
                }
            });
        }
    }
}
