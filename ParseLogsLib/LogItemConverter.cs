using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using NLog;

namespace ParseLogsLib
{
    class LogItemConverter 
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsSubclassOf(typeof(LogItem)))
            {
                return null;
            }

            if (value is string)
            {
                LogItem item = new LogItem((string)value);
                return item;
            }
            LogItem newVal = null;
            if (value !=  null)
            {
                newVal = value as LogItem;
            }

            return (object) newVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as LogItem).FullName;
        }
    }
}
