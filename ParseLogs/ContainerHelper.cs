using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ParseLogsLib;

namespace ParseLogs
{
    public class ContainerHelper
    {
        public IContainer Container { get; private set; }
        private LogEntryFactory.Factory logFactory;

        public ContainerHelper()
        {
            CompanyName = GetCompanyForAssembly(GetType().Assembly);
        }

        public string CompanyName { get; set; }

        public void InitializeContainer()
        {
            var builder = new ContainerBuilder();
            var executingAssembly = Assembly.GetExecutingAssembly();
            Container = builder.Build();

            builder.RegisterAssemblyTypes(executingAssembly);
            foreach (var ass in FindRelevantAssemblies(executingAssembly))
            {

                builder.RegisterAssemblyTypes(ass);
            }
        }
        

        public IEnumerable<Assembly> FindRelevantAssemblies(Assembly refAssembly)
        {
            var myCompany = GetCompanyForAssembly(refAssembly);
            if (string.IsNullOrEmpty(myCompany))
            {
                throw new ContainerHelperException("No company info");
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var companyAssemblies = assemblies.Where(AssemblyCompanyMatch);
            return companyAssemblies;
        }

        private static string GetCompanyForAssembly(Assembly refAssembly)
        {
            object[] attribs = refAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
            string theCompany = "";
            if (attribs.Length > 0)
            {
                theCompany = ((AssemblyCompanyAttribute) attribs[0]).Company;
            }

            return theCompany;
        }

        private bool AssemblyCompanyMatch(Assembly arg)
        {
            return GetCompanyForAssembly(arg) == CompanyName;
        }
    }

    public class ContainerHelperException : Exception
    {
        public ContainerHelperException(string noCompanyInfo) : base(noCompanyInfo)
        {
        }
    }
}
