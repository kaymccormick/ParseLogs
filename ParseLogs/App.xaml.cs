using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ParseLogs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public object FilesLock { get; } = new object();
        public ObservableCollection<Item> Files = new ObservableCollection<Item>(new List<Item>());
        protected override void OnStartup(StartupEventArgs e)
        {
            BindingOperations.EnableCollectionSynchronization(Files, FilesLock);

        }
    }
}
