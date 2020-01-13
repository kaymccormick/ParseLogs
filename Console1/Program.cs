using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Console1
{
    class Program
    {
        static void Main(string[] args)
        {
            LogAdjunct.LogHelper.SerializeLogEventInfo(new LogEventInfo());
        }
    }
}
