using NUnit.Framework;
using LogAdjunct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace LogAdjunctTest
{
    [TestFixture()]
    public class LogHelperTests
    {
        [Test()]
        public void SerializeLogEventInfoTest()
        {
            LogEventInfo entry = new LogEventInfo();
            LogAdjunct.LogHelper.SerializeLogEventInfo(entry);
        }

        [Test()]
        public void DeserializeLogEventInfoTest()
        {
            var json = "{ \"time\": \"2020-01-12 08:43:24.5903\", \"level\": \"DEBUG\", \"message\": \"CreateContract: NLog.LogEventInfo\" }";
            var deserializeLogEventInfo = LogAdjunct.LogHelper.DeserializeLogEventInfo(json);
            LogEventInfo entry = deserializeLogEventInfo;
        }

    }
}