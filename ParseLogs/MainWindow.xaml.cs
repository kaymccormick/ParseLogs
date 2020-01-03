using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;
using FolderBrowser.Views;
using ParseLogsLib;
using Path = System.IO.Path;

namespace ParseLogs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private App app = Application.Current as App;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private LogWindow _logWindow;

        public XDocument Document
        {
            get
            {
                return (FilesListView.SelectedItem != null ? (FilesListView.SelectedItem as LogItem).Document : null);
            }
            set { (FilesListView.SelectedItems[0] as LogItem).Document = value as XDocument; }
        }

        class XCol : CollectionViewSource
        {
            public Type CollectionViewType { get; set; } = typeof(ListCollectionView);

            public XCol() : base()
            {
            }
        }

        public MainWindow() : base()
        {
            LogFinder = new LogFinder();
            app.LogFinder = LogFinder;


            app = Application.Current as App;
            // s.Source = app.Files;
            // var d = CollectionViewSource.GetDefaultView(app.     );
            // int c = (from x in d.SortDescriptions select x).Count();
            // app.Files.Add(new Item(null));
            // Logger.Trace($"{c}");
            InitializeComponent();

            Logger.Debug($"Checkign resources");
            if (Resources.Count == 0)
            {
                Logger.Warn($"No resources in MainWindow");
            }
            else
            {
                foreach (var r in Resources.Keys)
                {
                    var v = TryFindResource(r);
                    Logger.Debug($"resource {r} = {v}");
                    if (!(v is string))
                    {
                        Logger.Debug($"Type is {v.GetType().Name});");
                        var typeConverter = TypeDescriptor.GetConverter(v.GetType(), true);
                        string vs;
                        if (typeConverter.CanConvertTo(typeof(string)))
                        {
                            vs = typeConverter.ConvertTo(v, typeof(string)) as string;
                        }
                        else
                        {
                            vs = "";
                        }

                        Logger.Debug($"value: {vs}");
                        if (v is ObjectDataProvider)
                        {
                            var od = v as ObjectDataProvider;
                            var d = od.Data;
                            Logger.Debug($"ObjectInstance is {od.ObjectInstance}, data is {d}");
                        } else if (v is CollectionViewSource)
                        {
                            var cvs = v as CollectionViewSource;
                            Logger.Debug($"View is {cvs.View}");
                        }
                    }
                }
            }

            ObjectDataProvider o = TryFindResource("DrivesProvider") as ObjectDataProvider;
            var typ = o.ObjectInstance.GetType().ToString();
            Logger.Info($"{o.ObjectInstance}");

            CommandBindings.Add(
                app.LogFinder.FindLogsCommandBinding);


            CommandManager.AddPreviewCanExecuteHandler(this, (sender, args) =>
            {
                CommandConverter con = new CommandConverter();
                string cmd = WhatCommand(args.Command);
                var convertFrom = con.ConvertToString(args.Command);
                //Logger.Info($"{convertFrom} {convertFrom.GetType().FullName}");
                Logger.Info(
                    $"Can execute {cmd} {args.Parameter} {args.Source} {args.OriginalSource} {args.RoutedEvent}");
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
            _logWindow = new LogWindow();
            _logWindow.ShowActivated = true;
            _logWindow.Loaded += (sender, args) =>
            {
                // var shell = new Shell32.Shell();
                // shell.TileHorizontally();
            };
            _logWindow.Show();

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

        private string WhatCommand(ICommand argsCommand)
        {
            if (argsCommand == Commands.FindLogsCommand)
            {
                return "FindLogsCommand";
            }


            return "unknown";
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
            _logWindow.Close();
        }

        private void DriveListBox_Initialized(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            DriveListBox.ItemsSource = drives;
        }
    }
}