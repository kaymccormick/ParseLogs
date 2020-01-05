using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommandLine.Text;

namespace ParseLogs
{
    public class UsageConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.IsSubclassOf(typeof(UsageInfo)))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);

        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            UsageInfo i = value as UsageInfo;
            if (i != null)
            {
                Usage u = new Usage();
                TypeConverter c = TypeDescriptor.GetConverter(typeof(Example));
                u.Examples =
                    new FreezableCollection<Example>(i.Examples.ToList()
                        .ConvertAll<Example>(input => (Example) c.ConvertFrom(input)));
                return u;
            }
            
            return base.ConvertFrom(context, culture, value);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(new DependencyPropertyDescriptor[] { DependencyPropertyDescriptor.FromProperty(Usage.ExamplesProperty, value.GetType()) });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
