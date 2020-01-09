using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParseLogsControlsTests
{
    class WindowWrap<T> : Window
    {
        private T contentInstance;
        public WindowWrap()
        {
            try
            {
                contentInstance = Activator.CreateInstance<T>();
                Content = contentInstance;
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}
