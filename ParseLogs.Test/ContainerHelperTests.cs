using NUnit.Framework;
using ParseLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ParseLogsLib;
using ParseLogsLib.LogProperties;

namespace ParseLogsLib.LogProperties
{
}

namespace ParseLogs.Test
{
    
    [TestFixture()]
    public class ContainerHelperTests
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();



        [Test()]
        public void InitializeContainerTest()
        {
            ContainerHelper ch = new ContainerHelper();
            ch.InitializeContainer();

            Assert.NotNull(ch.Container);

            LogEventInfo le = new LogEventInfo(LogLevel.Debug, null, "");
            
            foreach (var x in ch.Container.ComponentRegistry.Registrations)
            {
                foreach (var s in x.Services)
                {
                    Logger.Debug($"{x} {s} {s}");
                }
            }
        }

    
        
    }
}