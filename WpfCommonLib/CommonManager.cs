using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCommonLib
{
    public class CommonManager
    {
        private static CommonManager _instance = null;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private IList<Application> Applications = new List<Application>();

        public static CommonManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new CommonManager();
                    return _instance;
                }

                return _instance;
            }
        }

        private CommonManager()
        {
            LoggerConfigurer configurer = new LoggerConfigurer();

        }

        public void RegisterApplication(Application app)
        {
            Logger.Debug($"Registering app {app}");
            WpfWindowManager manager = new WpfWindowManager();
                
            Applications.Add(app);
        }
    }
}
