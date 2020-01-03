using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfCommonLib
{
    public class WpfWindowManager
    {
        public void StartMonitorThread()
        {
            Task.Run(() =>
            {
                Thread.Sleep(1000);

            });
        }
    }
}
