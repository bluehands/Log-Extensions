﻿namespace Bluehands.Repository.Diagnostics.Log
{
    public class CallerInfo
    {
        public string TypeOfMessageCreator { get; private set; }
        public string ClassOfMessageCreator { get; private set; }
        public string MethodNameOfMessageCreator { get; private set; }
        
        public CallerInfo(string typeOfMessageCreator, string classOfMessageCreator, string methodNameOfMessageCreator)
        {
            TypeOfMessageCreator = typeOfMessageCreator;
            ClassOfMessageCreator = classOfMessageCreator;
            MethodNameOfMessageCreator = methodNameOfMessageCreator;
        }
    }
}
