namespace Bluehands.Diagnostics.LogExtensions
{
    internal class CallerInfo
    {
        public string Type { get; }
        public string Class { get;}
        public string Method { get;}
		public string CallContext { get; }
        public string Correlation { get; }

        public CallerInfo(string creatorType, string creatorClass, string method, string callContext, string correlation)
        {
            Type = creatorType;
            Class = creatorClass;
            Method = method;
	        CallContext = callContext;
            Correlation = correlation;
        }
	}
}
