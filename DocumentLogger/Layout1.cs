using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.LayoutRenderers;
using NLog.Layouts;

namespace DocumentLogger
{
    [LayoutRenderer("LayoutRenderer1")]
    class Layout1 : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            Debug.WriteLine(builder.GetHashCode());
        }
    }
}
