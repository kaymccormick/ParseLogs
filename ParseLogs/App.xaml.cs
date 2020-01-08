using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Threading;
using Autofac;
using CommandLine;
using CommandLine.Text;
using EO.Internal;
using NHibernate.Util;
using NLog;
using ParseLogsLib;
//using WpfCommonLib;

namespace ParseLogs
{
    public enum ExitCode
    {
        Success = 0,
        GeneralError = 1,
        ArgumentsError = 2,
    }

    [Verb("ListResources", HelpText = "List resources available.")]

    public class ListResourcesOptions : IVerbOptions
    {
    }

    public interface IVerbOptions
    {
    }

    class Options
    {
        [Option('D', "debugOnly", Default = false)]
        public Boolean DebugOnly { get; set; }

        [Option(shortName: 'R', longName: "useRibbon", Default = true)]
        public Boolean UseRibbon { get; set; } = true;
    }


    public partial class App : Application
    {
        //public CommonManager CommonManager { get; }
        public Parser Parser { get; set; } = Parser.Default;
        public Type LogWindowType { get; set; } = null;//typeof(LogWindow);

        private System.Data.SQLite.SQLiteConnectionStringBuilder builder =
            new System.Data.SQLite.SQLiteConnectionStringBuilder();


        public App()
        {
            var parserResult = Parser.ParseArguments<ListResourcesOptions, Options>(Array.Empty<string>());

            PresentationTraceSources.Refresh();
            // LoggerConfigurer.PerformConfiguration = false;
            // CommonManager = CommonManager.Instance;
            // CommonManager.RegisterApplication(this);
        }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private ObservableCollection<LogEventInfo> _LogEventInfos { get; } = new ObservableCollection<LogEventInfo>();
        
        public ILogFinder LogFinder { get; set; }

        private static void ErrorExit(ExitCode exitcode = ExitCode.GeneralError)
        {
            if (exitcode != null)
            {
                object code = Convert.ChangeType(exitcode, exitcode.GetTypeCode());
                if (code != null)
                {
                    int intCode = (int) code;

                    Logger.Info($"Exiting with code {intCode}, {exitcode}");
                    if (System.Windows.Application.Current == null)
                    {
                        Logger.Debug("No application reference");
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                    else
                    {

                        System.Windows.Application.Current.Shutdown(intCode);
                    }
                }
            }
        }

        private void Application_DispatcherUnhandledException1(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error(e.Exception, "Unhandled");
            Exception inner = e.Exception.InnerException;
            while (inner != null)
            {
                Logger.Debug(inner, inner.Message);
            }
            ErrorExit(ExitCode.GeneralError);
            // foreach (var window in Windows)
            // {
            //     ((Window) window).Close();
            // }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var newArgs = new string[e.Args.Length + 1];
            newArgs[0] = Assembly.GetEntryAssembly().Location;
            e.Args.CopyTo(newArgs, 1);
            HandleArguments(args: newArgs);

            var target = MyCacheTarget.GetInstance(1000);
            target.Cache.SubscribeOn(Scheduler.Default).Buffer(TimeSpan.FromMilliseconds(100)).Where(x => x.Any()).ObserveOnDispatcher(DispatcherPriority.Background).Subscribe(infos =>
            {
                if (Pause) return;
                foreach (LogEventInfo info in infos)
                {
                    _LogEventInfos.Add(info);
                }
            });

        }

        public bool Pause { get; }

        private void HandleArguments(ICollection<string> args)
        {
            Logger.Debug("Parsing command line arguments {arguments}", args);
            StringBuilder b = new StringBuilder(200);
            TextWriter t = new StringWriter(b);
            Parser parser = new Parser(settings => { settings.HelpWriter = t; });

            ContainerBuilder bld = new ContainerBuilder();
            
            bld.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => typeof(IApplicationVerb).IsAssignableFrom(type)).AsImplementedInterfaces();
            var container = bld.Build();

            IEnumerable<IApplicationVerb> verbs = container.Resolve<IEnumerable<IApplicationVerb>>();
            Debug.Assert(verbs.Any());
            var parserResult = Parser.ParseArguments<ListResourcesOptions, Options>(args);
            try
            {
                parserResult.WithParsed<ListResourcesOptions>(
                        options => { }).WithParsed<Options>(options => { })
                    .WithNotParsed(errors =>
                    {
                        TypeConverter converter = TypeDescriptor.GetConverter(typeof(Usage));
                        var usages1 = CommandLine.Text.HelpText
                            .UsageTextAs(parserResult, example => example);
                        Logger.Debug(String.Join(", ", usages1.ToList().ConvertAll<string>(input =>
                            TypeDescriptor.GetConverter(typeof(UsageInfo)).ConvertToString(input))));

                        IEnumerable<Usage> usages = usages1
                            .ToList().ConvertAll<Usage>(input => (Usage) converter.ConvertFrom(input));
                        Window w = new Window();
                        var ctrl = new CommandLineParserMessages
                            {Usages = new UsagesFreezableCollection<Usage>(usages)};
                        w.Content = ctrl;
                        w.ShowDialog();
                        ErrorExit(ExitCode.ArgumentsError);

                        var r = new StringReader(b.ToString());
                        try
                        {
                            string s;
                            while ((s = r.ReadLine()) != null)
                            {
                                Logger.Error(s);
                            }

                            MessageBox.Show(b.ToString(), "Help text");
                            ErrorExit(ExitCode.ArgumentsError);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Exception {exception}", ex);
                        }
                    }).WithParsed<Options>(options =>
                    {
                        AppOptions = options;
                        if (AppOptions.DebugOnly)
                        {
                            //Logger.Debug("{StartupUri}.AbsolutePath);
                            var debugwindowXaml = "DebugWindow.xaml";
                            // StartupUri = new Uri(debugwindowXaml);
                            // Logger.Debug("Startup URI is {startupUri}", StartupUri);

                            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Uri));
                            if (converter.CanConvertFrom(typeof(string)))
                            {
                                StartupUri = (Uri) converter.ConvertFrom(debugwindowXaml);
                                Logger.Debug("Startup URI is {startupUri}", StartupUri);
                            }

                            // StartupUri.AbsolutePath  = debugwindowXaml;
                            // StartupUri = new Uri();
                            // Uri
                            //     startupUri= new Uri(debugwindowXaml);
                            // StartupUri = startupUri;
                        }

                        if (options.UseRibbon)
                        {
                            // Logger.Debug("Use ribbon enabled.");
                            // LogWindowType = typeof(LogRibbonWindow);
                        }
                    });
            }
            catch (Exception exception)
            {
                Logger.Error(exception, "{exception}", exception);
                ErrorExit(ExitCode.GeneralError);
            }
            finally
            {
                Parser.Dispose();
            }
        }

        Options AppOptions { get; set; }
    }

    public interface IApplicationVerb
    {
        string Name { get; }
        Type OptionsType { get; }
    }

    class TestVerb : IApplicationVerb
    {
        public TestVerb()
        {
            Name = nameof(TestVerb);
        }

        public string Name { get; }
        public Type OptionsType => typeof(TestVerbOptions);
        public TestVerbOptions Options { get; set; }
    }
    [Verb("ListResources", HelpText = "List resources available.")]
    public class TestVerbOptions
    {

    }
}