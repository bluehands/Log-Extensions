using System;
using System.Collections.Generic;

namespace Bluehands.Diagnostics.LogExtensions
{
    internal class LogEventInfo
    {
        public LogLevel Level { get; set; }
        public string Indent { get; set; }
        public string TypeName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string CallContext { get; set; }
        public string Correlation { get; set; }
        public Func<string> MessageFactory { get; set; }
        public KeyValuePair<string, string>[] CustomProperties { get; set; }
    }
}