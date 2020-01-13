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
                ContractResolver = new ContractResolver()
            });
            Logger.Debug("serialization result is " + jsonOut);
            return jsonOut;
        }

        public static LogEventInfo DeserializeLogEventInfo(string json)
        {
            var d = JsonConvert.DeserializeObject(json,
                new JsonSerializerSettings()
                {
                    ContractResolver = new ContractResolver()
                });
            Logger.Debug($"{d.GetType()}");
            LogEventInfo e = (LogEventInfo) d;
            return e;
        }
    }
}
