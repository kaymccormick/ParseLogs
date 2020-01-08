using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommandLine.Text;

namespace ParseLogs
{
    public class UsagesFreezableCollection : FreezableCollection<Usage> 
    {
        public UsagesFreezableCollection() : base()
        {
        }

        public UsagesFreezableCollection(IEnumerable<UsageInfo> usages) : base(new List<Usage>())
        {
            foreach (var u in usages)
            {
                Usage instance = new Usage();
                foreach(var example in u.Examples)
                {
                    instance.Examples.Add(new Example()
                    {
                        HelpText = example.MapResult.HelpText,
                    });
                }

                Add(instance);
            }
        }

        public UsagesFreezableCollection(IEnumerable<Usage> usages) : base(usages)
        {
        }
    }
}
