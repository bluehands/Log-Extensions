using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluehands.Diagnostics.Log;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new Log<Program>();
            log.Error("Das sollte nicht passieren");
        }
    }
}
