using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Newtonsoft.Json;
using NLog;

namespace ParseLogsControls
{
    [MarkupExtensionReturnType(typeof(object))]
    public class JsonStoreMarkupExtension : MarkupExtension
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private string _x = "";

        public JsonStoreMarkupExtension()
        {
        }

        public JsonStoreMarkupExtension(WpfLogEventInfo i)
        {
        }

        public JsonStoreMarkupExtension(string x)
        {
            _x = x;
        }

        [ConstructorArgument("x")]
        public string X
        {
            get { return _x; }
            set
            {
                _x = value;
                Logger.Debug("Prop x set to {_x}");
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            byte[] b = System.Convert.FromBase64String(X);
            var json = System.Text.Encoding.UTF8.GetString(b);
            WpfLogEventInfo i = JsonConvert.DeserializeObject<WpfLogEventInfo>(json);
            return i;
        }
    }
}