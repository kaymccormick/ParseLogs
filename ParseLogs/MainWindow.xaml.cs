using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;
using EO.Internal;
using FolderBrowser.Views;
using NLog;
using ParseLogsLib;
using Path = System.IO.Path;

namespace ParseLogs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private static App app = Application.Current as App;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private Window _logWindow;
        public static readonly DependencyProperty LogWindowTypeProperty = DependencyProperties.LogWindowTypeProperty;
        public Type LogWindowType
        {
            get { return (Type)GetValue(DependencyProperties.LogWindowTypeProperty); }
            set { SetValue(DependencyProperties.LogWindowTypeProperty, value); }
        }

        public XDocument Document
        {
            get
            {
                return (FilesListView.SelectedItem != null ? (FilesListView.SelectedItem as LogItem).Document : null);
            }
            set { (FilesListView.SelectedItems[0] as LogItem).Document = value as XDocument; }
        }

        public MainWindow() : base()
        {
            LogFinder = new LogFinder();
            app.LogFinder = LogFinder;

            // s.Source = app.Files;
            // var d = CollectionViewSource.GetDefaultView(app.     );
            // int c = (from x in d.SortDescriptions select x).Count();
            // app.Files.Add(new Item(null));
            // Logger.Trace($"{c}");
            InitializeComponent();

            Binding binding = new Binding
            {
                Source = Application.Current,
                Path = new PropertyPath("LogWindowType")
            };
            SetBinding(LogWindowTypeProperty, binding);
            
            // DebugWindow debugWindow = new DebugWindow();
            // debugWindow.Show();

            foreach (FieldInfo f in typeof(StartupActions).GetFields(
                BindingFlags.Static | BindingFlags.Public | BindingFlags.ExactBinding))
            {
                var fieldValue = f.GetValue(null);
                Delegate d = fieldValue as Delegate;
                if (d != null)
                {
                    if (!d.GetMethodInfo().GetParameters().Any())
                    {
                        Action a = d as Action;
                        Debug.Assert(a != null);
                        Task.Run(a);
                    }
                }
            }

            // ConsoleWindow consoleWindow = new ConsoleWindow();
            // consoleWindow.Show();
             if (LogWindowType != null)
            {
                _logWindow = LogWindowType.GetConstructor(Type.EmptyTypes).Invoke(null) as Window;
                //_logWindow.ShowActivated = true;
                _logWindow.Show();
            }

            Task.Run(() =>
            {
            });


            ObjectDataProvider o = TryFindResource("DrivesProvider") as ObjectDataProvider;
            var typ = o.ObjectInstance.GetType().ToString();
            Logger.Info($"{o.ObjectInstance}");

            CommandBindings.Add(
                app.LogFinder.FindLogsCommandBinding);

            CommandManager.AddPreviewExecutedHandler(this, (sender, args) =>
            {
                CommandConverter con = new CommandConverter();
                string cmd = WhatCommand(args.Command);
                var convertFrom = con.ConvertToString(args.Command);
                //Logger.Info($"{convertFrom} {convertFrom.GetType().FullName}");
                Logger.Info(
                    $" executed {cmd} {args.Parameter} {args.Source} {args.OriginalSource} {args.RoutedEvent}");
            });

            // CollectionViewSource s = new CollectionViewSource();
            // var def = CollectionViewSource.GetDefaultView(LogFinder.Files);
            // Logger.Debug($"{def}");
            // Logger.Debug($"{def}");
            //
            //
            // CollectionViewSource src = new XCol();
            //
            // Binding binding = new Binding
            // {
            //     Source = def
            // };
            //
            // FilesListView.SetBinding(ItemsControl.ItemsSourceProperty,binding);
            //
            // //            FilesListView.ItemsSource = app.Files;
            //            FilesListView.
            /*            Binding binding = new Binding("FullName")
                        {
                            Source = this.Files

                        };

                        var itemsSourceProperty = ItemsControl.ItemsSourceProperty;

                        BindingOperations.SetBinding(FilesListView, itemsSourceProperty, binding);
                        */
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var typeInfos = assembly.DefinedTypes.Where((info, i) => info.IsSubclassOf(typeof(TraceSource)));
                if (typeInfos.Count() > 0)
                {
                    var s2 = String.Join(" ",
                        typeInfos);
                    var @join = s2;
                    NLog.Logger l = NLog.LogManager.GetLogger(
                        Path.GetFileName(assembly.Location));
                    foreach (var t in typeInfos)
                    {
                        l.Info(t.Name);
                    }

                    //l.Info($"{assembly.Location} {@join}");
                }
            }
        }

        private void LogInstance(object instance, FieldInfo field, LogLevel loglevel)
        {
            //LogEventInfo event = new LogEventInfo(loglevel, null, )
            throw new NotImplementedException();
        }

        private string WhatCommand(ICommand argsCommand)
        {
            if (argsCommand == ParseLogsCommands.FindLogsCommand)
            {
                return "FindLogsCommand";
            }
            else if (argsCommand == DebugCommands.DumpResources)
            {
                return "DebugCommands.DumpResources";
            }

            var nameProp = argsCommand.GetType().GetProperty("Name");
            if (nameProp != null)
            {
                object o = nameProp.GetValue(argsCommand);
                //Logger.Debug($"Command has name of {o}");
                return o.ToString();
            }   

            return $"{argsCommand}";
        }


        private void MainWindow_OnInitialized(object sender, EventArgs e)
        {
            SearchButton = _searchButton;
            Logger.Debug($"{Thread.CurrentThread.ManagedThreadId}");
        }

        public LogFinder LogFinder { get; set; }
        public Button SearchButton { get; set; }

        private static void ErrorExit()
        {
            System.Windows.Application.Current.Shutdown(1);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Logger.Info("mainWindow " +
                        "loaded");
            //_searchButton.Command.Execute(null);
            //ErrorExit();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            if (_logWindow != null) _logWindow.Close();
        }

        private void DriveListBox_Initialized(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            DriveListBox.ItemsSource = drives;
        }
    }
}