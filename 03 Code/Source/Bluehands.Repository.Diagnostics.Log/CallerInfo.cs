using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluehands.Repository.Diagnostics.Log
{
    public class CallerInfo
    {
        public readonly string NamespaceOfCallerOfLog;
        public readonly string ClassNameOfLog;
        public readonly string MethodNameOfCallerOfLog;

        public CallerInfo()
        {

        }

        public CallerInfo(string namespaceOfCallerOfLog, string classNameOfLog, string methodNameOfCallerOfLog)
        {
            NamespaceOfCallerOfLog = namespaceOfCallerOfLog;
            ClassNameOfLog = classNameOfLog;
            MethodNameOfCallerOfLog = methodNameOfCallerOfLog;
        }
    }
}
