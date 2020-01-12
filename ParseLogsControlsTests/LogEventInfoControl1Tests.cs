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
using LogAdjunct;
using Newtonsoft.Json;
using NLog;
using ParseLogsControlsTests;
using Formatting = System.Xml.Formatting;


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
                    ContractResolver = new ContractResolver(),
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
                ContractResolver = new ContractResolver(),
            });
            var json2 = JsonConvert.SerializeObject(info,
                Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    
                    ContractResolver = new ContractResolver(),
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
                ContractResolver = new ContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            });
        }






    }
}