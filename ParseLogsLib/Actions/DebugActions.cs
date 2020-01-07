using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLogsLib.Actions
{
    public class DebugActions : IActionContainer
    {
        public Action DumpLogConfig { get; set; }
    }
}
