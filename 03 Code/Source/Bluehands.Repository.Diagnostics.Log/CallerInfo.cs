namespace Bluehands.Repository.Diagnostics.Log
{
    internal class CallerInfo
    {

		public string TypeOfMessageCreator { get; private set; }
        public string ClassOfMessageCreator { get; private set; }
        public string CallerMethodName { get; private set; }
		public string CallerContextId { get; private set; }
        
        public CallerInfo(string typeOfMessageCreator, string classOfMessageCreator, string callerCallerMethodName, string callerContextId)
        {
            TypeOfMessageCreator = typeOfMessageCreator;
            ClassOfMessageCreator = classOfMessageCreator;
            CallerMethodName = callerCallerMethodName;
	        CallerContextId = callerContextId;
        }

	}
}
