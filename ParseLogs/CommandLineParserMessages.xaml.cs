using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
using CommandLine.Text;

namespace ParseLogs
{
    /// <summary>
    /// Interaction logic for CommandLineParserMessages.xaml
    /// </summary>
    [DefaultBindingProperty("Usages")]
    public partial class CommandLineParserMessages : UserControl
    {
        public event RoutedPropertyChangedEventHandler<UsagesFreezableCollection> OnUsagesChanged
        {
            add { AddHandler(UsagesChangedEvent, value); }
            remove { RemoveHandler(UsagesChangedEvent, value); }
        }

        public static readonly RoutedEvent UsagesChangedEvent = EventManager.RegisterRoutedEvent("UsagesChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<UsagesFreezableCollection>), typeof(CommandLineParserMessages));

        protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<string> args)
        {
            RaiseEvent(args);
        }


        private static readonly DependencyProperty UsagesProperty = DependencyProperty.Register("Usages",
            typeof(UsagesFreezableCollection), typeof(CommandLineParserMessages),
            new FrameworkPropertyMetadata(new UsagesFreezableCollection(), FrameworkPropertyMetadataOptions.None,
                PropertyChangedCallback));
        [Bindable(true)]
        public UsagesFreezableCollection Usages
        {
            get { return (UsagesFreezableCollection) GetValue(UsagesProperty); }
            set { SetValue(UsagesProperty, value);}
        }

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private CompositeCollection _examples = null;


        public CompositeCollection Examples
        {
            get
            {
                if (_examples == null)
                {
                    _examples = new CompositeCollection();
                    foreach(var usage in Usages)
                    {
                        _examples.Add(usage.Examples);
                    }

                    return _examples;
                }

                return _examples;
            }
            set { _examples = value; }
        }

        public CommandLineParserMessages()
        {
            InitializeComponent();
        }
    }
}
