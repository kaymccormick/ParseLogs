using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ParseLogsLib
{
    public class FileFinder : ICommand
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private IEnumerable<Lazy<IFinderSubject>> subjects;

        public class FoundItem
        {
            public string Name { get; set; }
            public FileSystemInfo Info { get; }
        }


        public FileFinder(IEnumerable<Lazy<IFinderSubject>> subjects)
        {
            this.subjects = subjects;
        }

        public FileFinder()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public object FilesLock { get; } = new object();

        public void Execute(object parameter)
        {
            var dirVal = parameter as string;
            lock (FilesLock)
            {
                //Files.Clear();
            }

            CancellationTokenSource s = new CancellationTokenSource();
            var token = s.Token;
            Task<TaskResult<int?>>.Run(() =>
            {
                int? count = null;
                try
                {
                    DirectoryInfo
                        dir = new DirectoryInfo(dirVal);
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

                    // Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    //
                    //     new RaiseDelegate(() => { RaiseEvent(new RoutedEventArgs(CommandFailedEvent, this)); }));
                    return new TaskResult<int?>(null);
                }

                return new TaskResult<int?>(0);
            }, token);
        }

        public ICollection<IItem> Files { get; }

        public event EventHandler CanExecuteChanged;

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
                IItem item = new Item(fsi);
                // Console.WriteLine();
                // Console.WriteLine($"count is {app.Files.Count}")
                foreach (var s in subjects)

                {
                    if (s.Value.selectionDelegate.Invoke(item))
                    {
                        s.Value.ProcessAction.Invoke(item);
                    }
                }
            }
        }

        public bool isTraceLogging { get; set; }
    }

    internal class TaskResult<T>
    {
        public Exception Ex { get; }
        public T Result { get; }

        public TaskResult(T result, Exception ex)
        {
            this.Result = result;
            this.Ex = ex;
        }

        public TaskResult(T result)
        {
            this.Result = result;
        }
    }
}