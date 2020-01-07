// using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using ParseLogsLib;

namespace ParseLogs.Test
{
    [TestFixture()]
    public class LogFinderTests
    {
        [Test(), Apartment(ApartmentState.MTA)]
        public void ExecuteTest()
        {
         //   LogFinder f = new LogFinder();

        }
    }
}