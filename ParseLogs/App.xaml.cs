using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ParseLogsLib;

namespace ParseLogs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CompositionContainer _container; 
        [Import(typeof(ILogFinder))]
        public ILogFinder LogFinder { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            InitCatalog();
            //BindingOperations.EnableCollectionSynchronization(Files, FilesLock);
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
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
