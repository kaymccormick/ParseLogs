using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using Newtonsoft.Json;
using NLog;
using XamlReader = System.Windows.Markup.XamlReader;

namespace ParseLogsControlsTests
{
    [TestFixture()]
    public class XamlLoadTests
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        [Test]
        public void loadXamlTest()
        {
            var root = Path.GetDirectoryName(typeof(XamlLoadTests).Assembly.Location);
            var dir = Path.Combine(root, "test_files/xaml");
            var context = XamlReader.GetWpfSchemaContext();
            foreach (var q in Directory.EnumerateFiles(dir))
            {
                Logger.Info($"Loading XAML file {q}");
                var result = XamlReader.Load(new FileStream(q, FileMode.Open));
                var xt = context.GetXamlType(result.GetType());
                var logXamlType = new XamlTypeLogger(Logger);
            }
        }
    }

    public class XamlTypeLogger
    {
        private NLog.Logger _logger;

        public XamlTypeLogger(Logger logger)
        {
            _logger = logger;
        }

        public void LogXamlType(XamlType theType)
        {
            try
            {
                var serializedJson = JsonConvert.SerializeObject(theType);
                _logger.Debug(
                    $"{nameof(XamlTypeLogger)}:{nameof(LogXamlType)}: Serialized XamType {theType} successfully.");
                _logger.Info(serializedJson);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}