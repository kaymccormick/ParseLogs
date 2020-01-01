using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace DocumentLogger
{
    public class FlowDocumentTarget : NLog.Targets.AsyncTaskTarget
    {
        protected override Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
