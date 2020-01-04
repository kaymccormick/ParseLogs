using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParseLogs
{
    public interface IDependencyProperties
    {
    }

    public class DependencyProperties : DependencyObject, IDependencyProperties
    {
        public static readonly DependencyProperty LogWindowTypeProperty = DependencyProperty.Register("LogWindowType",
            typeof(Type), typeof(DebugWindow),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnLogWindowTypeChanged));

        private static void OnLogWindowTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public Type LogWindowType
        {
            get { return (Type)GetValue(DependencyProperties.LogWindowTypeProperty); }
            set { SetValue(DependencyProperties.LogWindowTypeProperty, value); }
        }

    }
}
