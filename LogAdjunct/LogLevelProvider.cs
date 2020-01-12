using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using NLog;

namespace LogAdjunct
{
    public class LogLevelProvider : IValueProvider
    {
        public void SetValue(object target, object value)
        {
            var item = target as LogEventInfo;
            var t = typeof(LogLevel);
            var f = t.GetField(value as string, BindingFlags.Static | BindingFlags.Public);
            item.Level = f.GetValue(null) as LogLevel;
        }

        public object GetValue(object target)
        {
            var item = target as LogEventInfo; 
            Debug.Assert(item != null);
            var dic = new Dictionary<string, string>();
            dic["Name"] = item.Level.ToString();
            return dic;
            //return item.Level;
            //return item.Level.ToString();
        }
    }
}