using Android.Util;
using NLog.Config;

namespace NLog.Targets.Logcat
{
    [Target("Logcat")]
    public class LogcatTarget : TargetWithLayout
    {
        [RequiredParameter]
        public string Tag { get; set; }
        protected override void Write(LogEventInfo logEvent)
        {
            string logMessage = this.Layout.Render(logEvent);
            var priority = GetPriority(logEvent);
            Log.WriteLine(priority, Tag, logMessage);
        }

        private static LogPriority GetPriority(LogEventInfo logEvent)
        {
            var priority = LogPriority.Debug;
            if (logEvent.Level == LogLevel.Debug)
            {
                priority = LogPriority.Debug;
            }
            else if (logEvent.Level == LogLevel.Error)
            {
                priority = LogPriority.Error;
            }
            else if (logEvent.Level == LogLevel.Trace)
            {
                priority = LogPriority.Verbose;
            }
            else if (logEvent.Level == LogLevel.Fatal)
            {
                priority = LogPriority.Error;
            }
            else if (logEvent.Level == LogLevel.Info)
            {
                priority = LogPriority.Info;
            }
            else if (logEvent.Level == LogLevel.Warn)
            {
                priority = LogPriority.Warn;
            }
            return priority;
        }
    }
}
