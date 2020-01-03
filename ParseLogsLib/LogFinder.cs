using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace ParseLogsLib
{
    public interface ILogFinder
    {
        CommandBinding FindLogsCommandBinding { get; }
        LogItemObservableList Files { get; }
        void Recurse(DirectoryInfo dir);
        void ProcessFile(FileInfo fsi);
        event RoutedEventHandler CommandFailed;
    }

    [Export(typeof(ILogFinder))]
    public class LogFinder : FrameworkElement, ILogFinder
    {
        public static readonly RoutedEvent CommandFailedEvent = EventManager.RegisterRoutedEvent("CommandFailed",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LogFinder));


        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public event RoutedEventHandler CommandFailed
        {
            add { AddHandler(CommandFailedEvent, value); }
            remove { RemoveHandler(CommandFailedEvent, value); }
        }


        //public Dispatcher Dispatcher { get; }
        public object FilesLock { get; } = new object();
        public Boolean isTraceLogging { get; set; } = true;

        public static readonly DependencyPropertyKey FilesPropertyKey = DependencyProperty.RegisterReadOnly("Files",
            typeof(LogItemObservableList), typeof(LogFinder),
            new FrameworkPropertyMetadata(new LogItemObservableList(),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnFilesChanged,
                CoerceValueCallback));

        public static readonly DependencyProperty FilesProperty = FilesPropertyKey.DependencyProperty;

        private static object CoerceValueCallback(DependencyObject d, object basevalue)
        {
            return basevalue;
        }

        private static void OnFilesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        //public ObservableCollection<LogItem> Files { get; } = new ObservableCollection<LogItem>();
        public LogItemObservableList Files
        {
            get
            {
                Logger.Info($"trying to access collection {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                return (LogItemObservableList) GetValue(FilesProperty);
            }
            set { SetValue(FilesProperty, value); }
        }

        public CommandBinding FindLogsCommandBinding { get; set; }

        public delegate void DispatcherDelegate();

        public LogFinder() : base()
        {
            Logger.Info("LogFinder constructor.");
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherDelegate(() =>
            {
                SetValue(FilesPropertyKey, new LogItemObservableList());
                BindingOperations.EnableCollectionSynchronization(Files, FilesLock);
            }));
            FindLogsCommandBinding = new CommandBinding(Commands.FindLogsCommand, Executed);
        }

        private delegate void RaiseDelegate();

        private void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lock (FilesLock)
            {
                Files.Clear();
            }

            CancellationTokenSource s = new CancellationTokenSource(
            );
            var token = s.Token;
            Task<TaskResult<int?>>.Run(() =>
            {
                int? count = null;
                try
                {
                    DirectoryInfo
                        dir = new DirectoryInfo(
                            @"e:\trillian"); ///@"H:\Users\root\AppData\Roaming\Trillian\users\paigeat");
                    Recurse(dir);

                    lock (FilesLock)
                    {
                        // if (isTraceLogging)
                        //   Logger.Trace("Files: " + String.Join(";", from file in Files select file.FullName));
                    }
                }
                catch (Exception ex)
                {
                    var appEx = new ApplicationException("Unable to recurse dir", ex);
                    Logger.Error(ex, appEx.Message);
                    StackTrace s3 = new StackTrace(ex);
                    //Logger.Debug($"{ex.StackTrace}");
                    Logger.Debug($"{ex}");

                    foreach (var f in s3.GetFrames())
                    {
                        //Logger.Info(f.ToString);
                    }

                    Dispatcher.BeginInvoke(DispatcherPriority.Normal,

                        new RaiseDelegate(() => { RaiseEvent(new RoutedEventArgs(CommandFailedEvent, this)); }));
                    return new TaskResult<int?>(null);
                }

                return new TaskResult<int?>(0);
            }, token);
        }

        public void Recurse(DirectoryInfo dir)
        {
            IEnumerable<DirectoryInfo> dirs;
            //Console.WriteLine(dir.FullName);
            try
            {
                dirs = dir.GetDirectories();
            }
            catch (IOException e)
            {
                LogItem item = new LogItem(dir.FullName, e);
                var msg = "Unabble to get directory info";
                var inner = (e.InnerException != null) ? e.InnerException.Message : "";
                Logger.Error(e, $"{msg}: {e.Message} {inner}");
                ApplicationException ex = new ApplicationException(msg, e);
                throw ex;
            }

            foreach (var dir2 in dirs)
            {
                this.Recurse(dir2);
            }

            IList<FileInfo> files = dir.GetFiles();
            if (isTraceLogging)
                Logger.Trace($"{files.Count}");
            try
            {
                foreach (var file in files)
                {
                    var isXml = file.Extension.ToLower().StartsWith(".xml");
                    if (isTraceLogging)
                        Logger.Trace($"{file.Extension} {isXml}");
                    if (isXml)
                    {
                        ProcessFile(file);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Unable to completely process files in {dir.FullName}");
                throw;
            }
        }

        public delegate void AddFileDelegate(FileInfo f);

        public void ProcessFile(FileInfo fsi)
        {
            lock (FilesLock)
            {
                if (isTraceLogging)
                    Logger.Trace($"adding {fsi.Name}");
                // Console.WriteLine();
                // Console.WriteLine($"count is {app.Files.Count}");


                Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        Files.Add(new LogItem(fsi));
                    }
                    catch (Exception e)
                    {
                        Logger.Trace(e, $"unable to add {fsi.Name}");
                        throw;
                    }
                }));
                // Console.WriteLine($"count is {app.Files.Count}");
            }
        }
    }
}