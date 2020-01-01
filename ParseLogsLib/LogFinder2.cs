using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace ParseLogsLib
{
    public interface ILogFinder2
    {
        CommandBinding FindLogsCommandBinding { get; }
        ObservableCollection<LogItem> Files { get; }
        void Recurse(DirectoryInfo dir);
        void ProcessFile(FileInfo fsi);
    }

    [Export(typeof(ILogFinder2))]
    public class LogFinder2 : DependencyObject, ILogFinder2
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public Dispatcher Dispatcher { get; }
        public object FilesLock { get; } = new object();

        public static readonly DependencyPropertyKey FilesPropertyKey = DependencyProperty.RegisterReadOnly("Files",
            typeof(ICollection<LogItem>), typeof(LogFinder),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnFilesChanged,
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
        public ObservableCollection<LogItem> Files
        {
            get { return (ObservableCollection<LogItem>) GetValue(FilesProperty); }
            set { SetValue(FilesProperty, value); }
        }
        
        public CommandBinding FindLogsCommandBinding { get; set; }

        public LogFinder2() : base()
        {
            try
            {
                SetValue(FilesPropertyKey, new ObservableCollection<LogItem>());
            }
            catch (Exception e)
            {
                Logger.Trace(e);
                throw;
            }
            BindingOperations.EnableCollectionSynchronization(Files, FilesLock);
            FindLogsCommandBinding = new CommandBinding(Commands.FindLogsCommand, Executed);
        }
        private void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lock (FilesLock)
            {
                Files.Clear();
            }

            CancellationTokenSource s = new CancellationTokenSource(
            );
            var token = s.Token;
            Task.Run(() =>
            {
                DirectoryInfo dir = new DirectoryInfo(@"c:\temp");///@"H:\Users\root\AppData\Roaming\Trillian\users\paigeat");
                Recurse(dir);
                lock (FilesLock)
                {
                    Logger.Trace("xx" + String.Join(";", from file in Files select file.FullName));
                }
            }, token);
        }

        public void Recurse(DirectoryInfo dir)
        {
            IEnumerable<DirectoryInfo> dirs;
            //Logger.Trace(dir.FullName);
            try
            {
                dirs = dir.GetDirectories();
            }
            catch (IOException e)
            {
                LogItem item = new LogItem(dir.FullName, e);
                Logger.Trace(e);
                throw;
            }

            foreach (var dir2 in dirs)
            {
                this.Recurse(dir2);
            }

            IList<FileInfo> files = dir.GetFiles();
            Logger.Trace($"{files.Count}");
            foreach (var file in files)
            {
                var isXml = file.Extension.ToLower().StartsWith(".xml");
                Logger.Trace($"{file.Extension} {isXml}");
                if (isXml)
                {
                    ProcessFile(file);
                }
            }
        }

        public delegate void AddFileDelegate(FileInfo f);

        public void ProcessFile(FileInfo fsi)
        {
            lock (FilesLock)
            {
                // Logger.Trace($"adding {fsi.Name}");
                // Logger.Trace($"count is {app.Files.Count}");

                Files.Add(new LogItem(fsi));
                // Logger.Trace($"count is {app.Files.Count}");
            }
        }
    }

}