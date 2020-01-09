using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ParseLogs.Properties;

namespace ParseLogs
{
    public static class DebugCommands
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static readonly RoutedCommand ShowLogEventInfoCommand =
            new RoutedUICommand("Show Log Event Info", nameof(ShowLogEventInfoCommand), typeof(DebugCommands));
/*        public static readonly ICommand DumpResources = new DelegateCommand(() =>
        {
            if (Logger.IsDebugEnabled)
            {
                var curApp = Application.Current;
                Logger.Debug($"Cur app is {curApp}");
                var merged = curApp.Resources.MergedDictionaries;
                foreach (var a in merged)
                {
                    Logger.Debug($"Resource dict is {a}");
                }
                Logger.Debug($"Checking resources");
                /*if (Resources.Count == 0)
                {
                    Logger.Warn($"No resources in MainWindow");
                }
                else
                {
                    foreach (var r in Resources.Keys)
                    {
                        var v = TryFindResource(r);
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

                ObjectDataProvider o = TryFindResource("DrivesProvider") as ObjectDataProvider;
                var typ = o.ObjectInstance.GetType().ToString();
                Logger.Info($"{o.ObjectInstance}");
                
            }
        }, () => true);
    */
    }
}