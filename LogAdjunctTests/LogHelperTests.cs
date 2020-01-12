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
            Assert.Fail();
        }
    }
}