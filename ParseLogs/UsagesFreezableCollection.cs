using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommandLine.Text;

namespace ParseLogs
{
    public class UsagesFreezableCollection<T> : FreezableCollection<T> where T : Usage
    {
        public UsagesFreezableCollection() : base()
        {
        }

        public UsagesFreezableCollection(IEnumerable<UsageInfo> usages) : base(new List<T>())
        {
            foreach (var u in usages)
            {
                object o = typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null);
                T instance = (T) o;
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

        public UsagesFreezableCollection(IEnumerable<T> usages) : base(usages)
        {
        }
    }
}
