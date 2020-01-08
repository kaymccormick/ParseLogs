using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using SautinSoft;

namespace DocumentLogger
{
    [Target("FlowDocument")]
    public class FlowDocumentTarget : AsyncTaskTarget
    {
        private RichTextBox rtb = new RichTextBox();

        public void SetRTFText(string text)
        {
            MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(text));
            rtb.Selection.Load(stream, DataFormats.Rtf);
        }

        public FlowDocumentTarget()
        {
        }


        public delegate object MyDelegate();

        protected override Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            if (FlowDoc == null)
            {
                return Task.Run(() =>
                {
                    var me = this;
                    var dispatcherOperation = Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        (Func<object>) (() => Application.Current.FindResource(me.ResourceKey)),
                        cancellationToken);
                    dispatcherOperation.Task.ContinueWith((task) =>
                    {
                        lock (me.FlowDocLock)
                        {
                            me.FlowDoc = (FlowDocument) dispatcherOperation.Result;
                        }
                    }, cancellationToken);
                });
            }

            return Task.Run(() =>
            {
                string logMessage = this.RenderLogEvent(this.Layout, logEvent);
                SautinSoft.HtmlToRtf h = new HtmlToRtf();
                string rtf = String.Empty;
                if (h.OpenHtml(logMessage))
                {
                    rtf = h.ToRtf();
                    SetRTFText(rtf);
                    foreach (var block in rtb.Document.Blocks)
                    {
                        FlowDoc.Blocks.Add(block);
                    }
                }
            });
        }

        public string FieldName { get; set; }

        public string ClassName { get; set; }

        private FlowDocument FlowDoc = null;
        public object FlowDocLock = new object();
        public object ResourceKey { get; set; }
    }
}