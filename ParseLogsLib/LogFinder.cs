using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParseLogsLib
{
    public interface ILogFinder
    {
        void Execute();
        void Recurse(DirectoryInfo dir);
        void ProcessFile(FileInfo fsi);
    }

    [Export(typeof(ILogFinder))]
    public class LogFinder : ILogFinder
    {
        public object FilesLock { get; } = new object();
        public ObservableCollection<LogItem> Files { get; } = new ObservableCollection<LogItem>();

        public void Execute()
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
                DirectoryInfo dir = new DirectoryInfo(@"H:\Users\root\AppData\Roaming\Trillian\users\paigeat");
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