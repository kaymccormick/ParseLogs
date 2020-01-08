using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using NLog;
using TB.ComponentModel;

namespace ParseLogs
{
    [ValueConversion(typeof(LogEventInfo), typeof(object[]))]
    public class EventLogInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LogEventInfo i = (LogEventInfo) value;
            //Dictionary<object, object> d = new Dictionary<object, object>();
            return i.ToDictionary().Select(pair => new object[] {pair.Key, pair.Value});
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
