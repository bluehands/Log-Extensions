using System;
using System.Globalization;

namespace Bluehands.Repository.Diagnostics.Log
{
    public static class LogFormatters
    {
        public static Func<string, string> TraceEnter => message => $"{message} [Enter]";

        public static Func<string, double, string> TraceLeave => (message, runtime) => $"{message} [Leave {runtime.ToString(CultureInfo.InvariantCulture)}ms]";

        public static Func<string, string> ContextPart => caller => Guid.NewGuid().ToString("N").Substring(0,4);
        public static string ContextPartSeparator = "_";
    }
}