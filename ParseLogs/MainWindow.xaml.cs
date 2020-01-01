using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using ParseLogs.Annotations;
using ItemsControl = System.Windows.Controls.ItemsControl;
using Path = System.IO.Path;

namespace ParseLogs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private App app;
        public MainWindow()
        {
            CollectionViewSource s = new CollectionViewSource();

            app = Application.Current as App;
            s.Source = app.Files;
            var d = CollectionViewSource.GetDefaultView(app.Files);
            int c = (from x in d.SortDescriptions select x).Count();
            app.Files.Add(new Item(null));
            Console.WriteLine($"{c}");
            InitializeComponent();

//            FilesListView.ItemsSource = app.Files;
//            FilesListView.
/*            Binding binding = new Binding("FullName")
            {
                Source = this.Files
                
            };

            var itemsSourceProperty = ItemsControl.ItemsSourceProperty;

            BindingOperations.SetBinding(FilesListView, itemsSourceProperty, binding);
            */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            lock (app.FilesLock)
            {
                app.Files.Clear();
            }
            CancellationTokenSource s =  new CancellationTokenSource(
                );
            var token = s.Token;
            Task.Run(() => {
                DirectoryInfo dir = new DirectoryInfo(@"H:\Users\root\AppData\Roaming\Trillian\users\paigeat");
                Recurse(dir);
                lock (app.FilesLock)
                {
                    Console.WriteLine("xx" + String.Join(";", from file in app.Files select file.FullName));
                }
            }, token);
        }

        private void Recurse(DirectoryInfo dir)
        {
            IEnumerable<DirectoryInfo> dirs ;
            //Console.WriteLine(dir.FullName);
            try
            {
                dirs = dir.GetDirectories();
            }
            catch (IOException e)
            {
                Item item = new Item(dir.FullName, e);
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

        private void ProcessFile(FileInfo fsi)
        {
            lock (app.FilesLock)
            {
                Console.WriteLine($"adding {fsi.Name}");
                Console.WriteLine($"count is {app.Files.Count}");
                
                app.Files.Add(new Item(fsi));
                Console.WriteLine($"count is {app.Files.Count}");
            }
        }

    }
}
