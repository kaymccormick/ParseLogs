using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace ParseLogsControls
{
    [TypeConverter(typeof(LogItemInfoToCommandsConverter))]
    [Serializable]
    [JsonObject]
    public class WpfLogEventInfo : ISerializable
    {
        public LogEventInfo EventInfo { get; set; }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
         info.AddValue("EventInfo", EventInfo, typeof(LogEventInfo));
        }
    }
}
