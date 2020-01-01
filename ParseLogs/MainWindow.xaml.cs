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
            // s.Source = app.Files;
            // var d = CollectionViewSource.GetDefaultView(app.Files);
            // int c = (from x in d.SortDescriptions select x).Count();
            // app.Files.Add(new Item(null));
            // Console.WriteLine($"{c}");
            InitializeComponent();

            CommandBindings.Add(
                app.LogFinder.FindLogsCommandBinding);


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

        private void MainWindow_OnInitialized(object sender, EventArgs e)
        {
            SearchButton = _searchButton;
        }

        public Button SearchButton { get; set; }
    }
}
