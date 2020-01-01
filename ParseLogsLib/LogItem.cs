using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ParseLogsLib.Annotations;

namespace ParseLogsLib
{
    public class LogItem : INotifyPropertyChanged
    {
        public FileInfo File { get; set; }
        public Exception Ex { get; set; }

        public String FullName => (File != null ? File.FullName : AltFullName);

        public string AltFullName { get; set; }

        public 
            LogItem(FileInfo file)
        {
            File = file;
        }

        public LogItem(FileInfo file, Exception ex)
        {
            File = file;
            Ex = ex;
        }

        public LogItem(string dirFullName, Exception ioException)
        {
            AltFullName = dirFullName;
            Ex = ioException;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
