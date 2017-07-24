namespace Bluehands.Repository.Diagnostics.Log
{
    internal class CallerInfo
    {
        public string Type { get; private set; }
        public string Class { get; private set; }
        public string Method { get; private set; }
		public string CallContext { get; private set; }
        
        public CallerInfo(string creatorType, string creatorClass, string method, string callContext)
        {
            Type = creatorType;
            Class = creatorClass;
            Method = method;
	        CallContext = callContext;
        }

	}
}
