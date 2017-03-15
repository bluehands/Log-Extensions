namespace Bluehands.Repository.Diagnostics.Log
{
    public class CallerInfo
    {
        public string TypeOfMessageCreator { get; private set; }
        public string ClassOfMessageCreator { get; private set; }
        public string MethodNameOfMessageCreator { get; private set; }
		public string ThreadIdOfMessageCreator { get; private set; }
        
        public CallerInfo(string typeOfMessageCreator, string classOfMessageCreator, string methodNameOfMessageCreator, string threadIdOfMessageCreator)
        {
            TypeOfMessageCreator = typeOfMessageCreator;
            ClassOfMessageCreator = classOfMessageCreator;
            MethodNameOfMessageCreator = methodNameOfMessageCreator;
	        ThreadIdOfMessageCreator = threadIdOfMessageCreator;
        }
    }
}
