using System.ComponentModel;
using System.Windows;

namespace ParseLogs
{
    [TypeConverter(typeof(UsageConverter))]
    [DataObject(true)]
    [DefaultBindingProperty("Examples")]
    public class Usage : AppDependencyObject
    {
        public static readonly DependencyProperty ExamplesProperty =
            DependencyProperty.Register("Examples", typeof(FreezableCollection<Example>), typeof(Usage), new FrameworkPropertyMetadata(new FreezableCollection<Example>(), FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnExamplesChanged)));

        private static void OnExamplesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Usage u = (Usage) d;
            u.RaiseEvent(new RoutedPropertyChangedEventArgs<FreezableCollection<Example>>((FreezableCollection<Example>) e.OldValue, (FreezableCollection<Example>) e.NewValue));
        }

        [DataObjectField(false, false, false)]
        public FreezableCollection<Example> Examples
        {
            get { return (FreezableCollection<Example>) GetValue(ExamplesProperty); }
            set { SetValue(ExamplesProperty, value); }
        }

        public static readonly RoutedEvent ExamplesChangedEvent = EventManager.RegisterRoutedEvent("ExamplesChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<FreezableCollection<Example>>), typeof(Usage));

        public event RoutedPropertyChangedEventHandler<FreezableCollection<Example>> ExamplesChanged
        {
            add {  AddHandler(ExamplesChangedEvent, value);}
            remove { RemoveHandler(ExamplesChangedEvent, value); }
        }
    }
}