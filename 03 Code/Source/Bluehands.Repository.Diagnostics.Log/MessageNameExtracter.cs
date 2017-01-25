using System;
using System.Diagnostics;


namespace Bluehands.Repository.Diagnostics.Log
{
    public class MethodNameExtracter
    {
        private const int takeTheUnknowmCaller = 1;
        private const int takeTheUnknowmCallerOfTheUnknowmCaller = 2;

        public MethodNameExtracter()
        {
            CallerOfLogMessageWriter = string.Empty;
            CallerOfTheCallerOfLogMessageWriter = string.Empty;
        }

        public string CallerOfLogMessageWriter;
        public string CallerOfTheCallerOfLogMessageWriter;

        public void ExtractMethodNameFromStackTrace()
        {
            var stackTrace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });

            var frames = stackTrace.GetFrames();
            var ourType = typeof(LogMessageWriter);

            if (frames == null) return;
            for (var i = frames.Length - 1; i > 0; i--)
            {
                var frame = frames[i];
                var method = frame.GetMethod();
                var declaringType = method.DeclaringType;
                if (ourType == declaringType)
                {
                    if (i + takeTheUnknowmCaller < frames.Length)
                    {
                        CallerOfLogMessageWriter = frames[i + takeTheUnknowmCaller].GetMethod().Name;
                        CallerOfTheCallerOfLogMessageWriter =
                            frames[i + takeTheUnknowmCallerOfTheUnknowmCaller].GetMethod().Name;
                        return;

                    }
                }
            }
        }
    }
}
