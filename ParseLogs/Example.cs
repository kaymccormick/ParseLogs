using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommandLine.Text;

namespace ParseLogs
{
    public class Example : AppDependencyObject
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static  readonly  DependencyProperty HelpTextProperty = DependencyProperty.Register("HelpText", typeof(string), typeof(Example), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnHelpTextChanged)));

        private static void OnHelpTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            Example ex = (Example) d;
            Logger.Debug($"OnHelpTextChanged: old={args.OldValue}; new={args.NewValue}");
        
            ex.RaiseEvent(new RoutedPropertyChangedEventArgs<string>((string) args.OldValue, (string) args.NewValue)
            {
                RoutedEvent = Example.HelpTextChangedEvent,
            });

        
        }

        public string HelpText
        {
            get { return (string) GetValue(HelpTextProperty); }
            set { SetValue(HelpTextProperty, value); }
        }


        public event RoutedPropertyChangedEventHandler<string> HelpTextChanged
        {
            add
            {
                Logger.Debug("Add handler to help text changed event");
                AddHandler(HelpTextChangedEvent, value);
            }
            remove
            {
                Logger.Debug("remove handler to help text changed event"); 
                RemoveHandler(HelpTextChangedEvent, value);
            }
            }
        
        public static readonly RoutedEvent HelpTextChangedEvent = EventManager.RegisterRoutedEvent("HelpTextChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<string>), typeof(Example));

        // protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<string> args)
        // {
        //     RaiseEvent(args);
        // }

        //  Example e2 = (Example) d; 
            // RoutedPropertyChangedEventArgs<string> e3 = new RoutedPropertyChangedEventArgs<string>((string) e.OldValue, (string) e.NewValue);
        //     e2.OnHelpTextChanged(e3);
        // }
        //     throw new NotImplementedException();
        // }
    }
}
