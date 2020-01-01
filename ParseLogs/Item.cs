using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ParseLogs.Annotations;

namespace ParseLogs
{
    public class Item : INotifyPropertyChanged
    {
        public FileInfo File { get; set; }
        public Exception Ex { get; set; }

        public String FullName => (File != null ? File.FullName : AltFullName);

        public string AltFullName { get; set; }

        public Item(FileInfo file)
        {
            File = file;
        }

        public Item(FileInfo file, Exception ex)
        {
            File = file;
            Ex = ex;
        }

        public Item(string dirFullName, Exception ioException)
        {
            AltFullName = dirFullName;
            Ex = ioException;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged1([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
