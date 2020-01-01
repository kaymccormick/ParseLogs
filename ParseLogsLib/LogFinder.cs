using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace ParseLogsLib
{
    public interface ILogFinder
    {
        CommandBinding FindLogsCommandBinding { get; }
        ObservableCollection<LogItem> Files { get; }
        void Recurse(DirectoryInfo dir);
        void ProcessFile(FileInfo fsi);
    }

    [Export(typeof(ILogFinder))]
    public class LogFinder : ILogFinder
    {
        public Dispatcher Dispatcher { get; }
        public object FilesLock { get; } = new object();
        public ObservableCollection<LogItem> Files { get; } = new ObservableCollection<LogItem>();
        public CommandBinding FindLogsCommandBinding { get; set; }

        public LogFinder()
        {
            BindingOperations.EnableCollectionSynchronization(Files, FilesLock);
            FindLogsCommandBinding = new CommandBinding(Commands.FindLogsCommand, Executed);
        }

        public LogFinder(Dispatcher dispatcher)
        {
            BindingOperations.EnableCollectionSynchronization(Files, FilesLock);
            Dispatcher = dispatcher;
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
                    Console.WriteLine("xx" + String.Join(";", from file in Files select file.FullName));
                }
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
                Console.WriteLine(e);
                throw;
            }

            foreach (var dir2 in dirs)
            {
                this.Recurse(dir2);
            }

            IList<FileInfo> files = dir.GetFiles();
            Console.WriteLine($"{files.Count}");
            foreach (var file in files)
            {
                var isXml = file.Extension.ToLower().StartsWith(".xml");
                Console.WriteLine($"{file.Extension} {isXml}");
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
                // Console.WriteLine($"adding {fsi.Name}");
                // Console.WriteLine($"count is {app.Files.Count}");

                Files.Add(new LogItem(fsi));
                // Console.WriteLine($"count is {app.Files.Count}");
            }
        }
    }

}