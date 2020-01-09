using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
    public class Contract : IContractResolver
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private DefaultContractResolver contractResolver;


        public string GetResolvedPropertyName(string propertyName)
        {
            return contractResolver.GetResolvedPropertyName(propertyName);
        }

        public bool DynamicCodeGeneration => contractResolver.DynamicCodeGeneration;

        public BindingFlags DefaultMembersSearchFlags
        {
            get => contractResolver.DefaultMembersSearchFlags;
            set => contractResolver.DefaultMembersSearchFlags = value;
        }

        public bool SerializeCompilerGeneratedMembers
        {
            get => contractResolver.SerializeCompilerGeneratedMembers;
            set => contractResolver.SerializeCompilerGeneratedMembers = value;
        }

        public bool IgnoreSerializableInterface
        {
            get => contractResolver.IgnoreSerializableInterface;
            set => contractResolver.IgnoreSerializableInterface = value;
        }

        public bool IgnoreSerializableAttribute
        {
            get => contractResolver.IgnoreSerializableAttribute;
            set => contractResolver.IgnoreSerializableAttribute = value;
        }

        public bool IgnoreIsSpecifiedMembers
        {
            get => contractResolver.IgnoreIsSpecifiedMembers;
            set => contractResolver.IgnoreIsSpecifiedMembers = value;
        }

        public bool IgnoreShouldSerializeMembers
        {
            get => contractResolver.IgnoreShouldSerializeMembers;
            set => contractResolver.IgnoreShouldSerializeMembers = value;
        }

        public NamingStrategy NamingStrategy
        {
            get => contractResolver.NamingStrategy;
            set => contractResolver.NamingStrategy = value;
        }

        public Contract()
        {
            contractResolver = new DefaultContractResolver();
            LogEventInfoContract =
                new LogEventInfoContract(contractResolver.ResolveContract(typeof(LogEventInfo)));
            var _wpf = contractResolver.ResolveContract(typeof(WpfLogEventInfo));
            Logger.Debug($"default contract is {_wpf}");
            WpfLogEventInfoContract = new WpfLogEventInfoContract(_wpf);
        }

        public JsonContract ResolveContract(Type type)
        {
            Logger.Debug($"resolve contract for {type}");
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

            return contractResolver.ResolveContract(type);
        }

        public JsonContract LogEventInfoContract { get; set; }

        public JsonContract WpfLogEventInfoContract { get; set; }
    }

    public class WpfLogEventInfoContract : JsonObjectContract
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private JsonObjectContract defImpl;

        public WpfLogEventInfoContract(JsonContract resolveContract) : base(typeof(WpfLogEventInfo))
        {
            defImpl = (JsonObjectContract) resolveContract;
        }

        public Type UnderlyingType
        {
            get
            {
                var r = defImpl.UnderlyingType;
                Logger.Debug($"returning {r}");
                return r;
            }
        }


        public Type CreatedType
        {
            get
            {
                var t = defImpl.CreatedType;
                Logger.Debug($"{nameof(CreatedType)} returning {t}");
                return t;
            }

            set => defImpl.CreatedType = value;
        }

        public Boolean IsReference
        {
            get
            {
                var r = (bool) defImpl.IsReference;
                Logger.Debug($"returning {r}");
                return r;
            }

            set => defImpl.IsReference = value;
        }

        public JsonConverter Converter
        {
            get
            {
                var r = defImpl.Converter;
                Logger.Debug($"returning {r}");
                return r;
            }

            set => defImpl.Converter = value;
        }

        public JsonConverter InternalConverter {
	get {
	var r = defImpl.InternalConverter;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationCallback> OnDeserializedCallbacks {
	get {
	var r = defImpl.OnDeserializedCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationCallback> OnDeserializingCallbacks {
	get {
	var r = defImpl.OnDeserializingCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationCallback> OnSerializedCallbacks {
	get {
	var r = defImpl.OnSerializedCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationCallback> OnSerializingCallbacks {
	get {
	var r = defImpl.OnSerializingCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationErrorCallback> OnErrorCallbacks {
	get {
	var r = defImpl.OnErrorCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public Func<object> DefaultCreator
        {
            get
            {
                var r = defImpl.DefaultCreator;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.DefaultCreator = value;
        }

        public bool DefaultCreatorNonPublic
        {
            get
            {
                var r = defImpl.DefaultCreatorNonPublic;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.DefaultCreatorNonPublic = value;
        }

        public JsonConverter ItemConverter
        {
            get
            {
                var r = defImpl.ItemConverter;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemConverter = value;
        }

        public Boolean ItemIsReference
        {
            get
            {
                var r = (bool) defImpl.ItemIsReference;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemIsReference = value;
        }

        public ReferenceLoopHandling ItemReferenceLoopHandling
        {
            get
            {
                var r = (ReferenceLoopHandling) defImpl.ItemReferenceLoopHandling;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemReferenceLoopHandling = value;
        }

        public TypeNameHandling ItemTypeNameHandling
        {
            get
            {
                var r = (TypeNameHandling) defImpl.ItemTypeNameHandling;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemTypeNameHandling = value;
        }

        public MemberSerialization MemberSerialization
        {
            get
            {
                var r = defImpl.MemberSerialization;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.MemberSerialization = value;
        }

        public MissingMemberHandling MissingMemberHandling
        {
            get
            {
                var r = (MissingMemberHandling) defImpl.MissingMemberHandling;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.MissingMemberHandling = value;
        }

        public Required ItemRequired
        {
            get
            {
                var r = (Required) defImpl.ItemRequired;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemRequired = value;
        }

        public NullValueHandling ItemNullValueHandling
        {
            get
            {
                var r = (NullValueHandling) defImpl.ItemNullValueHandling;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemNullValueHandling = value;
        }

        public JsonPropertyCollection Properties {
	get {
	var r = defImpl.Properties;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public JsonPropertyCollection CreatorParameters {
	get {
	var r = defImpl.CreatorParameters;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public ObjectConstructor<object> OverrideCreator
        {
            get
            {
                var r = defImpl.OverrideCreator;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.OverrideCreator = value;
        }

        public ExtensionDataSetter ExtensionDataSetter
        {
            get
            {
                var r = defImpl.ExtensionDataSetter;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ExtensionDataSetter = value;
        }

        public ExtensionDataGetter ExtensionDataGetter
        {
            get
            {
                var r = defImpl.ExtensionDataGetter;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ExtensionDataGetter = value;
        }

        public Type ExtensionDataValueType
        {
            get
            {
                var r = defImpl.ExtensionDataValueType;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ExtensionDataValueType = value;
        }

        public Func<string, string> ExtensionDataNameResolver
        {
            get
            {
                var r = defImpl.ExtensionDataNameResolver;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ExtensionDataNameResolver = value;
        }
    }

    public class LogEventInfoContract : JsonObjectContract
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private JsonObjectContract defImpl;

        public LogEventInfoContract(JsonContract def) : base(typeof(LogEventInfo))
        {
            defImpl = (JsonObjectContract) def;
        }

        public Type UnderlyingType
        {
            get
            {
                var r = defImpl.UnderlyingType;
                Logger.Debug($"returning {r}");
                return r;
            }
        }

        public Type CreatedType
        {
            get
            {
                var r = defImpl.CreatedType;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.CreatedType = value;
        }

        public Boolean IsReference
        {
            get
            {
                var r = (bool) defImpl.IsReference;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.IsReference = value;
        }

        public JsonConverter Converter
        {
            get
            {
                var r = defImpl.Converter;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.Converter = value;
        }

        public JsonConverter InternalConverter {
	get {
	var r = defImpl.InternalConverter;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationCallback> OnDeserializedCallbacks {
	get {
	var r = defImpl.OnDeserializedCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationCallback> OnDeserializingCallbacks {
	get {
	var r = defImpl.OnDeserializingCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationCallback> OnSerializedCallbacks {
	get {
	var r = defImpl.OnSerializedCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationCallback> OnSerializingCallbacks {
	get {
	var r = defImpl.OnSerializingCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public IList<SerializationErrorCallback> OnErrorCallbacks {
	get {
	var r = defImpl.OnErrorCallbacks;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public Func<object> DefaultCreator
        {
            get
            {
                var r = defImpl.DefaultCreator;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.DefaultCreator = value;
        }

        public bool DefaultCreatorNonPublic
        {
            get
            {
                var r = defImpl.DefaultCreatorNonPublic;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.DefaultCreatorNonPublic = value;
        }

        public JsonConverter ItemConverter
        {
            get
            {
                var r = defImpl.ItemConverter;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemConverter = value;
        }

        public Boolean ItemIsReference
        {
            get
            {
                var r = (bool) defImpl.ItemIsReference;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemIsReference = value;
        }

        public ReferenceLoopHandling ItemReferenceLoopHandling
        {
            get
            {
                var r = (ReferenceLoopHandling) defImpl.ItemReferenceLoopHandling;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemReferenceLoopHandling = value;
        }

        public TypeNameHandling ItemTypeNameHandling
        {
            get
            {
                var r = (TypeNameHandling) defImpl.ItemTypeNameHandling;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemTypeNameHandling = value;
        }

        public MemberSerialization MemberSerialization
        {
            get
            {
                var r = defImpl.MemberSerialization;
                Logger.Debug($"{nameof(MemberSerialization)} = {r}");
                return r;
            }
            set => defImpl.MemberSerialization = value;
        }

        public MissingMemberHandling MissingMemberHandling
        {
            get
            {
                var r = (MissingMemberHandling) defImpl.MissingMemberHandling;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.MissingMemberHandling = value;
        }

        public Required ItemRequired
        {
            get
            {
                var r = (Required) defImpl.ItemRequired;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemRequired = value;
        }

        public NullValueHandling ItemNullValueHandling
        {
            get
            {
                var r = (NullValueHandling) defImpl.ItemNullValueHandling;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ItemNullValueHandling = value;
        }

        public JsonPropertyCollection CreatorParameters {
	get {
	var r = defImpl.CreatorParameters;
	Logger.Debug($"Returning {r}");
	return r;
	}
	}

        public ObjectConstructor<object> OverrideCreator
        {
            get
            {
                var r = defImpl.OverrideCreator;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.OverrideCreator = value;
        }

        public ExtensionDataSetter ExtensionDataSetter
        {
            get
            {
                var r = defImpl.ExtensionDataSetter;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ExtensionDataSetter = value;
        }

        public ExtensionDataGetter ExtensionDataGetter
        {
            get
            {
                var r = defImpl.ExtensionDataGetter;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ExtensionDataGetter = value;
        }

        public Type ExtensionDataValueType
        {
            get
            {
                var r = defImpl.ExtensionDataValueType;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ExtensionDataValueType = value;
        }

        public Func<string, string> ExtensionDataNameResolver
        {
            get
            {
                var r = defImpl.ExtensionDataNameResolver;
                Logger.Debug($"returning {r}");
                return r;
            }
            set => defImpl.ExtensionDataNameResolver = value;
        }

        public JsonPropertyCollection Properties
        {
            get
            {
                var r = defImpl.Properties;
                foreach (var w in r)
                {
                    if (w.UnderlyingName == "MessageFormatter")
                    {
                        w.Ignored = true;
                    }
                }

                return r;
            }
        }
    }

    [ValueConversion(typeof(WpfLogEventInfo), typeof(List<dynamic>))]
    public class LogItemInfoToCommandsConverter : TypeConverter, IValueConverter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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
                var type = context.PropertyDescriptor.PropertyType;
                var ctype = context.PropertyDescriptor.ComponentType;
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
            if (context == null)
            {
                var o = context.GetService(typeof(IProvideValueTarget));
                if (o == null)
                {
                    Logger.Debug("no servie");
                }

                if (value == null)
                {
                    Logger.Debug("value is null");
                }

                var propType = context.PropertyDescriptor.PropertyType;
                var propName = context.PropertyDescriptor.Name;
                var propComponentType = context.PropertyDescriptor.ComponentType;
                var propString = $"[{propType}] [{propComponentType}].[{propName}]";
                Logger.Debug($"prop: {propString}");
            }

            if (value is string)
            {
                byte[] b = System.Convert.FromBase64String((string) value);
                var json = System.Text.Encoding.UTF8.GetString(b);
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
                var felem = instance as FrameworkElement;
                if (felem != null)
                {
                    var elemStr = $"Name={felem.Name}";
                    instanceStr += elemStr;
                }

                var propType = context.PropertyDescriptor.PropertyType;
                var propName = context.PropertyDescriptor.Name;
                var propComponentType = context.PropertyDescriptor.ComponentType;
                var propString = $"[{propType}] [{propComponentType}].[{propName}]";
                Logger.Debug($"prop: {propString}");
                Logger.Debug($"instance: {instanceStr}");

                var service = o as IProvideValueTarget;
                if (service != null)
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
                    System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json)));
            }
            else if (destinationType == typeof(String))
            {
                WpfLogEventInfo i = (WpfLogEventInfo) value;
                var json = JsonConvert.SerializeObject(i);

                return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json));
            }

            Logger.Debug("beep");
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IServiceProvider provider;
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
            WpfLogEventInfoCommand e = new WpfLogEventInfoCommand();
            e.Command = DiagnosticCommands.ShowLogEventInfoCommand;
            e.WpfLogEventInfo = i;
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