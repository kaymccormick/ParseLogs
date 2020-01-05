using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Shapes;

namespace ParseLogs
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public static IEnumerable<ProcessThread> Threads { get; } =
            Process.GetCurrentProcess().Threads.Cast<ProcessThread>() as IEnumerable<ProcessThread>;

        public static readonly DependencyProperty LogWindowTypeProperty = DependencyProperties.LogWindowTypeProperty;
        public Type LogWindowType
        {
            get { return (Type)GetValue(LogWindowTypeProperty); }
            set { SetValue(LogWindowTypeProperty, value); }
        }

        public DebugWindow()
        {
            InitializeComponent();
            Binding binding = new   Binding
            {
                Source = Application.Current,
                Path = new PropertyPath("LogWindowType")
            };
            SetBinding(LogWindowTypeProperty, binding);
        }

        private void DebugWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            StartupActions.CheckResources(sender as Window);
        }
    }
}