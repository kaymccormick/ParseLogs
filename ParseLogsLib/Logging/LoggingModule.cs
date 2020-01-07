using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Module = Autofac.Module;

namespace ParseLogsLib.Logging
{
    class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //builder.RegisterAssemblyTypes(new Assembly[] {ThisAssembly});

        }
        
        
    }
}
