using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Sandbox
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetLogger("MyClass");

        static void Main(string[] args)
        {
            var st = new StackTrace();

            //Logger logger = LogManager.GetLogger("foo");

            logger.Fatal("Geht dieser?");


            var x = string.Format("Benutzer {0} hat sich angemeldet", "Aydin");


            //var log = new Log<Program>();
            //log.Debug("Das sollte nicht passieren");
            //log.Warning("Bitte anzeigen");
            //log.Fatal("Bitte anzeigen");
        }
    }
}
