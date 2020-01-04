using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NLog;
using Prism.Commands;
using Type = System.Type;

namespace ParseLogsLib
{
    public static class ParseLogsCommands
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        class ShowWindowCommandImpl : ICommand
        {
            public bool CanExecute(object parameter)
            {
                Window window;
                Type t;
                HandleParameter(parameter, out window, out t);
                object[] attribs = t.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
                string myCompany = "";
                if (attribs.Length > 0)
                {
                    myCompany = ((AssemblyCompanyAttribute) attribs[0]).Company;
                }

                foreach (var w in Application.Current.Windows)
                {
                    var type = w.GetType();
                    var ass = type.Assembly;

                    object[] myAttribs = ass.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
                    string company = "";

                    if (myAttribs.Length > 0)
                    {
                        var a = (AssemblyCompanyAttribute) myAttribs[0];
                        company = a.Company;
                    }

                    if (company != myCompany)
                    {
                        Logger.Trace("Ignoring window {window} company is {company} not {myCompany}",
                            w, company, myCompany);
                        continue;
                    }

                    Logger.Debug("Checking {window} to see if it is a {type}", w, t);
                    if (type.IsSubclassOf(t) || type == t)
                    {
                        Logger.Debug("returning false for {action}", this);
                        return false;
                    }
                }

                Logger.Debug("returning true for {action}", this);
                return true;
            }

            private void HandleParameter(object parameter, out Window window, out Type type)
            {
                window = parameter as Window;
                if (window != null)
                {
                    type = window.GetType();
                    return;
                }

                type = parameter as Type;
                if (type == null)
                {
                    throw new ArgumentException(
                        String.Format("Invalid parameter for command {command}, {parameter}", this, parameter),
                        "parameter");
                }

                return;
            }

            public void Execute(object parameter)
            {
                Logger.Debug("Executing command {command}", this);
                if (!CanExecute(parameter))
                {
                    throw new Exception();
                }

                Type type;
                Window w;
                HandleParameter(parameter, out w, out type);
                if (type == null)
                {
                    throw new ArgumentException("Invalid argument", "parameter");
                }

                if (w != null)
                {
                    if (w.Visibility != Visibility.Visible)
                    {
                        w.Show();
                    }

                    return;
                }

                w = type.GetConstructor(Type.EmptyTypes).Invoke(null) as Window;
                if (w == null)
                {
                    throw new ArgumentException();
                }

                w.Show();
                w.Initialized += (sender, args) =>
                {
                    Logger.Debug("In initialized, calling CanExecuteChanged handler.");
                    var eventArgs = new EventArgs();
                    CanExecuteChanged.Invoke(this, EventArgs.Empty);
                };
            }


            public event EventHandler CanExecuteChanged;
        }

        public static RoutedCommand FindLogsCommand = new RoutedCommand();

        public static ICommand ShowWindowCommand { get; } = new ShowWindowCommandImpl();
    }
}