using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Autofac;
using CommandLine;
using CommandLine.Text;
using EO.Internal;
using ParseLogsLib;
using WpfCommonLib;

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
        public CommonManager CommonManager { get; }
        public Parser Parser { get; set; } = Parser.Default;
        public Type LogWindowType { get; set; } = typeof(LogWindow);

        private System.Data.SQLite.SQLiteConnectionStringBuilder builder =
            new System.Data.SQLite.SQLiteConnectionStringBuilder();


        public App()
        {
            var parserResult = Parser.ParseArguments<ListResourcesOptions, Options>(Array.Empty<string>());

            NativeMethods.CBTProc x;
            PresentationTraceSources.Refresh();
            LoggerConfigurer.PerformConfiguration = false;

            CommonManager = CommonManager.Instance;
            CommonManager.RegisterApplication(this);
        }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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
        }

        private void HandleArguments(ICollection<string> args)
        {
            Logger.Debug("Parsing command line arguments {arguments}", args);
            StringBuilder b = new StringBuilder(200);
            TextWriter t = new StringWriter(b);
            Parser parser = new Parser(settings => { settings.HelpWriter = t; });

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
                            Logger.Debug("Use ribbon enabled.");
                            LogWindowType = typeof(LogRibbonWindow);
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


        private static class NativeMethods
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct MOUSEHOOKSTRUCT
            {
                public POINT pt; // Can't use System.Windows.Point because that has X,Y as doubles, not integer
                public IntPtr hwnd;
                public uint wHitTestCode;
                public IntPtr dwExtraInfo;

                public override string ToString()
                {
                    return $"({pt.X,4},{pt.Y,4})";
                }
            }

#pragma warning disable 649 // CS0649: Field 'MainWindow.NativeMethods.POINT.Y' is never assigned to, and will always have its default value 0
            public struct POINT
            {
                public int X;
                public int Y;
            }
#pragma warning restore 649

            // from WinUser.h
            public enum HookType
            {
                WH_MIN = (-1),
                WH_MSGFILTER = (-1),
                WH_JOURNALRECORD = 0,
                WH_JOURNALPLAYBACK = 1,
                WH_KEYBOARD = 2,
                WH_GETMESSAGE = 3,
                WH_CALLWNDPROC = 4,
                WH_CBT = 5,
                WH_SYSMSGFILTER = 6,
                WH_MOUSE = 7,
                WH_HARDWARE = 8,
                WH_DEBUG = 9,
                WH_SHELL = 10,
                WH_FOREGROUNDIDLE = 11,
                WH_CALLWNDPROCRET = 12,
                WH_KEYBOARD_LL = 13,
                WH_MOUSE_LL = 14
            }

            public enum HookCodes
            {
                HC_ACTION = 0,
                HC_GETNEXT = 1,
                HC_SKIP = 2,
                HC_NOREMOVE = 3,
                HC_NOREM = HC_NOREMOVE,
                HC_SYSMODALON = 4,
                HC_SYSMODALOFF = 5
            }

            public enum CBTHookCodes
            {
                HCBT_MOVESIZE = 0,
                HCBT_MINMAX = 1,
                HCBT_QS = 2,
                HCBT_CREATEWND = 3,
                HCBT_DESTROYWND = 4,
                HCBT_ACTIVATE = 5,
                HCBT_CLICKSKIPPED = 6,
                HCBT_KEYSKIPPED = 7,
                HCBT_SYSCOMMAND = 8,
                HCBT_SETFOCUS = 9
            }

            public delegate IntPtr CBTProc(int code, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(IntPtr hookPtr);

            [DllImport("user32.dll")]
            public static extern IntPtr CallNextHookEx(IntPtr hookPtr, int nCode, IntPtr wordParam, IntPtr longParam);

            [DllImport("user32.dll")]
            public static extern IntPtr SetWindowsHookEx(HookType hookType, CBTProc hookProc, IntPtr instancePtr,
                uint threadID);

            [DllImport("kernel32.dll")]
            public static extern uint GetCurrentThreadId();
        }
    }
}