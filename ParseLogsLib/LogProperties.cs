using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text;
using NLog;

namespace ParseLogsLib
{
    public interface ILogInterface
    {
    }

    public class LogEntryFactory
    {
        public delegate LogEventInfo Factory();

        public LogEventInfo createLogEntry()
        {
            LogEventInfo r = new LogEventInfo();
            return r;
        }

        public class LogProperties
        {
            public static string Objects = typeof(LogProperties).FullName + ".Objects";
        }
    }
}
