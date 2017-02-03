namespace Bluehands.Repository.Diagnostics.Log
{
    public class CallerInfo
    {
        public string TypeOfMessageCreator { get; private set; }
        public string ClassNameOfGround { get; private set; }
        public string MethodNameOfMessageCreator { get; private set; }
        
        public CallerInfo(string typeOfMessageCreator, string classNameOfGround, string methodNameOfMessageCreator)
        {
            TypeOfMessageCreator = typeOfMessageCreator;
            ClassNameOfGround = classNameOfGround;
            MethodNameOfMessageCreator = methodNameOfMessageCreator;
        }
    }
}
