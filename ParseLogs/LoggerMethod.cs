using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLogs
{
    public class Target : NLog.Targets.Target
    {

    }
    public static class LoggerMethod
    {
        public static void Receive(params object[] p)
        {
            Console.WriteLine(p[0].ToString());
        }
    }
}
