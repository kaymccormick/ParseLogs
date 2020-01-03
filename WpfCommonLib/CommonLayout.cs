using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Layouts;

namespace WpfCommonLib
{
    class CommonLayout : Layout
    {
        public override void Precalculate(LogEventInfo logEvent)
        {
            base.Precalculate(logEvent);
        }

        protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
        {
            base.RenderFormattedMessage(logEvent, target);
        }

        protected override void InitializeLayout()
        {
            base.InitializeLayout();
        }

        protected override void CloseLayout()
        {
            base.CloseLayout();
        }

        protected override string GetFormattedMessage(LogEventInfo logEvent)
        {
            return logEvent.Message;
        }
    }
}
