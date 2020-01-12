using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace LogAdjunct
{
    public class LogHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static object SerializeLogEventInfo(LogEventInfo entry)
        {
            var jsonOut = JsonConvert.SerializeObject(entry, new JsonSerializerSettings()
            {
            });
            Logger.Debug("serialization result is " + jsonOut);
            return jsonOut;
        }
    }
}
