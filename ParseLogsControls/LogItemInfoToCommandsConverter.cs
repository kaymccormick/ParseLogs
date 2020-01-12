using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;

namespace ParseLogsControls
{
    public class Contract : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            var r =  base.ResolveContractConverter(objectType);
            
            //Logger.Debug($"{objectType} ; {r.//GetType()}");
            return r;
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            if (objectType == typeof(LogLevel))
            {
                return new JsonObjectContract(objectType);
                var q = CreateProperties(objectType, MemberSerialization.OptIn);
                Debug.Assert(q.Any());
            }
            return base.CreateContract(objectType);
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {

            var r = base.CreateProperties(type, memberSerialization);
            // if (type == typeof(LogLevel))
            // {
            //     var t = this.ResolveContract(type) as JsonObjectContract;
            //     Logger.Debug(t.GetHashCode().ToString);
            //
            // }
            if (type == typeof(LogLevel))
            {
                foreach (var q in r)
                {
                    Logger.Debug($"eep {q.UnderlyingName}");
                    if (q.UnderlyingName == "Level")
                    {
                        //q.converte
                        q.ValueProvider = new LogLevelProvider();
                    }
                }

                return r;
            }
            if (typeof(LogEventInfo).IsAssignableFrom(type))
            {
                bool found = false;
                var r2 = r.Where(p => p.UnderlyingName != "FormattedMessage").ToList();
                found = r2.Count == r.Count - 1;
                foreach(var q in r2)
                {
                    if(q.UnderlyingName == "Level")
                    {
                        q.ValueProvider = new LogLevelProvider();
                    }
                }
                Debug.Assert(found);
                return r2;
                // foreach (var p in r.Where(property => property.UnderlyingName == "FormattedMessage"))
                // {
                //     p.Ignored = true;
                //     found = true;
                // }
                //
                // if (!found)
                // {
                //     throw new Exception("Beep");
                // }
            }

            return r;
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //private readonly DefaultContractResolver contractResolver { get { }};

        public Contract()
        {
            //contractResolver = new DefaultContractResolver();
            // LogEventInfoContract =
            //     new LogEventInfoContract(/*contractResolver.ResolveContract(typeof(LogEventInfo))*/);
            //var _wpf = base.ResolveContract(typeof(WpfLogEventInfo));
            //Logger.Debug($"default contract is {_wpf}");
            //WpfLogEventInfoContract = new WpfLogEventInfoContract(/*_wpf*/);
        }

        public override JsonContract ResolveContract(Type type)
        {
            var r = base.ResolveContract(type);
            if(type == typeof(LogLevel))
            {
                var q = r as JsonObjectContract;
                //q.MemberSerialization = MemberSerialization.OptIn;
                return q;
            }
            //Logger.Debug(r.GetType() + " " + type.FullName);
            return r;
        }

        private JsonContract _ResolveContract(Type type)
        {
            Logger.Debug($"resolve contract for {type}");
            if (type == typeof(LogLevel))
            {
            //     return new JsonObjectContract()
            //     {
            //         CreatorParameters = { }
            //         DefaultCreator =  
            //     }
            }
            if (typeof(WpfLogEventInfo).IsAssignableFrom(type))
            {
                Logger.Debug($"returning {WpfLogEventInfoContract}");
                return WpfLogEventInfoContract;
            }
            else if (typeof(LogEventInfo).IsAssignableFrom(type))
            {
                Logger.Debug($"returning {LogEventInfoContract}");
                return LogEventInfoContract;
            }

            return null;//cotractResolver.ResolveContract(type);
        }

        public JsonContract LogEventInfoContract { get; set; }

        public JsonContract WpfLogEventInfoContract { get; set; }
    }

    public class LogLevelProvider : IValueProvider
    {
        public void SetValue(object target, object value)
        {
            var item = target as LogEventInfo;
            var t = typeof(LogLevel);
            var f = t.GetField(value as string, BindingFlags.Static | BindingFlags.Public);
            item.Level = f.GetValue(null) as LogLevel;
        }

        public object GetValue(object target)
        {
            var item = target as LogEventInfo; 
            Debug.Assert(item != null);
            var dic = new Dictionary<string, string>();
            dic["Name"] = item.Level.ToString();
            return dic;
            //return item.Level;
            //return item.Level.ToString();
        }
    }

    public class WpfLogEventInfoContract : JsonObjectContract
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public WpfLogEventInfoContract() : base(typeof(WpfLogEventInfo))
        {
        }
    }

    public class LogEventInfoContract : JsonObjectContract
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public LogEventInfoContract() : base(typeof(LogEventInfo))
        {
        }
    }

    //[ValueConversion(typeof(WpfLogEventInfo), typeof(List<dynamic>))]
    public class LogItemInfoToCommandsConverter : TypeConverter, IValueConverter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public LogItemInfoToCommandsConverter()
        {
            Logger.Debug($"Constructing {nameof(LogItemInfoToCommandsConverter)}");
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            Logger.Debug($"CanConvertFrom(({context.Instance}, {context.PropertyDescriptor}), {sourceType})");
            var o = context.GetService(typeof(IProvideValueTarget));
            if (o == null)
            {
                Logger.Debug("no servie");
            }

            if (sourceType.IsSubclassOf(typeof(MarkupExtension)))
            {
                return true;
            }

            if (sourceType == typeof(LogEventInfo))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (context != null)
            {
                if (context.PropertyDescriptor != null)
                {
                    var type = context.PropertyDescriptor.PropertyType;
                    var desc = context.PropertyDescriptor.Description;
                    var name = context.PropertyDescriptor.Name;
                    var propDesc = String.Join(";", type, type, desc, name);
                    var o = context.GetService(typeof(IProvideValueTarget));
                    if (o == null)
                    {
                        Logger.Debug("no servie");
                    }

                    var o2 = context.GetService(typeof(IDestinationTypeProvider));
                    if (o2 == null)
                    {
                        Logger.Debug("no servie");
                    }

                    Logger.Debug($"CanConvertTo({context.Instance},{propDesc}, {destinationType})");
                }
            }
            else
            {
                Logger.Debug($"CanConvertTo(null, {destinationType})");
            }

            if (typeof(MarkupExtension).IsAssignableFrom(destinationType))
            {
                return true;
            }

            if (destinationType == typeof(String))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Logger.Debug("ConvertFrom");

            if (value is string s)
            {
                byte[] b = System.Convert.FromBase64String(s);
                var json = Encoding.UTF8.GetString(b);
                WpfLogEventInfo i = JsonConvert.DeserializeObject<WpfLogEventInfo>(json);
                return i;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (context != null)
            {
                var o = context.GetService(typeof(IProvideValueTarget));
                if (o == null)
                {
                    Logger.Debug("no servie");
                }

                var o2 = context.GetService(typeof(IDestinationTypeProvider));
                if (o2 == null)
                {
                    Logger.Debug("no servie");
                }

                var instance = context.Instance;
                var instanceStr = $"[{instance.GetType()}]";
                if (instance is FrameworkElement fElem)
                {
                    var elemStr = $"Name={fElem.Name}";
                    instanceStr += elemStr;
                }

                if (context.PropertyDescriptor != null)
                {
                    var propType = context.PropertyDescriptor.PropertyType;
                    var propName = context.PropertyDescriptor.Name;
                    var propComponentType = context.PropertyDescriptor.ComponentType;
                    var propString = $"[{propType}] [{propComponentType}].[{propName}]";
                    Logger.Debug($"prop: {propString}");
                }

                Logger.Debug($"instance: {instanceStr}");

                if (o is IProvideValueTarget service)
                {
                    Logger.Debug($"{service.TargetObject}.{service.TargetProperty}");
                }
            }

            Logger.Debug("ConvertTo");
            if (typeof(MarkupExtension).IsAssignableFrom(destinationType))
            {
                if (value == null)
                {
                    return new NullExtension();
                }

                WpfLogEventInfo i = (WpfLogEventInfo) value;
                var json = JsonConvert.SerializeObject(i);

                return new TestMarkupExtension(
                    System.Convert.ToBase64String(Encoding.UTF8.GetBytes(json)));
            }
            else if (destinationType == typeof(String))
            {
                WpfLogEventInfo i = (WpfLogEventInfo) value;
                var json = JsonConvert.SerializeObject(i);

                return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            }

            Logger.Debug("beep");
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueStr = "null";
            if (value == null)
            {
                Logger.Debug("Convert with null value object");
            }
            else
            {
                valueStr = $"{value} ({value.GetType()})";
            }


            Logger.Debug($"Call to {nameof(Convert)} ({valueStr}), {targetType}, {parameter}, {culture})");
            WpfLogEventInfo i = (WpfLogEventInfo) value;
            List<WpfLogEventInfoCommand> r = new List<WpfLogEventInfoCommand>();
            WpfLogEventInfoCommand e = new WpfLogEventInfoCommand
            {
                Command = DiagnosticCommands.ShowLogEventInfoCommand, WpfLogEventInfo = i
            };
            r.Add(e);
            Logger.Debug($"Returning {r}");
            return r;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Logger.Debug("Convert back");
            throw new NotImplementedException();
        }
    }

    public class WpfLogEventInfoCommand
    {
        public RoutedCommand Command { get; set; }
        public WpfLogEventInfo WpfLogEventInfo { get; set; }
    }
}