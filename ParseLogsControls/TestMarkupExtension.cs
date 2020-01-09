using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Newtonsoft.Json;
using NLog;

namespace ParseLogsControls
{

    [MarkupExtensionReturnType(typeof(object))]
    public class TestMarkupExtension : MarkupExtension
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private string _x = "";

        public TestMarkupExtension()
        {
        }

	public TestMarkupExtension(WpfLogEventInfo i)
        {
        }

        public TestMarkupExtension(string x)
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
            FromBase64Transform from = new FromBase64Transform();
            byte[] b = System.Convert.FromBase64String(X);
            var json = System.Text.Encoding.UTF8.GetString(b);
            WpfLogEventInfo i = JsonConvert.DeserializeObject<WpfLogEventInfo>(json);
            return i;
        }
    }
}
