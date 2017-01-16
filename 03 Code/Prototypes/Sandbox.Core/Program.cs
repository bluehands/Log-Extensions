using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var st = (StackTrace) Activator.CreateInstance(typeof (StackTrace), new object[] {});
        }
    }
}
