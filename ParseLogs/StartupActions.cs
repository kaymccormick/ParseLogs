using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ParseLogs.Properties;

namespace ParseLogs
{
    public static class StartupActions
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static Action FindCommandsFunc { get; } = FindCommands;

        public static void FindCommands()
        {
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                Logger.Trace("Processing assembly {assembly}", a);
                foreach (var t in a.GetExportedTypes())
                {
                    //Logger.Info($"{t}");
                    foreach (var field in t.GetFields(BindingFlags.Public | BindingFlags.Static |
                                                      BindingFlags.ExactBinding))
                    {
                        //Logger.Info($"{field}");
                        var v = field.GetValue(null);
                        if (v != null && v.GetType().IsInstanceOfType(typeof(ICommand)))
                        {
                            Logger.Debug($"{t.FullName}.{field.Name} is ICommand");
                        }
                    }
                }
            }

        }

        public static void CheckResources(FrameworkElement elem)
        {
            Logger.Debug($"Checking resources");
            var res = elem.Resources;
            if (res.Count == 0)
            {
                Logger.Warn("No resources in {FrameworkElement}", elem);
            }
            else
            {
                foreach (var r in res.Keys)
                {
                    var v = res[r];
                    Logger.Debug($"resource {r} = {v}");
                    if (!(v is string))
                    {
                        Logger.Debug($"Type is {v.GetType().Name});");
                        var typeConverter = TypeDescriptor.GetConverter(v.GetType(), true);
                        string vs;
                        if (typeConverter.CanConvertTo(typeof(string)))
                        {
                            vs = typeConverter.ConvertTo(v, typeof(string)) as string;
                        }
                        else
                        {
                            vs = "";
                        }

                        Logger.Debug($"value: {vs}");
                        if (v is ObjectDataProvider)
                        {
                            var od = v as ObjectDataProvider;
                            var d = od.Data;
                            Logger.Debug($"ObjectInstance is {od.ObjectInstance}, data is {d}");
                        }
                        else if (v is CollectionViewSource)
                        {
                            var cvs = v as CollectionViewSource;
                            Logger.Debug($"View is {cvs.View}");
                        }
                    }
                }
            }

        }
    }
}
