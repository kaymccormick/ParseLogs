using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCommonLib
{
    public class CommonManager
    {
        private static CommonManager _instance = null;

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
    }
}
