using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJ.Targets;
using NLog.Config;
using NLog.Targets;

namespace WpfCommonLib
{

    public class LoggerConfigurer
    {
        public static bool PerformConfiguration = true;
        public LoggerConfigurer()
        {
            if (PerformConfiguration)
            {
                var config = new LoggingConfiguration();
                CacheTarget cacheTarget = new CacheTarget();
                cacheTarget.Name = "WpfCommonLib.Cache1";
                ConsoleTarget console = new ConsoleTarget("ConsoleTarget1");
                CommonLayout layout = new CommonLayout();
                NLog.LogManager.Configuration = config;
            }
        }

    }
}
