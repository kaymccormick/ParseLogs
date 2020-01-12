using NUnit.Framework;
using ParseLogsControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NLog;
using ParseLogsControlsTests;
using Formatting = System.Xml.Formatting;
using WriteState = System.Xml.WriteState;


namespace ParseLogsControls.Test
{

//     [SetUpFixture]
//     public class Helper
//     {
//         [OneTimeSetUp]
//         public void setUp()
//         {
//             Assembly.Load("NLog.dll");
//         }
//
//         //System.Threading.Thread.CurrentThread.
//   //          AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromSameFolderResolveEventHandler);
// //        }
//
//
//         public Assembly LoadFromSameFolderResolveEventHandler(object sender, ResolveEventArgs args)
//         {
//             string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
//             string assemblyPath = Path.Combine(folderPath, args.Name + ".dll");
//             Assembly assembly = Assembly.LoadFrom(assemblyPath);
//             return assembly;
//         }
//     }

    public class TestXmlWriter : XmlWriter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private XmlWriter xmlWriterImplementation;

        public TestXmlWriter(XmlWriter xmlWriterImplementation)
        {
            this.xmlWriterImplementation = xmlWriterImplementation;
        }

        public override void WriteStartDocument()
        {
            Logger.Trace(nameof(WriteStartDocument));
            xmlWriterImplementation.WriteStartDocument();
        }

        public override void WriteStartDocument(bool standalone)
        {
            Logger.Trace(nameof(WriteStartDocument));
            xmlWriterImplementation.WriteStartDocument(standalone);
        }

        public override void WriteEndDocument()
        {
            xmlWriterImplementation.WriteEndDocument();
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            xmlWriterImplementation.WriteDocType(name, pubid, sysid, subset);
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            Logger.Trace(nameof(WriteStartElement) + $" {prefix} {localName} {ns}");
            xmlWriterImplementation.WriteStartElement(prefix, localName, ns);
        }

        public override void WriteEndElement()
        {
            xmlWriterImplementation.WriteEndElement();
        }

        public override void WriteFullEndElement()
        {
            xmlWriterImplementation.WriteFullEndElement();
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            xmlWriterImplementation.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteEndAttribute()
        {
            xmlWriterImplementation.WriteEndAttribute();
        }

        public override void WriteCData(string text)
        {
            xmlWriterImplementation.WriteCData(text);
        }

        public override void WriteComment(string text)
        {
            xmlWriterImplementation.WriteComment(text);
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            xmlWriterImplementation.WriteProcessingInstruction(name, text);
        }

        public override void WriteEntityRef(string name)
        {
            xmlWriterImplementation.WriteEntityRef(name);
        }

        public override void WriteCharEntity(char ch)
        {
            xmlWriterImplementation.WriteCharEntity(ch);
        }

        public override void WriteWhitespace(string ws)
        {
            xmlWriterImplementation.WriteWhitespace(ws);
        }

        public override void WriteString(string text)
        {
            xmlWriterImplementation.WriteString(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            xmlWriterImplementation.WriteSurrogateCharEntity(lowChar, highChar);
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            xmlWriterImplementation.WriteChars(buffer, index, count);
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            xmlWriterImplementation.WriteRaw(buffer, index, count);
        }

        public override void WriteRaw(string data)
        {
            xmlWriterImplementation.WriteRaw(data);
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            xmlWriterImplementation.WriteBase64(buffer, index, count);
        }

        public override void Flush()
        {
            xmlWriterImplementation.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            return xmlWriterImplementation.LookupPrefix(ns);
        }

        public override WriteState WriteState => xmlWriterImplementation.WriteState;
    }

    [TestFixture()]
    public class LogEventInfoControl1Tests
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        [SetUp]
        public void SetUp()
        {
            Application app = new Application();
            var assemblyFullName = typeof(LogEventInfoControl1).Assembly.FullName;
            var assPart = Uri.EscapeUriString(assemblyFullName.ToString());
            var stream = Application.GetResourceStream(new Uri(
                $"pack://application:,,,/{assPart};component/dictionary.xaml",
                UriKind.RelativeOrAbsolute));
            System.Uri resourceLocater =
                new System.Uri("/ParseLogsControls;component/dictionary.xaml", System.UriKind.Relative);
            app.Resources = (ResourceDictionary) Application.LoadComponent(resourceLocater);
            MyApp = app;
        }

        [TearDown]
        public void TearDown()
        {
            MyApp.Shutdown();
        }

        public Application MyApp { get; set; }

        [Test(), Apartment(ApartmentState.STA)]
        public void LogEventInfoControl1Test()
        {
            try
            {
                var name = typeof(LogEventInfoControl1).Assembly.GetName().Name;

                WindowWrap<LogEventInfoControl1> wrap = new WindowWrap<LogEventInfoControl1>();
                wrap.Loaded += (sender, args) =>
                {
                    var stringWriter = new StringWriter();
                    try
                    {
                        XmlTextWriter textWriter = new XmlTextWriter(stringWriter);
                        textWriter.Formatting = Formatting.Indented;
                        XmlWriter w = new TestXmlWriter(textWriter);
                        XamlWriter.Save(wrap.Content, w);
                        Logger.Info($"{stringWriter}");
                        var fileStream = new StreamWriter(File.OpenWrite("c:\\data\\logs\\example.xml"));
                        fileStream.WriteLine(stringWriter.ToString());

                        wrap.Dispatcher.Invoke(() => wrap.Close());
                    }
                    catch (Exception e)
                    {
                        Logger.Debug(stringWriter);
                        Console.WriteLine(e);
                        throw;
                    }
                };

                MyApp.Run(wrap);
            }
            catch (XamlParseException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Unable to save runtime instance to XAML");
                Logger.Debug(e, $"{e.Message}");
                Console.WriteLine(e);
                throw;
            }

            // LogEventInfoControl1 test = new LogEventInfoControl1();
            // test.Instance = new LogEventInfo(LogLevel.Debug, "testLogger", "eat food");
            // Window w = new Window();
            // w.Content = test;
            // MyApp.Run(w);
            //
            //Assert.Fail();
        }

        [Test]
        public void Test2()
        {
            Logger.Error("here");
            var logEventInfo = new LogEventInfo(LogLevel.Debug, "testlogger", "message");
            var json = JsonConvert.SerializeObject(logEventInfo,
                Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new Contract(),
                });
            var fileStream = File.Open(@"c:\data\loginfo.json", FileMode.Create);
            var writer = new StreamWriter(fileStream);
            writer.Write(json);
            writer.Close();
            return;
//            fileStream.Close();

            Console.WriteLine(json);
            LogEventInfo info = JsonConvert.DeserializeObject<LogEventInfo>(json, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new Contract(),
            });
            var json2 = JsonConvert.SerializeObject(info,
                Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    
                    ContractResolver = new Contract(),
                });
            Assert.AreEqual(json, json2);
        }

        [Test]
        public void Test3()
        {
            var json = File.ReadAllText(@"C:\data\loginfo.json");
            LogEventInfo info = JsonConvert.DeserializeObject<LogEventInfo>(json, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new Contract(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            });
        }






    }
}