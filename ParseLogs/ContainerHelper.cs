using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Prism.Ioc;

namespace ParseLogs
{
    public class ContainerHelper
    {
        public IContainer Container { get; private set; }

        public ContainerHelper()
        {
        }

        public void InitializeContainer()
        {
            var builder = new ContainerBuilder();
            var a = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(a);
            Container = builder.Build();
        }
    }
}
