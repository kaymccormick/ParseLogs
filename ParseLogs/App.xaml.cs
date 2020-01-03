using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ParseLogsLib;
using WpfCommonLib;

namespace ParseLogs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public CommonManager CommonManager { get; }

        public App()
        {
            PresentationTraceSources.Refresh();

            CommonManager = CommonManager.Instance;

            foreach (var thread in Process.GetCurrentProcess().Threads)
            {
                
                ProcessThread t = thread as ProcessThread;
                Logger.Info($"Thread {t.Id} is \"\"");
            }
        }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private CompositionContainer _container; 
        [Import(typeof(ILogFinder))]
        public ILogFinder LogFinder { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Info("On startup");
            //Assert.True(Logger.IsDebugEnabled);
         //   InitCatalog();


        }

        public void InitCatalog()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Application).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ParseLogsLib.ILogFinder).Assembly));
            _container = new CompositionContainer(catalog);
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException exception)
            {
                Logger.Error(exception, "Unable to compose");
                ErrorExit();
            }
            
        }

        private static void ErrorExit()
        {
            System.Windows.Application.Current.Shutdown(1);
        }

        private void Application_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error(e.Exception, "Unhandled");
            ErrorExit();
            // foreach (var window in Windows)
            // {
            //     ((Window) window).Close();
            // }
        }
    }
}
