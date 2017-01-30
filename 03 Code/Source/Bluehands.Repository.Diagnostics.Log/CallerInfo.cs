namespace Bluehands.Repository.Diagnostics.Log
{
    public class CallerInfo
    {
        public string TypeOfCallerOfGround { get; private set; }
        public string ClassNameOfGround { get; private set; }
        public string MethodNameOfCallerOfGround { get; private set; }
        
        public CallerInfo(string typeOfCallerOfGround, string classNameOfGround, string methodNameOfCallerOfGround)
        {
            TypeOfCallerOfGround = typeOfCallerOfGround;
            ClassNameOfGround = classNameOfGround;
            MethodNameOfCallerOfGround = methodNameOfCallerOfGround;
        }
    }
}
